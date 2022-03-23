using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySite4u.Interfaces;
using MySite4u.Models;
using MySite4u.Models.Comments;
using MySite4u.Utility;
using MySite4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySite4u.Areas.UI.Controllers
{
    [Area("UI")]
    public class PostController : Controller
    {
        private IRepository _repo;
   
        public PostController(IRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var post = _repo.GetAllPosts().ToList();
            return View(post);
        }
        public IActionResult Detail(int id) =>
         View(_repo.GetPost(id));

        [HttpPost]
        public async Task<ActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Post", new { id = vm.PostId });

            var post = _repo.GetPost(vm.PostId);

            if (vm.MainCommentId == 0)
            {
                post.MainComments = post.MainComments ?? new List<MainComment>();

                post.MainComments.Add(new MainComment
                {
                    Message = vm.Message,
                    Created = DateTime.Now

                });
                _repo.UpdatePost(post);
            }
            else
            {
                var comment = new SubComment
                {
                    MainCommentId = vm.MainCommentId,
                    Message = vm.Message,
                    Created = DateTime.Now,
                };
                _repo.AddSubComment(comment);

            }
            await _repo.SaveChangeAsync();

            return RedirectToAction("Detail", new { id = vm.PostId });
        }

    }
}
