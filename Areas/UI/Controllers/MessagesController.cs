using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySite4u.Data;
using MySite4u.Models;
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
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public IActionResult New()
        {
            var viewModel = new MyMessagesViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MyMessage mymessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                _context.MyMessages.Add(mymessage);
                await _context.SaveChangesAsync();
                return Json(new { IsSuccess = "redirect", description = Url.Action("Home", "Index", new { id = mymessage.Id }), mymessage });
            }
        }


    }
}
