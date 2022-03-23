using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySite4u.Data;
using MySite4u.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySite4u.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class MyMessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyMessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS
        [HttpGet]

        public IActionResult GetAll()
        {
            var objFromDb = _context.MyMessages.ToList();
            return Json(new { data = objFromDb });
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var objFromDb = _context.MyMessages.FirstOrDefault(c => c.Id == id);
            if (objFromDb == null)
            {
                TempData["Error"] = "Error deleting Message";
                return Json(new { success = false, message = "Error while deleting" });
            }
            _context.MyMessages.Remove(objFromDb);
            _context.SaveChanges();

            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
