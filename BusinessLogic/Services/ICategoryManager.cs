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
        Task<Result<bool>> AddCategory(Category category);
        Task<bool> UpdateCategory(Category category);
        Task RemoveCategory(int? id);
        Task<Category> GetCategoryById(int? id);
        Task<IEnumerable<Category>> GetCategories();
    }

    public class Result<T>
    {
        public T Value { get; set; }
        public string ErrorMessage { get; set; }
    }
}
