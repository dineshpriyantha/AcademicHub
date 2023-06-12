using DataAccessLayer;
using DataAccessLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessLogic.Services
{
    public class CategoryManager : ICategoryManager
    {
        private readonly AcademicHubDbContext _context;

        public CategoryManager(AcademicHubDbContext context)
        {
            _context = context;
        }

        public Result<bool> AddCategory(Category category)
        {
            if (category == null) return new Result<bool> { Value = false , ErrorMessage = "Invalid categoty"};

            try
            {                
                string sql = "EXEC sp_AHub_AddCategory @Name, @Date, @DisplayAreaId, @CRank, @Result OUTPUT, @ErrorMessage OUTPUT";
                
                var name = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = category.Name };
                var date = new SqlParameter("@Date", SqlDbType.DateTime) { Value = DateTime.Now };
                var displayAreaId = new SqlParameter("@DisplayAreaId", SqlDbType.Int) { Value = category.DisplayArea };
                var crank = new SqlParameter("@CRank", SqlDbType.Int) { Value = category.CRank };
                var result = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var errorMsg = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
               
                _context.Database.ExecuteSqlRaw(sql, name, date, displayAreaId, crank, result, errorMsg);

                var resultFromDb = Convert.ToBoolean(result.Value);
                var errorFromDb = errorMsg.Value?.ToString();

                return new Result<bool> { Value = resultFromDb , ErrorMessage = errorFromDb };

            }
            catch (TaskCanceledException ex)
            {                
                // Handle the cancellation by returning an appropriate result or error message
                return new Result<bool> { Value = false, ErrorMessage = "Operation canceled. Please try again." };
            }
            catch (Exception ex)
            {
                return new Result<bool> { Value = false, ErrorMessage = $"Error : {ex.Message}" };
            }
        }

        //public Result<List<Category>> GetCategories()
        //{
        //    var categories = new List<Category>();
                
                
        //    return new Result<categories> { Value = true, ErrorMessage = "Success"};
        //}

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

        public Result<bool> RemoveCategory(int? id)
        {
            if (id == null) return new Result<bool> { Value = false, ErrorMessage = "Invalid Category" };

            try
            {
                string sql = "EXEC sp_AHub_DeleteCategory @CId";
                var cId = new SqlParameter("@CId", SqlDbType.Int) { Value = id };
                var result = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var errorMsg = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

                _context.Database.ExecuteSqlRaw(sql, cId, result, errorMsg);
                var resultFromDb = Convert.ToBoolean(result.Value);
                var errorFromDb = errorMsg.Value?.ToString();

                return new Result<bool> { Value = resultFromDb, ErrorMessage = errorFromDb };
            }
            catch (Exception ex)
            {
                return new Result<bool> { Value = false, ErrorMessage = $"Error {ex.Message}" };
            }
        }

        public Result<bool> UpdateCategory(Category category)
        {
            if (category == null) return new Result<bool> { Value = false, ErrorMessage = "Invalid categoty" };

            try
            {
                string sql = "EXEC sp_AHub_UpdateCategory @CId, @Name, @Date, @DisplayAreaId, @CRank, @Result OUTPUT, @ErrorMessage OUTPUT";
                var cId = new SqlParameter("@CId", SqlDbType.Int) { Value = category.CId };
                var name = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = category.Name };
                var date = new SqlParameter("@Date", SqlDbType.DateTime) { Value = DateTime.Now };
                var displayAreaId = new SqlParameter("@DisplayAreaId", SqlDbType.Int) { Value = category.DisplayArea };
                var crank = new SqlParameter("@CRank", SqlDbType.Int) { Value = category.CRank };
                var result = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var errorMsg = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

                _context.Database.ExecuteSqlRaw(sql, cId, name, date, displayAreaId, crank, result, errorMsg);

                var resultFromDb = Convert.ToBoolean(result.Value);
                var errorFromDb = errorMsg.Value?.ToString();

                return new Result<bool> { Value = resultFromDb, ErrorMessage = errorFromDb };
            }
            catch (Exception ex)
            {
                return new Result<bool> { Value = false, ErrorMessage = $"Error {ex.Message}" };
            }
        }
    }
}
