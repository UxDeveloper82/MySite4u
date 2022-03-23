using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySite4u.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PanelController: Controller
    {
        public IActionResult FrontPage()
        {
            return View();
        }
    }
}
