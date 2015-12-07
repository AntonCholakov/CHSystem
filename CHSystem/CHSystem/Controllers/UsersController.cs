using CHSystem.Models;
using CHSystem.Repositories;
using CHSystem.ViewModels.Users;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHSystem.Controllers
{
    public class UsersController : BaseController
    {
        static UnitOfWork unitOfWork = new UnitOfWork();
        UserRepository userRep = new UserRepository(unitOfWork);

        public ActionResult List(string currentFilter, int? page)
        {
            UsersListVM model = new UsersListVM();
            TryUpdateModel(model);

            if (!String.IsNullOrEmpty(model.Search))
            {
                page = 1;
            }
            else
            {
                model.Search = currentFilter;
            }

            model.Users = userRep.GetAll();

            if (!String.IsNullOrEmpty(model.Search))
            {
                model.Search = model.Search.ToLower();
                model.Users = model.Users.Where(u => u.Username.ToLower().Contains(model.Search) ||
                                                    u.FirstName.ToLower().Contains(model.Search) ||
                                                    u.LastName.ToLower().Contains(model.Search)).ToList();
            }

            model.Props = new Dictionary<string, object>();
            switch (model.SortOrder)
            {
                case "username_desc":
                    model.Users = model.Users.OrderByDescending(u => u.Username).ToList();
                    break;
                case "fname_asc":
                    model.Users = model.Users.OrderBy(u => u.FirstName).ToList();
                    break;
                case "fname_desc":
                    model.Users = model.Users.OrderByDescending(u => u.FirstName).ToList();
                    break;
                case "lname_asc":
                    model.Users = model.Users.OrderBy(u => u.LastName).ToList();
                    break;
                case "lname_desc":
                    model.Users = model.Users.OrderByDescending(u => u.LastName).ToList();
                    break;
                case "email_asc":
                    model.Users = model.Users.OrderBy(u => u.Email).ToList();
                    break;
                case "email_desc":
                    model.Users = model.Users.OrderByDescending(u => u.Email).ToList();
                    break;
                case "username_asc":
                default:
                    model.Users = model.Users.OrderBy(u => u.Username).ToList();
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            model.UsersPagedList = model.Users.ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            User user;
            if (!id.HasValue)
            {
                user = new User();
            }
            else
            {
                user = userRep.GetByID(id.Value);
                if (user==null)
                {
                    return RedirectToAction("List");
                }
            }

            UsersEditVM model = new UsersEditVM();
            model.ID = user.ID;
            model.Username = user.Username;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.Groups = PopulateAssignedGroups(user);
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            UsersEditVM model = new UsersEditVM();
            TryUpdateModel(model);

            string selectedGroups = Request.Form["assignedGroups"];
            string[] assignedGroups;
            if (selectedGroups == null)
            {
                assignedGroups = new string[0];
            }
            else
            {
                assignedGroups = selectedGroups.Split(',');
            }

            User user;
            if (model.ID == 0)
            {
                user = new User();
            }
            else
            {
                user = userRep.GetByID(model.ID);
                if (user == null)
                {
                    return RedirectToAction("List");
                }
            }

            if (!ModelState.IsValid)
            {
                model.Groups = PopulateAssignedGroups(user);

                return View(model);
            }


            user.ID = model.ID;
            user.Username = model.Username;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            UpdateUserGroups(user, assignedGroups);

            userRep.Save(user);

            return RedirectToAction("List");
        }
        
        public ActionResult Delete(int id)
        {
            userRep.Delete(id);

            return RedirectToAction("List");
        }

        public List<AssignedGroupsVM> PopulateAssignedGroups(User user)
        {
            List<AssignedGroupsVM> assignedGroups = new List<AssignedGroupsVM>();
            List<Group> groups = new GroupRepository(unitOfWork).GetAll();

            if (user.Groups==null)
            {
                user.Groups = new List<Group>();
            }

            foreach (var g in groups)
            {
                assignedGroups.Add(new AssignedGroupsVM
                {
                    ID = g.ID,
                    Name = g.Name,
                    IsAssigned = user.Groups.Contains(g)
                });
            }

            return assignedGroups;
        }
        public void UpdateUserGroups(User user, string[] assignedGroups)
        {
            if (assignedGroups==null)
            {
                return;
            }

            if (user.Groups==null)
            {
                user.Groups = new List<Group>();
            }

            var assignedGroupsHS = new HashSet<string>(assignedGroups);
            var userGroups = new HashSet<int>(user.Groups.Select(g => g.ID));
            foreach (var group in new GroupRepository(unitOfWork).GetAll())
            {
                if (assignedGroupsHS.Contains(group.ID.ToString()))
                {
                    if (!userGroups.Contains(group.ID))
                    {
                        user.Groups.Add(group);
                    }
                }
                else
                {
                    if (userGroups.Contains(group.ID))
                    {
                        user.Groups.Remove(group);
                    }
                }
            }
        }
    }
}