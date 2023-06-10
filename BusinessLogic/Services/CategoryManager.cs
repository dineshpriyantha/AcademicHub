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
    public class CategoryManager : ICategoryManager
    {
        private readonly AcademicHubDbContext _context;

        public CategoryManager(AcademicHubDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCategory(Category category)
        {
            if (category == null) return false;

            // check if a hub with the same category
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Name == category.Name);

            if (existingCategory != null) return false;

            _context.Categories.Add(category);
            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int? id)
        {
            if(id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.CId == id);

            if(category == null)
            {
                throw new ArgumentException("The specified post does not exist.", nameof(category));
            }

            return category;
        }

        public async Task RemoveCategory(int? id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.CId == id);

            if (category == null) return;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            if (category == null) return false;

            var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CId == category.CId);

            if (existingCategory == null) return false;

            existingCategory.CRank = category.CRank;
            existingCategory.DisplayArea = category.DisplayArea;
            existingCategory.Date = DateTime.Now;

            _context.Categories.Update(category);
            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }
    }
}
