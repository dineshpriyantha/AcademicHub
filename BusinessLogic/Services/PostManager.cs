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
    public class PostManager : IPostManager
    {
        private readonly AcademicHubDbContext _context;

        public PostManager(AcademicHubDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPost(Post post)
        {
            if(post == null) return false;

            // Check if a hub with the same title
            var existingTitle = await _context.Posts.FirstOrDefaultAsync(b => b.Title == post.Title);

            if(existingTitle != null) { return false; }

            _context.Posts.Add(post);
            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetPostById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PId == id);
            if(post == null)
            {
                throw new ArgumentException("The specified post does not exist.", nameof(post));
            }

            return post;
        }

        public async Task RemovePost(int? id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(s => s.PId == id);

            if(post == null) return;

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            if(post == null) return false;

            var existingPost = await _context.Posts.FirstOrDefaultAsync(s => s.PId == post.PId);

            if(existingPost == null) return false;

            existingPost.Date = DateTime.Now;
            existingPost.Title = post.Title;
            existingPost.Content = post.Content;

            _context.Posts.Update(post);
            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }
    }
}
