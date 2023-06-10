using DataAccessLayer;
using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SubCategoryManager : ISubCategoryManager
    {
        private readonly AcademicHubDbContext _context;

        public SubCategoryManager(AcademicHubDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddSubCategory(Subcategory subcategory)
        {
            if (subcategory == null) return false;

            // check if a hub with the same category
            var existingCategory = await _context.Subcategories.FirstOrDefaultAsync(x => x.Name == subcategory.Name);

            if (existingCategory != null) return false;

            _context.Subcategories.Add(subcategory);
            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Subcategory>> GetAllSubCategories()
        {
            return await _context.Subcategories.ToListAsync();
        }

        public async Task<Subcategory> GetSubCategoryById(int? id)
        {
            if(id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var subCategory = await _context.Subcategories.FirstOrDefaultAsync(x => x.SId == id);

            if(subCategory == null)
            {
                throw new ArgumentException("The specified sub category does not exist", nameof(subCategory));
            }
            return subCategory;
        }

        public async Task RemoveSubCategory(int? id)
        {
            var subCategory = await _context.Subcategories.FirstOrDefaultAsync(x => x.SId == id);

            if (subCategory == null) return;

            _context.Subcategories.Remove(subCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateSubCategory(Subcategory subcategory)
        {
            if (subcategory == null) return false;

            var existingCategory = await _context.Subcategories.FirstOrDefaultAsync(x => x.SId == subcategory.SId);

            if (existingCategory == null) return false;

            existingCategory.Date = DateTime.Now;

            _context.Subcategories.Update(subcategory);
            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }
    }
}
