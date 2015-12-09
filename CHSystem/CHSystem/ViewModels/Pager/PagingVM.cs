using CHSystem.Attributes;

namespace CHSystem.ViewModels.Pager
{
    /// <summary>
    /// Holds information, which the corresponding view uses to calculate and render page numbers.
    /// Warning! Paging View has a form in it. Don't include view in another form! It goes agains W3C specifications!
    /// </summary>
    public class PagingVM
    {
        /// <summary>
        /// Name of the action the pager exists in
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Name of the controller the pager exists in
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Prefix to use in links. Must match path to the PagingVM in the view model it is used in. Default: Pager
        /// </summary>
        public string Prefix { get; set; }

        private int pageCount = 1;

        /// <summary>
        /// Gets or sets the page count (items / perPage).
        /// </summary>
        /// <value>
        /// The page count.
        /// </value>
        public int PageCount
        {
            get { return pageCount; }
            set { pageCount = value > 1 ? value : 1; }
        }

        private int currentPage = 1;

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value > 1 ? value : 1; }
        }

        private int itemsPerPage = 5;

        /// <summary>
        /// Gets or sets how many items will be rendered per page.
        /// </summary>
        /// <value>
        /// The items per page.
        /// </value>
        public int ItemsPerPage
        {
            get { return itemsPerPage; }
            set { itemsPerPage = value > 1 ? value : 1; }
        }


        private int maxPageNumbersShown = 7;

        /// <summary>
        /// Gets or sets the maximum page numbers shown.
        /// </summary>
        /// <value>
        /// The maximum page numbers shown.
        /// </value>
        public int MaxPageNumbersShown
        {
            get { return maxPageNumbersShown; }
            set { maxPageNumbersShown = value; }
        }

        /// <summary>
        /// Gets or sets the link parameters.
        /// </summary>
        /// <value>
        /// The link parameters.
        /// </value>
        public System.Collections.Generic.Dictionary<string, object> LinkParameters { get; set; }
    }
}