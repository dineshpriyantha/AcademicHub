using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface ICategoryManager
    {
        Result<bool> AddCategory(Category category);
        Result<bool> UpdateCategory(Category category);
        Result<bool> RemoveCategory(int? id);
        Task<Category> GetCategoryById(int? id);
        //Result<List<Category>> GetCategories();
    }

    public class Result<T>
    {
        public T Value { get; set; }
        public string ErrorMessage { get; set; }
    }
}
