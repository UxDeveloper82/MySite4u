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

namespace MySite4u.Areas.UI.Controllers
{
    [Area("UI")]
    public class PortfolioController : Controller
    {
        private readonly IPortRepository _repo;
        public PortfolioController(IPortRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index(int pageNumber, string category, string search, string orderBy)
        {
            if (pageNumber < 1)
                return RedirectToAction("Index", new { pageNumber = 1, category });
            var ports = _repo.GetAllPortfolios(pageNumber, category, search, orderBy);
            return View("Index", ports);
        }

        public ActionResult Details(int id)
        {
            var port = _repo.GetPort(id);
            return View(port);
        }

    }
}
