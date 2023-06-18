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
        Result<bool> AddPost(Post post);
        Result<bool> RemovePost(int? id);
        Result<bool> UpdatePost(Post post);
        Result<Post> GetPostById(int? id);
        Result<List<Post>> GetAllPosts();
    }
}
