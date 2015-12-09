using CHSystem.Models;
using CHSystem.Repositories;
using CHSystem.Services;
using CHSystem.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CHSystem.Controllers
{
    public class EventsController : BaseController
    {
        static UnitOfWork unitOfWork = new UnitOfWork();
        EventRepository eventRep = new EventRepository(unitOfWork);

        public ActionResult List()
        {
            EventsListVM model = new EventsListVM();
            TryUpdateModel(model);
            model.Events = eventRep.GetAll();

            if (!String.IsNullOrEmpty(model.Search))
            {
                model.Search = model.Search.ToLower();
                model.Events = model.Events.Where(e => e.Name.ToLower().Contains(model.Search)).ToList();
            }

            model.Props = new Dictionary<string, object>();
            switch (model.SortOrder)
            {
                case "name_desc":
                    model.Events = model.Events.OrderByDescending(e => e.Name).ToList();
                    break;
                case "hall_asc":
                    model.Events = model.Events.OrderBy(e => e.Hall.Name).ToList();
                    break;
                case "hall_desc":
                    model.Events = model.Events.OrderByDescending(e => e.Hall.Name).ToList();
                    break; ;
                case "start_asc":
                    model.Events = model.Events.OrderBy(e => e.Start).ToList();
                    break;
                case "start_desc":
                    model.Events = model.Events.OrderByDescending(e => e.Start).ToList();
                    break;
                case "end_asc":
                    model.Events = model.Events.OrderBy(e => e.End).ToList();
                    break;
                case "end_desc":
                    model.Events = model.Events.OrderByDescending(e => e.End).ToList();
                    break;
                case "name_asc":
                default:
                    model.Events = model.Events.OrderBy(e => e.Name).ToList();
                    break;
            }

            PagingService.Prepare(model, ControllerContext, model.Events);

            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            Event CHevent;

            if (!id.HasValue)
            {
                CHevent = new Event();
            }
            else
            {
                CHevent = eventRep.GetByID(id.Value);
                if (CHevent == null)
                {
                    return RedirectToAction("List");
                }
            }

            EventsEditVM model = new EventsEditVM();
            model.ID = CHevent.ID;
            model.Name = CHevent.Name;
            model.Date = CHevent.Date;
            model.Start = CHevent.Start;
            model.End = CHevent.End;
            model.HallID = CHevent.HallID;
            model.Halls = GetHalls();

            model.Users = PopulateAssignedUsers(CHevent);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            EventsEditVM model = new EventsEditVM();
            TryUpdateModel(model);

            string selectedUsers = Request.Form["assignedUsers"];
            string[] assignedUsers;
            if (selectedUsers == null)
            {
                assignedUsers = new string[0];
            }
            else
            {
                assignedUsers = selectedUsers.Split(',');
            }
            
            Event CHevent;
            if (model.ID == 0)
            {
                CHevent = new Event();
            }
            else
            {
                CHevent = eventRep.GetByID(model.ID);
                if (CHevent == null)
                {
                    return RedirectToAction("List");
                }
            }

            if (!ModelState.IsValid)
            {
                model.Users = PopulateAssignedUsers(CHevent);
                model.Halls = GetHalls();

                return View(model);
            }
            
            CHevent.ID = model.ID;
            CHevent.Name = model.Name;
            CHevent.Date = model.Date;
            CHevent.Start = model.Start;
            CHevent.End = model.End;
            CHevent.HallID = model.HallID;
            UpdateEventUsers(CHevent, assignedUsers);

            eventRep.Save(CHevent);
            unitOfWork.Commit();

            System.Threading.Tasks.Task.Run(() => EmailService.SendEmail(CHevent.Users));

            return RedirectToAction("List");
        }

        private IEnumerable<SelectListItem> GetHalls()
        {
            var halls = new HallRepository().GetAll()
                .Select(h => new SelectListItem
                {
                    Value = h.ID.ToString(),
                    Text = h.Name
                });

            return new SelectList(halls, "Value", "Text");
        }

        public List<AssignedUsersVM> PopulateAssignedUsers(Event CHevent)
        {
            List<AssignedUsersVM> assignedUsers = new List<AssignedUsersVM>();
            List<User> users = new UserRepository(unitOfWork).GetAll();

            if (CHevent.Users == null)
            {
                CHevent.Users = new List<User>();
            }

            foreach (var u in users)
            {
                assignedUsers.Add(new AssignedUsersVM
                {
                    ID = u.ID,
                    Name = u.FirstName + " " + u.LastName,
                    IsAssigned = CHevent.Users.Contains(u)
                });
            }

            return assignedUsers;
        }
        public void UpdateEventUsers(Event CHevent, string[] assignedUsers)
        {
            if (assignedUsers == null)
            {
                return;
            }

            if (CHevent.Users == null)
            {
                CHevent.Users = new List<User>();
            }

            var assignedUsersHS = new HashSet<string>(assignedUsers);
            var eventUsers = new HashSet<int>(CHevent.Users.Select(g => g.ID));
            foreach (var user in new UserRepository(unitOfWork).GetAll())
            {
                if (assignedUsersHS.Contains(user.ID.ToString()))
                {
                    if (!eventUsers.Contains(user.ID))
                    {
                        CHevent.Users.Add(user);
                    }
                }
                else
                {
                    if (eventUsers.Contains(user.ID))
                    {
                        CHevent.Users.Remove(user);
                    }
                }
            }
        }
    }
}