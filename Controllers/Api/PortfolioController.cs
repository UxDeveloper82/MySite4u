using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySite4u.Interfaces;
using MySite4u.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySite4u.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortRepository _repo;

        public PortfolioController(IPortRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<List<Portfolio>> GetPorts()
        {
            return _repo.GetAllPortfolios();
        }

        //GET: api/Portfolio/1
        [HttpGet("{id}")]
        public IActionResult GetPort([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var port = _repo.GetPort(id);

            if (port == null)
            {
                return NotFound();
            }
            return Ok(port);
        }
        //Delete : api/Portfolio/delete
        [HttpDelete("{id}")]
        public IActionResult DeletePort([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var port = _repo.GetPort(id);
            if (port == null)
            {
                return NotFound();
            }

            _repo.RemovePort(id);
            _repo.SaveChangesAsync();
            return Ok(port);

        }

    }
}
