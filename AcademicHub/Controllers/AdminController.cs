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
        private readonly CategoryManager _manager;

        public AdminController(AcademicHubDbContext context)
        {
            _context = context;
            _manager = new CategoryManager(_context);
        }

        public IActionResult Index()
        {
            var categorylist = _manager.GetCategories();

            if(categorylist.Value != null)
            {
                var categories = categorylist.Value;
                return View(categories);
            }
            else
            {
                var errorMassage = categorylist.ErrorMessage;
                return View("Error", errorMassage);
            }
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
                var addCategory = _manager.AddCategory(category);
            }
            catch (Exception ex)
            {
                
            }
            return View(category);
        }
        public IActionResult Update(int id)
        {
            
            try
            {
                // Retrive the category from the database using the provided id
                var category = _manager.GetCategoryById(id);

                if (category != null)
                {
                    var enumData = from DisplayArea e in Enum.GetValues(typeof(DisplayArea))
                                   select new
                                   {
                                       ID = (int)e,
                                       Name = e.ToString()
                                   };
                    ViewBag.DisplayArea = new SelectList(enumData, "ID", "Name");

                    return View(category.Value);
                }
                else
                {
                    return View("Error ", "Category not found.");
                }
            }
            catch (Exception ex)
            {
                return View("Error ", ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Update()
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
                //var addCategory = _manager.AddCategory(category);
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        
        public IActionResult Delete(int id)
        {
            try
            {
                Result<bool> category = _manager.RemoveCategory(id);
                if (category.Value)
                {
                    ViewBag.Category = category.ErrorMessage;
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(category.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
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
