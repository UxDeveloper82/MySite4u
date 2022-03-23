using Microsoft.EntityFrameworkCore;
using MySite4u.Data;
using MySite4u.Helpers;
using MySite4u.Interfaces;
using MySite4u.Models;
using MySite4u.Models.Comments;
using MySite4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySite4u.Services
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
        }
        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public async Task<PagedList<Post>> GetPostsAsync(PostParams postParams)
        {
            var query = _context.Posts.AsQueryable();

            query = postParams.OrderBy switch
            {
                "created" => query.OrderByDescending(u => u.Created),
                _ => query.OrderByDescending(u => u.Created)
            };

            return await PagedList<Post>.CreateAsync(
                query.AsNoTracking(), postParams.PageNumber, postParams.PageSize);
        }

        public Post GetPost(int id)
        {
            return _context.Posts
                .Include(p => p.MainComments)
                         .ThenInclude(m => m.SubComments)
                .FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
            _context.Posts.Remove(GetPost(id));
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }

        public async Task<bool> SaveChangeAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public void AddSubComment(SubComment comment)
        {
            _context.SubComments.Add(comment);
        }

    }
}
