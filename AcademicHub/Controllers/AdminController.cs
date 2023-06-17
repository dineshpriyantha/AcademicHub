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
            try
            {
                var enumData = from DisplayArea e in Enum.GetValues(typeof(DisplayArea))
                               select new
                               {
                                   ID = (int)e,
                                   Name = e.ToString()
                               };

                ViewBag.DisplayArea = new SelectList(enumData, "ID", "Name");

                var addCategory = _manager.AddCategory(category);
                ViewBag.message = addCategory.ErrorMessage;
                ViewBag.Success = addCategory.Value;
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                ViewBag.Success = false;
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
                ViewBag.message = "Category not found.";
                ViewBag.Success = false;
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                ViewBag.Success = false;
                
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            //if (!ModelState.IsValid) { return View(category); }

            try
            {
                var enumData = from DisplayArea e in Enum.GetValues(typeof(DisplayArea))
                               select new
                               {
                                   ID = (int)e,
                                   Name = e.ToString()
                               };

                ViewBag.DisplayArea = new SelectList(enumData, "ID", "Name");
                var updateCategory = _manager.UpdateCategory(category);
                ViewBag.message = updateCategory.ErrorMessage;
                ViewBag.success = updateCategory.Value;
            }
            catch (Exception ex)
            {
                return View("Error ", ex.Message);
            }
            return View(category);
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

        [HttpPost]
        public IActionResult SubCategory(Subcategory subcategory)
        {
            return View();
        }


        public IActionResult Post()
        {
            return View();
        }


    }
}
