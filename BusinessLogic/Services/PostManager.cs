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
    public class PostManager : IPostManager
    {
        private readonly AcademicHubDbContext _context;

        public PostManager(AcademicHubDbContext context)
        {
            _context = context;
        }

        public Result<bool> AddPost(Post post)
        {
            if (post == null) return new Result<bool> { Value = false, ReturnMessage = "Invalid categoty" };

            try
            {
                string sql = "EXEC sp_AHub_AddPost @Title, @Date, @Content, @CategoryId, @SubcategoryId, @Result OUTPUT, @ReturnMessage OUTPUT";

                var title = new SqlParameter("@Title", SqlDbType.NVarChar) { Value = post.Title };
                var date = new SqlParameter("@Date", SqlDbType.DateTime) { Value = DateTime.Now };
                var content = new SqlParameter("@Content", SqlDbType.NVarChar) { Value = post.Content };
                var categoryId = new SqlParameter("@CategoryId", SqlDbType.Int) { Value = post.CategoryId };
                var subcategoryId = new SqlParameter("@SubcategoryId", SqlDbType.Int) { Value = post.SubcategoryId };
                var result = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var returnMsg = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

                _context.Database.ExecuteSqlRaw(sql, title, date, content, categoryId, subcategoryId, result, returnMsg);

                var resultFromDb = Convert.ToBoolean(result.Value);
                var returnFromDb = returnMsg.Value?.ToString();

                return new Result<bool> { Value = resultFromDb, ReturnMessage = returnFromDb! };

            }
            catch (TaskCanceledException ex)
            {
                // Handle the cancellation by returning an appropriate result or error message
                return new Result<bool> { Value = false, ReturnMessage = $"Operation canceled. Please try again. {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new Result<bool> { Value = false, ReturnMessage = $"Error : {ex.Message}" };
            }
        }

        public Result<List<Post>> GetAllPosts()
        {
            string sql = "EXEC sp_AHub_GetAllPost @Result OUTPUT, @ReturnMessage OUTPUT";
            var result = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var errorMsg = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

            try
            {
                var postList = _context.Posts.FromSqlRaw<Post>(sql, result, errorMsg).ToList();
                var resultFromDb = Convert.ToBoolean(result.Value);
                var errorFromDb = errorMsg.Value?.ToString();

                return new Result<List<Post>> { Value = postList, ReturnMessage = errorFromDb! };
            }
            catch (Exception ex)
            {
                return new Result<List<Post>> { Value = null!, ReturnMessage = $"Error {ex.Message}" };
            }
        }

        public Result<Post> GetPostById(int? id)
        {
            throw new NotImplementedException();
        }

        public Result<bool> RemovePost(int? id)
        {
            throw new NotImplementedException();
        }

        public Result<bool> UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
