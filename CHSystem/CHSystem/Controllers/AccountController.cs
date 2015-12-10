using CHSystem.Services;
using CHSystem.ViewModels.Account;
using System;
using System.Web.Mvc;

namespace CHSystem.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string redirectUrl)
        {
            if (AuthenticationService.LoggedUser != null)
            {
                return RedirectToAction("Index", "Home");
            }

            AccountLoginVM model = new AccountLoginVM();

            if (!String.IsNullOrEmpty(redirectUrl))
            {
                model.RedirectUrl = redirectUrl;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login()
        {
            AccountLoginVM model = new AccountLoginVM();
            TryUpdateModel(model);

            AuthenticationService.AuthenticateUser(model.Username, model.Password);

            if (AuthenticationService.LoggedUser == null)
            {
                ModelState.AddModelError(String.Empty, "Invalid username or password");
                return View(model);
            }

            if (String.IsNullOrEmpty(model.RedirectUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(model.RedirectUrl);
        }

        public ActionResult Logout()
        {
            AuthenticationService.Logout();

            return RedirectToAction("Login");
        }
    }
}