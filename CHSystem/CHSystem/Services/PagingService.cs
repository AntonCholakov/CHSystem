using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Reflection;
using CHSystem.ViewModels.Pager;
using CHSystem.Attributes;

namespace CHSystem.Services
{
    /// <summary>
    /// Manages paging view models and fills some properties automatically.
    /// Warning! Paging View has a form in it. Don't include view in another form! It goes agains W3C specifications!
    /// </summary>
    public static class PagingService
    {
        private class ModelInfo
        {
            public PropertyInfo Pager { get; set; }

            public PropertyInfo[] FilterProperties { get; set; }

            public System.Reflection.PropertyInfo CollectionProperty { get; set; }
        }

        private class PropertyInfo
        {
            public System.Reflection.PropertyInfo Property { get; set; }
            public string Alias { get; set; }
        }

        static PagingService()
        {
            modelInfoCollection = new Dictionary<Type, ModelInfo>();

            var type = typeof(PagingVM);

            // get the alias of the ItemsPerPage property
            var itemsProp = type.GetProperty("ItemsPerPage");
            var itemsAlias = itemsProp.GetCustomAttribute<AliasAttribute>();
            ItemsPerPageAlias = itemsAlias != null ? itemsAlias.Name : itemsProp.Name;

            // get the alias of the CurrentPage property
            var pageProp = type.GetProperty("CurrentPage");
            var pageAlias = pageProp.GetCustomAttribute<AliasAttribute>();
            CurrentPageAlias = pageAlias != null ? pageAlias.Name : pageProp.Name;
        }

        private static Dictionary<Type, ModelInfo> modelInfoCollection { get; set; }
        public static string ItemsPerPageAlias { get; private set; }
        public static string CurrentPageAlias { get; private set; }

        /// <summary>
        /// Prepares the specified model, automatically filling its collection, link parameters and preparing the pager.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">The model.</param>
        /// <param name="context">The context.</param>
        /// <param name="items">The items.</param>
        /// <param name="orderFunc">The order function.</param>
        /// <exception cref="System.ArgumentNullException">Model must be instanciated.</exception>
        public static void Prepare<T>(object model, ControllerContext context, IEnumerable<T> items, Func<IEnumerable<T>, IEnumerable<T>> orderFunc = null)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Model must be instanciated.");
            }

            var type = model.GetType();

            // get model info
            if (!modelInfoCollection.ContainsKey(type))
            {
                modelInfoCollection.Add(type, GetProperties(type));
            }
            var modelInfo = modelInfoCollection[type];

            // get pager
            var pager = GetPager(context, (PagingVM)modelInfo.Pager.Property.GetValue(model));

            // calculate pager properties
            pager.Prefix = modelInfo.Pager.Alias;
            pager.CalcParameters(items.Count());

            modelInfo.Pager.Property.SetValue(model, pager);

            // set paged items
            modelInfo.CollectionProperty.SetValue(model, PagingService.GetItems(pager, items, orderFunc));

            // add filter properties to link parameters
            foreach (var prop in modelInfo.FilterProperties)
            {
                pager.LinkParameters.Add(prop.Alias, prop.Property.GetValue(model));
            }
        }

        /// <summary>
        /// Gets the properties of the model type.
        /// </summary>
        /// <param name="model">The model.</param>
        private static ModelInfo GetProperties(Type model)
        {
            var allProperties = model.GetProperties();
            var pagerType = typeof(PagingVM);
            var modelInfo = new ModelInfo();

            var pagerProp = allProperties.FirstOrDefault(p => p.PropertyType == pagerType);

            if (pagerProp == null)
            {
                throw new NotSupportedException("This service must work with types that contain a property of type PagingVM.");
            }

            var pagerAlias = pagerProp.GetCustomAttribute<AliasAttribute>();

            modelInfo.Pager = new PropertyInfo
            {
                Property = pagerProp,
                Alias = pagerAlias != null ? pagerAlias.Name : pagerProp.Name
            };
            
            modelInfo.CollectionProperty = allProperties.FirstOrDefault(p => p.GetCustomAttribute<CollectionPropertyAttribute>() != null);

            if (modelInfo.CollectionProperty == null)
            {
                throw new NotSupportedException("Specify the collection property.");
            }

            var filterProps = allProperties.Where(p => p.GetCustomAttribute<FilterPropertyAttribute>() != null).ToArray();
            modelInfo.FilterProperties = new PropertyInfo[filterProps.Length];

            for (int i = 0; i < filterProps.Length; i++)
            {
                var filterAlias = filterProps[i].GetCustomAttribute<AliasAttribute>();

                modelInfo.FilterProperties[i] = new PropertyInfo
                {
                    Property = filterProps[i],
                    Alias = filterAlias != null ? filterAlias.Name : filterProps[i].Name
                };
            }

            return modelInfo;
        }


        /// <summary>
        /// Get a new paging view model or fill the existing one with specific values for the given context
        /// </summary>
        /// <param name="context">Context, based on which the values will be</param>
        /// <param name="pager">The paging view model</param>
        /// <returns></returns>
        public static PagingVM GetPager(ControllerContext context, PagingVM pager = null)
        {
            if (pager == null)
            {
                pager = new PagingVM();

            }

            pager.Controller = context.RouteData.Values["controller"].ToString();
            pager.Action = context.RouteData.Values["action"].ToString();
            pager.Prefix = "Pager";

            return pager;
        }

        /// <summary>
        /// Calculate the paging view model's page count and current page, based on the filtered items' count.
        /// </summary>
        /// <param name="model">The paging view model</param>
        /// <param name="count">The filtered items' count.</param>
        public static void CalcParameters(this PagingVM model, int count)
        {
            if (model.LinkParameters == null)
            {
                model.LinkParameters = new Dictionary<string, object>();
            }

            // if the itemsPerPage or CurrentPage are default, there is no need to clutter the url
            if (model.ItemsPerPage != 5)
            {
                model.LinkParameters[model.Prefix + "." + ItemsPerPageAlias] = model.ItemsPerPage;
            }
            
            if (model.CurrentPage > 1)
            {
                model.LinkParameters[model.Prefix + "." + CurrentPageAlias] = model.CurrentPage;
            }

            // calculates the page count (how many pages there are) and the current page
            model.PageCount =
                count <= 0 ? 1 :
                count % model.ItemsPerPage == 0 ? count / model.ItemsPerPage :
                (count / model.ItemsPerPage) + 1;

            model.CurrentPage =
                model.PageCount < model.CurrentPage ? model.PageCount : model.CurrentPage;
        }

        /// <summary>
        /// Automates the skip, take and sort method calling. Using the given pager, it will take the needed items and return the items for the current page.
        /// </summary>
        /// <typeparam name="T">Item type, that populates the list</typeparam>
        /// <param name="pager">Pager</param>
        /// <param name="items">Items after filtration</param>
        /// <param name="orderFunc">Order the items are required to be in</param>
        /// <returns>Items for the current page</returns>
        public static List<T> GetItems<T>(PagingVM pager, IEnumerable<T> items, Func<IEnumerable<T>, IEnumerable<T>> orderFunc = null)
        {
            var result = items;

            if (orderFunc != null)
            {
                result = orderFunc(result);
            }

            return result.Skip((pager.CurrentPage - 1) * pager.ItemsPerPage).Take(pager.ItemsPerPage).ToList();
        }

        /// <summary>
        /// Automates the preparation of the pager by configuring it and returning the items for the current page.
        /// </summary>
        /// <typeparam name="T">Item type, that populates the list</typeparam>
        /// <param name="pager">Pager</param>
        /// <param name="items">Items after filtration</param>
        /// <param name="orderFunc">Order the items are required to be in</param>
        /// <returns>Items for the current page</returns>
        public static List<T> Prepare<T>(PagingVM pager, IEnumerable<T> items, Func<IEnumerable<T>, IEnumerable<T>> orderFunc = null)
        {
            pager.CalcParameters(items.Count());
            return PagingService.GetItems(pager, items, orderFunc);
        }

        /// <summary>
        /// Add additional link parameters to the pager
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="linkParams"></param>
        public static void AddLinkParameters(this PagingVM pager, IEnumerable<KeyValuePair<string, object>> linkParams)
        {
            if (pager.LinkParameters == null)
            {
                pager.LinkParameters = new Dictionary<string, object>();
            }

            foreach (var item in linkParams)
            {
                pager.LinkParameters.Add(item.Key, item.Value);
            }
        }
    }
}