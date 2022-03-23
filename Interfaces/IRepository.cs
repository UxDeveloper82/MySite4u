using MySite4u.Helpers;
using MySite4u.Models;
using MySite4u.Models.Comments;
using MySite4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySite4u.Interfaces
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        Task<PagedList<Post>> GetPostsAsync(PostParams postParams);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(int id);
        void AddSubComment(SubComment comment);
        Task<bool> SaveChangeAsync();

    }
}
