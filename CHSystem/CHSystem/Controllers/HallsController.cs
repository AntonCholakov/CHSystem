using CHSystem.Models;
using CHSystem.Repositories;
using CHSystem.ViewModels.Halls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHSystem.Controllers
{
    public class HallsController : BaseController
    {
        HallRepository hallRep = new HallRepository();
        // GET: Halls
        public ActionResult List()
        {
            HallsListVM model = new HallsListVM();
            TryUpdateModel(model);
            model.Halls = hallRep.GetAll();

            if (!String.IsNullOrEmpty(model.Search))
            {
                model.Search = model.Search.ToLower();
                model.Halls = model.Halls.Where(h => h.Name.ToLower().Contains(model.Search)).ToList();
            }

            model.Props = new Dictionary<string, object>();
            switch (model.SortOrder)
            {
                case "name_desc":
                    model.Halls = model.Halls.OrderByDescending(h => h.Name).ToList();
                    break;
                case "name_asc":
                default:
                    model.Halls = model.Halls.OrderBy(h => h.Name).ToList();
                    break;
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            Hall hall;

            if (!id.HasValue)
            {
                hall = new Hall();
            }
            else
            {
                hall = hallRep.GetByID(id.Value);
                if (hall==null)
                {
                    return RedirectToAction("List");
                }
            }
            HallsEditVM model = new HallsEditVM();
            model.ID = hall.ID;
            model.Name = hall.Name;
            model.LocationID = hall.LocationID;
            model.Locations = GetLocations();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            HallsEditVM model = new HallsEditVM();
            TryUpdateModel(model);
            
            if (!ModelState.IsValid)
            {
                model.Locations = GetLocations();
                return View(model);
            }

            Hall hall;
            if (model.ID==0)
            {
                hall = new Hall();
            }
            else
            {
                hall = hallRep.GetByID(model.ID);
                if (hall==null)
                {
                    return RedirectToAction("List");
                }
            }
            hall.ID = model.ID;
            hall.Name = model.Name;
            hall.LocationID = model.LocationID;

            hallRep.Save(hall);

            return RedirectToAction("List");
        }

        private IEnumerable<SelectListItem> GetLocations()
        {
            var locations = new LocationRepository().GetAll()
                .Select(l => new SelectListItem
                {
                    Value = l.ID.ToString(),
                    Text = l.Name
                });

            return new SelectList(locations, "Value", "Text");
        }

        public ActionResult Delete(int id)
        {
            hallRep.Delete(id);

            return RedirectToAction("List");
        }
    }
}