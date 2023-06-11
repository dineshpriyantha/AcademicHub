using BusinessLogic.Services;
using DataAccessLayer;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademicHub.Controllers
{
    public class AdminController : Controller
    {
        private readonly AcademicHubDbContext _context;

        public AdminController(AcademicHubDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Category()
        {
            //var displayArea = Enum.GetNames(typeof(DisplayArea))
            //                      .Select(x => new SelectListItem { Value = x, Text = x}).ToList();

            var enumData = from DisplayArea e in Enum.GetValues(typeof(DisplayArea))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };

            ViewBag.DisplayArea = new SelectList(enumData, "ID", "Name");
            var model = new Category();


            return View();
        }

        [HttpPost]
        public IActionResult Category(Category category)
        {
            var enumData = from DisplayArea e in Enum.GetValues(typeof(DisplayArea))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };

            ViewBag.DisplayArea = new SelectList(enumData, "ID", "Name");

            //if (!ModelState.IsValid) { return View(category); }

            try
            {
                CategoryManager manager = new(_context);
                var addCategory = manager.AddCategory(category);
            }
            catch (Exception ex)
            {
                
            }
            return View(category);
        }

        public IActionResult SubCategory()
        {
            return View();
        }

        public IActionResult Post()
        {
            return View();
        }


    }
}
