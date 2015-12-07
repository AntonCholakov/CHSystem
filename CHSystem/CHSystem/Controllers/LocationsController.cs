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
    public class LocationsController : Controller
    {
        LocationRepository locationRep = new LocationRepository();

        public ActionResult List()
        {
            LocationsListVM model = new LocationsListVM();
            model.Locations = locationRep.GetAll();

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