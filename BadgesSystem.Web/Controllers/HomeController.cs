using BadgesSystem.Web.Infrastructure;
using System.Web.Mvc;

namespace BadgesSystem.Web.Controllers
{
    [CustomAuthorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}