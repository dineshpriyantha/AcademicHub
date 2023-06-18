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

namespace BusinessLogic.Services
{
    public class SubCategoryManager : ISubCategoryManager
    {
        private readonly AcademicHubDbContext _context;

        public SubCategoryManager(AcademicHubDbContext context)
        {
            _context = context;
        }

        public Result<bool> AddSubCategory(Subcategory subcategory)
        {
            if (subcategory == null) return new Result<bool> { Value = false, ReturnMessage = "Invalid categoty" };

            try
            {
                string sql = "EXEC sp_AHub_AddSubCategory @Name, @Date, @CategoryId, @SRank, @Result OUTPUT, @ReturnMessage OUTPUT";

                var name = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = subcategory.Name };
                var date = new SqlParameter("@Date", SqlDbType.DateTime) { Value = DateTime.Now };
                var categoryId = new SqlParameter("@CategoryId", SqlDbType.Int) { Value = subcategory.CategoryId };
                var srank = new SqlParameter("@SRank", SqlDbType.Int) { Value = subcategory.SRank };
                var result = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var returnMsg = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

                _context.Database.ExecuteSqlRaw(sql, name, date, categoryId, srank, result, returnMsg);

                var resultFromDb = Convert.ToBoolean(result.Value);
                var returnFromDb = returnMsg.Value?.ToString();

                return new Result<bool> { Value = resultFromDb, ReturnMessage = returnFromDb };

            }
            catch (TaskCanceledException ex)
            {
                // Handle the cancellation by returning an appropriate result or error message
                return new Result<bool> { Value = false, ReturnMessage = $"Operation canceled. Please try again. : {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new Result<bool> { Value = false, ReturnMessage = $"Error : {ex.Message}" };
            }
        }
        public Result<bool> UpdateSubCategory(Subcategory subcategory)
        {
            if (subcategory == null) return new Result<bool> { Value = false, ReturnMessage = "Invalid Sub Categoty" };

            try
            {
                string sql = "EXEC sp_AHub_UpdateSubCategory @SId, @Name, @Date, @CategoryId, @SRank, @Result OUTPUT, @ReturnMessage OUTPUT";
                var cId = new SqlParameter("@SId", SqlDbType.Int) { Value = subcategory.SId };
                var name = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = subcategory.Name };
                var date = new SqlParameter("@Date", SqlDbType.DateTime) { Value = DateTime.Now };
                var categoryId = new SqlParameter("@CategoryId", SqlDbType.Int) { Value = subcategory.CategoryId };
                var srank = new SqlParameter("@SRank", SqlDbType.Int) { Value = subcategory.SRank };
                var result = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var errorMsg = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

                _context.Database.ExecuteSqlRaw(sql, cId, name, date, categoryId, srank, result, errorMsg);

                var resultFromDb = Convert.ToBoolean(result.Value);
                var errorFromDb = errorMsg.Value?.ToString();

                return new Result<bool> { Value = resultFromDb, ReturnMessage = errorFromDb };
            }
            catch (Exception ex)
            {
                return new Result<bool> { Value = false, ReturnMessage = $"Error {ex.Message}" };
            }
        }

        public Result<List<Subcategory>> GetAllSubCategories()
        {
            string sql = "EXEC sp_AHub_GetSubCategory @Result OUTPUT, @ReturnMessage OUTPUT";
            var result = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var returnMsg = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

            try
            {
                var subCategoryList = _context.Subcategories.FromSqlRaw<Subcategory>(sql, result, returnMsg).ToList();
                var resultFromDb = Convert.ToBoolean(result.Value);
                var returnFromDb = returnMsg.Value?.ToString();

                return new Result<List<Subcategory>> { Value = subCategoryList, ReturnMessage = returnFromDb! };
            }
            catch (Exception ex)
            {
                return new Result<List<Subcategory>> { Value = null!, ReturnMessage = $"Error {ex.Message}" };
            }
        }

        public Result<Subcategory> GetSubCategoryById(int? id)
        {
            if (id == null) return new Result<Subcategory> { Value = null!, ReturnMessage = "Invalid Category" };
            try
            {
                string sql = "EXEC sp_AHub_GetSubCategoryById @SId, @Result OUTPUT, @ReturnMessage OUTPUT";
                var sId = new SqlParameter("@SId", SqlDbType.Int) { Value = id };
                var result = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var errorMsg = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

                var subCategory = _context.Subcategories.FromSqlRaw<Subcategory>(sql, sId, result, errorMsg).AsEnumerable().FirstOrDefault();
                var resultFromDb = Convert.ToBoolean(result.Value);
                var returnFromDb = errorMsg.Value?.ToString();

                return new Result<Subcategory> { Value = subCategory!, ReturnMessage = returnFromDb! };
            }
            catch (Exception ex)
            {
                return new Result<Subcategory> { Value = null!, ReturnMessage = $"Error {ex.Message}" };
            }
        }

        public Result<bool> RemoveSubCategory(int? id)
        {
            if (id == null) return new Result<bool> { Value = false, ReturnMessage = "Invalid Category" };

            try
            {
                string sql = "EXEC sp_AHub_DeleteSubCategory @SId, @Result OUTPUT, @ReturnMessage OUTPUT";
                var sId = new SqlParameter("@SId", SqlDbType.Int) { Value = id };
                var result = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var returnMsg = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

                _context.Database.ExecuteSqlRaw(sql, sId, result, returnMsg);
                var resultFromDb = Convert.ToBoolean(result.Value);
                var returnFromDb = returnMsg.Value?.ToString();

                return new Result<bool> { Value = resultFromDb, ReturnMessage = returnFromDb! };
            }
            catch (Exception ex)
            {
                return new Result<bool> { Value = false, ReturnMessage = $"Error {ex.Message}" };
            }
        }

    }
}
