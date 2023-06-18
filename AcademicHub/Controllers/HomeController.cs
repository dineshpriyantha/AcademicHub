using AcademicHub.Models;
using BusinessLogic.Services;
using DataAccessLayer;
using DataAccessLayer.Model;
using DataAccessLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AcademicHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CategoryManager _categoryManager;
        private readonly AcademicHubDbContext _context;
        private readonly SubCategoryManager _subCategoryManager;
        private readonly PostManager _post;

        public HomeController(ILogger<HomeController> logger, AcademicHubDbContext context)
        {
            _logger = logger;
            _context = context;
            _categoryManager = new CategoryManager(_context);
            _subCategoryManager= new SubCategoryManager(_context);
            _post = new PostManager(_context);
        }

        public IActionResult Index()
        {
            
            // Retrieve the list of categories from your data source
            List<Category> categories = _categoryManager.GetCategories().Value;
            List<Subcategory> subCategories = _subCategoryManager.GetAllSubCategories().Value;
            List<Post> posts = _post.GetAllPosts().Value;

            var viewModel = new CategorySubcategoryViewModel()
            {
                Category = categories,
                Subcategory = subCategories,
                Posts = posts
            };

            // Populate the Subcategory property if needed
            // viewModel.Subcategory = ...

            return View(viewModel);
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