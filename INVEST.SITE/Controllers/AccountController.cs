using Microsoft.AspNetCore.Mvc;
namespace INVEST.SITE.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
