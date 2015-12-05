using CHSystem.Models;
using CHSystem.Repositories;
using CHSystem.ViewModels.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHSystem.Controllers
{
    public class GroupsController : Controller
    {
        GroupRepository groupRep = new GroupRepository();

        public ActionResult List()
        {
            GroupsListVM model = new GroupsListVM();
            model.Groups = groupRep.GetAll(); 

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            Group group;

            if (!id.HasValue)
            {
                group = new Group();
            }
            else
            {
                group = groupRep.GetByID(id.Value);
                if (group == null)
                {
                    return RedirectToAction("List");
                }
            }

            GroupsEditVM model = new GroupsEditVM();
            model.ID = group.ID;
            model.Name = group.Name;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            GroupsEditVM model = new GroupsEditVM();
            TryUpdateModel(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Group group;
            if (model.ID == 0)
            {
                group = new Group();
            }
            else
            {
                group = groupRep.GetByID(model.ID);
                if (group == null)
                {
                    return RedirectToAction("List");
                }
            }

            group.Name = model.Name;

            groupRep.Save(group);

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            groupRep.Delete(id);

            return RedirectToAction("List");
        }
    }
}