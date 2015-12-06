using CHSystem.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHSystem.Controllers
{
    [AuthenticationFilter]
    public class BaseController : Controller
    {
    }
}