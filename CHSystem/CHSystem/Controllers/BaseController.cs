using CHSystem.Filters;
using System.Web.Mvc;

namespace CHSystem.Controllers
{
    [AuthenticationFilter]
    public class BaseController : Controller
    {
    }
}