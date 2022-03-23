using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySite4u.Interfaces;
using MySite4u.Models;
using MySite4u.Utility;
using MySite4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySite4u.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class MyPortfolioController : Controller
    {
        private readonly IPortRepository _repo;
        private readonly IFileManager _fileManager;

        public MyPortfolioController(IPortRepository repo,
                                   IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }

        public IActionResult Index(int pageNumber, string category, string search, string orderBy)
        {
            if (User.IsInRole("Admin"))
                return View("Index");
            if (pageNumber < 1)
                return RedirectToAction("Index", new { pageNumber = 1, category });
            var ports = _repo.GetAllPortfolios(pageNumber, category, search, orderBy);
            return View("ReadOnlyList", ports);
        }

        public ActionResult Details(int id)
        {
            var port = _repo.GetPort(id);
            return View(port);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new PortfolioViewModel());
            else
            {
                var port = _repo.GetPort((int)id);
                return View(new PortfolioViewModel
                {
                    Id = port.Id,
                    Name = port.Name,
                    Type = port.Type,
                    Details = port.Details,
                    Language = port.Language,
                    CodeLink = port.CodeLink,
                    Link = port.Link,
                    CurrentImage = port.PortfolioPhoto

                });

            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(PortfolioViewModel vm)
        {
            var port = new Portfolio
            {
                Id = vm.Id,
                Name = vm.Name,
                Type = vm.Type,
                Details = vm.Details,
                Language = vm.Language,
                CodeLink = vm.CodeLink,
                Link = vm.Link
            };
            if (vm.PortfolioPhoto == null)
            {
                port.PortfolioPhoto = vm.CurrentImage;
            }
            else
            {
                port.PortfolioPhoto = await _fileManager.SaveImage(vm.PortfolioPhoto);
            }

            if (port.Id > 0)
                _repo.UpdatePortfolio(port);
            else
                _repo.AddPortfolio(port);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(port);

        }


        [HttpGet("/PortfolioPhoto/{portfolioPhoto}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult PortfolioPhoto(string portfolioPhoto)
        {
            var mine = portfolioPhoto.Substring(portfolioPhoto.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(portfolioPhoto), $"portfolioPhoto/{mine}");
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePort(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
