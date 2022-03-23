using Microsoft.AspNetCore.Mvc;
using MySite4u.Data;
using MySite4u.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySite4u.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyMessagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MyMessagesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<MyMessage> GetMessages()
        {
            return _context.MyMessages;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mymessage = await _context.MyMessages.FindAsync(id);
            if (mymessage == null)
                return NotFound();
            return Ok(mymessage);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] MyMessage mymessage)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _context.MyMessages.Add(mymessage);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetMessage", new { id = mymessage.Id }, mymessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mymessage = await _context.MyMessages.FindAsync(id);
            if (mymessage == null)
            {
                return NotFound();
            }

            _context.MyMessages.Remove(mymessage);
            await _context.SaveChangesAsync();
            return Ok(mymessage);
        }

    }
}
