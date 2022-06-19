using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySite4u.Interfaces;
using MySite4u.Utility;
using MySite4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySite4u.Areas.UI.Controllers
{
    [Area("UI")]
    public class MembersController : Controller
    {
        private readonly ITechRepository _repo;
        private readonly IFileManager _fileManager;

        public MembersController(ITechRepository repo,
            IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }
        //Index Page
        public IActionResult Index()
        {
            return View();
        }
        //Details
        public IActionResult Details(int id)
        {
            var member = _repo.GetMember(id);
            return View(member);
        }
       

        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            var objFromDb = _repo.GetAllMembers();
            return Json(new { data = objFromDb });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _repo.GetMember(id);
            if (objFromDb == null)
            {
                TempData["Error"] = "Error deleting Message";
                return Json(new { success = false, message = "Error while deleting" });
            };
            _repo.RemoveMember(id);
            _repo.SaveChangesAsync();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }
}