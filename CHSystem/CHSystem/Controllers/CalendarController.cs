using System.Web.Mvc;

namespace CHSystem.Controllers
{
    public class CalendarController : BaseController
    {
        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult CalendarGoogle()
        {
            return View();
        }
    }
}