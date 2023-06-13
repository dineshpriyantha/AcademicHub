using AcademicHub.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AcademicHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var categiesresult = 
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult ProcessEditorContent(string editor1)
        {
            // Use the 'editor1' parameter to access the editor content
            // Perform any necessary processing with the editor content
            // Return the appropriate response

            var htmlContent = new HtmlContentModel { HtmlContent= editor1 };

            return View(htmlContent);
        }

    }
}