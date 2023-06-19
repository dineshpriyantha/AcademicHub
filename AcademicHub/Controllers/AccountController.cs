using Microsoft.AspNetCore.Mvc;

namespace AcademicHub.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
