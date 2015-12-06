using CHSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHSystem.Filters
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AuthenticationService.LoggedUser == null)
            {
                filterContext.HttpContext.Response.Redirect("~/Account/Login/?RedirectUrl=" + filterContext.HttpContext.Request.Url);
                filterContext.Result = new EmptyResult();
            }
        }
    }
}