using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySite4u.Areas.UI.Controllers
{
    [Area("UI")]
    public class VideosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
