using Microsoft.AspNetCore.Mvc;
namespace INVEST.SITE.Controllers
{
    public class HomeController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
