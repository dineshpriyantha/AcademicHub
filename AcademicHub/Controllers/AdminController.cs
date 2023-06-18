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
        private readonly CategoryManager _CategoryManager;
        private readonly SubCategoryManager _subCategoryManager;

        public AdminController(AcademicHubDbContext context)
        {
            _context = context;
            _CategoryManager = new CategoryManager(_context);
            _subCategoryManager = new SubCategoryManager(_context);
        }

        public IActionResult Index()
        {
            var categorylist = _CategoryManager.GetCategories();

            if(categorylist.Value != null)
            {
                var categories = categorylist.Value;
                return View(categories);
            }
            else
            {
                var errorMassage = categorylist.ReturnMessage;
                return View("Error", errorMassage);
            }
        }

        public IActionResult CategoryIndex()
        {
            var categorylist = _CategoryManager.GetCategories();

            if (categorylist.Value != null)
            {
                var categories = categorylist.Value;
                return View(categories);
            }
            else
            {
                var errorMassage = categorylist.ReturnMessage;
                return View("Error", errorMassage);
            }
        }

        public IActionResult SubCategoryIndex()
        {
            var subCategorylist = _subCategoryManager.GetAllSubCategories();

            if (subCategorylist.Value != null)
            {
                var categories = subCategorylist.Value;
                return View(categories);
            }
            else
            {
                var errorMassage = subCategorylist.ReturnMessage;
                return View("Error", errorMassage);
            }
        }

        public IActionResult Category()
        {
            var enumData = from DisplayArea e in Enum.GetValues(typeof(DisplayArea))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };

            ViewBag.DisplayArea = new SelectList(enumData, "ID", "Name");
            
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

                var addCategory = _CategoryManager.AddCategory(category);
                ViewBag.message = addCategory.ReturnMessage;
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
                var category = _CategoryManager.GetCategoryById(id);

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
            try
            {
                var enumData = from DisplayArea e in Enum.GetValues(typeof(DisplayArea))
                               select new
                               {
                                   ID = (int)e,
                                   Name = e.ToString()
                               };

                ViewBag.DisplayArea = new SelectList(enumData, "ID", "Name");
                var updateCategory = _CategoryManager.UpdateCategory(category);
                ViewBag.message = updateCategory.ReturnMessage;
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
                Result<bool> category = _CategoryManager.RemoveCategory(id);
                if (category.Value)
                {
                    ViewBag.Category = category.ReturnMessage;
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(category.ReturnMessage);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public IActionResult SubCategory()
        {
            try
            {
                var categorylist = _CategoryManager.GetCategories();
                var enumData = from Category e in categorylist.Value.ToList()
                               select new
                               {
                                   ID = (int)e.CId,
                                   Name = e.Name.ToString()
                               };

                ViewBag.Category = new SelectList(enumData, "ID", "Name");
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                ViewBag.Success = false;
            }          

            return View();
        }

        [HttpPost]
        public IActionResult SubCategory(Subcategory subcategory)
        {
            try
            {
                var categorylist = _CategoryManager.GetCategories();
                var enumData = from Category e in categorylist.Value.ToList()
                               select new
                               {
                                   ID = (int)e.CId,
                                   Name = e.Name.ToString()
                               };

                ViewBag.Category = new SelectList(enumData, "ID", "Name");

                var addCategory = _subCategoryManager.AddSubCategory(subcategory);
                ViewBag.message = addCategory.ReturnMessage;
                ViewBag.Success = addCategory.Value;
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                ViewBag.Success = false;
            }

            return View(subcategory);
        }

        public IActionResult UpdateSubCategory(int id)
        {
            try
            {
                // Retrive the category from the database using the provided id
                var subCategory = _subCategoryManager.GetSubCategoryById(id);

                if (subCategory != null)
                {
                    var categorylist = _CategoryManager.GetCategories();
                    var enumData = from Category e in categorylist.Value.ToList()
                                   select new
                                   {
                                       ID = (int)e.CId,
                                       Name = e.Name.ToString()
                                   };

                    ViewBag.Category = new SelectList(enumData, "ID", "Name");

                    return View(subCategory.Value);
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
        public IActionResult UpdateSubCategory(Subcategory subCategory)
        {
            try
            {
                var categorylist = _CategoryManager.GetCategories();
                var enumData = from Category e in categorylist.Value.ToList()
                               select new
                               {
                                   ID = (int)e.CId,
                                   Name = e.Name.ToString()
                               };

                ViewBag.Category = new SelectList(enumData, "ID", "Name");

                var updateCategory = _subCategoryManager.UpdateSubCategory(subCategory);
                ViewBag.message = updateCategory.ReturnMessage;
                ViewBag.success = updateCategory.Value;
            }
            catch (Exception ex)
            {
                return View("Error ", ex.Message);
            }
            return View(subCategory);
        }

        public IActionResult DeleteSubcategory(int id)
        {
            try
            {
                Result<bool> subCategory = _subCategoryManager.RemoveSubCategory(id);
                if (subCategory.Value)
                {
                    ViewBag.Category = subCategory.ReturnMessage;
                    return RedirectToAction("SubCategoryIndex");
                }
                else
                {
                    return View(subCategory.ReturnMessage);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public IActionResult Post()
        {
            return View();
        }

    }
}
