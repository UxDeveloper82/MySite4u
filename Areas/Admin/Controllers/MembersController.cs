using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySite4u.Interfaces;
using MySite4u.Utility;
using MySite4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySite4u.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            if (User.IsInRole(SD.Role_Admin))
                return View("Index");
            return View("ReadOnlyList");
        }
        //Details
        public IActionResult Details(int id)
        {
            var member = _repo.GetMember(id);
            return View(member);
        }
        [Authorize(Roles = SD.Role_Admin)]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new MemberViewModel());
            else
            {
                var member = _repo.GetMember((int)id);
                return View(new MemberViewModel
                {
                    Id = member.Id,
                    UserName = member.UserName,
                    DateOfBirth = member.DateOfBirth,
                    KnownAs = member.KnownAs,
                    Created = member.Created,
                    LastActive = member.LastActive,
                    Gender = member.Gender,
                    Introduction = member.Introduction,
                    LookingFor = member.LookingFor,
                    Interests = member.Interests,
                    City = member.City,
                    Country = member.Country,
                    CurrentImage = member.Photo
                });

            }
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public async Task<ActionResult> Edit(MemberViewModel vm)
        {
            var user = new Models.Member
            {
                Id = vm.Id,
                UserName = vm.UserName,
                DateOfBirth = vm.DateOfBirth,
                KnownAs = vm.KnownAs,
                Created = vm.Created,
                LastActive = vm.LastActive,
                Gender = vm.Gender,
                Introduction = vm.Introduction,
                LookingFor = vm.LookingFor,
                Interests = vm.Interests,
                City = vm.City,
                Country = vm.Country
            };
            if (vm.Photo == null)
            {
                user.Photo = vm.CurrentImage;
            }
            else
            {
                user.Photo = await _fileManager.SaveImage(vm.Photo);
            }
            if (user.Id > 0)
                _repo.UpdateMember(user);
            else
                _repo.AddMember(user);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(user);
        }

        [HttpGet("/MemberPhoto/{memberPhoto}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult MemberPhoto(string memberPhoto)
        {
            var mine = memberPhoto.Substring(memberPhoto.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(memberPhoto), $"memberPhoto/{mine}");
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
