using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface ISubCategoryManager
    {
        Task<bool> AddSubCategory(Subcategory subcategory);
        Task RemoveSubCategory(int? id);
        Task<bool> UpdateSubCategory(Subcategory subcategory);
        Task<Subcategory> GetSubCategoryById(int? id);
        Task<IEnumerable<Subcategory>> GetAllSubCategories();
    }
}
