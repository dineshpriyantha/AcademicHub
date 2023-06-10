using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IPostManager
    {
        Task<bool> AddPost(Post post);
        Task RemovePost(int? id);
        Task<bool> UpdatePost(Post post);
        Task<Post> GetPostById(int? id);
        Task<IEnumerable<Post>> GetAllPosts();
    }
}
