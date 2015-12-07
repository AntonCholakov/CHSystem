using CHSystem.Models;
using CHSystem.Repositories;
using CHSystem.ViewModels.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHSystem.Controllers
{
    public class LocationsController : BaseController
    {
        LocationRepository locationRep = new LocationRepository();

        public ActionResult List()
        {
            LocationsListVM model = new LocationsListVM();
            TryUpdateModel(model);
            model.Locations = locationRep.GetAll();


            if (!String.IsNullOrEmpty(model.Search))
            {
                model.Search = model.Search.ToLower();
                model.Locations = model.Locations.Where(l => l.Name.ToLower().Contains(model.Search)).ToList();
            }

            model.Props = new Dictionary<string, object>();
            switch (model.SortOrder)
            {
                case "address_desc":
                    model.Locations = model.Locations.OrderByDescending(l => l.Address).ToList();
                    break;
                case "address_asc":
                    model.Locations = model.Locations.OrderBy(l => l.Address).ToList();
                    break;
                case "name_desc":
                    model.Locations = model.Locations.OrderByDescending(l => l.Name).ToList();
                    break;
                case "name_asc":
                default:
                    model.Locations = model.Locations.OrderBy(l => l.Name).ToList();
                    break;
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            Location location;

            if (!id.HasValue)
            {
                location = new Location();
            }
            else
            {
                location = locationRep.GetByID(id.Value);
                if (location==null)
                {
                    return RedirectToAction("List");
                }
            }
            LocationsEditVM model = new LocationsEditVM();
            model.ID = location.ID;
            model.Name = location.Name;
            model.Address = location.Address;
            model.Floor = location.Floor;
            model.RoomNumber = location.RoomNumber;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            LocationsEditVM model = new LocationsEditVM();
            TryUpdateModel(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Location location;
            if (model.ID==0)
            {
                location = new Location();
            }
            else
            {
                location = locationRep.GetByID(model.ID);
                if (location==null)
                {
                    return RedirectToAction("List");
                }
            }
            location.ID = model.ID;
            location.Name = model.Name;
            location.Address = model.Address;
            location.Floor = model.Floor;
            location.RoomNumber = model.RoomNumber;

            locationRep.Save(location);

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            locationRep.Delete(id);

            return RedirectToAction("List");
        }
    }
}