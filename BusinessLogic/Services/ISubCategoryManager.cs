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
        Result<bool> AddSubCategory(Subcategory subcategory);
        Result<bool> RemoveSubCategory(int? id);
        Result<bool> UpdateSubCategory(Subcategory subcategory);
        Result<Subcategory> GetSubCategoryById(int? id);
        Result<List<Subcategory>> GetAllSubCategories();
    }
}
