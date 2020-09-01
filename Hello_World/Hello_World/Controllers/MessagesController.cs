using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hello_World.Models;

namespace Hello_World.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageContext _context;

        public MessagesController(MessageContext context)
        {
            _context = context;
            Message message = new Message
            {
                Value = "Hello World!"
            };

            if (!_context.Messages.Any((m) => m.Value == message.Value))
            {
                _context.Messages.Add(message);
                _context.SaveChangesAsync();
            }
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessages()
        {
            return await _context.Messages
                .Select((m) => MessageToDto(m))
                .ToListAsync();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDto>> GetMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return MessageToDto(message);
        }

        // PUT: api/Messages/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            var _message = await _context.Messages.FindAsync(id);
            if (_message == null)
            {
                return NotFound();
            }

            _context.Entry(_message).State = EntityState.Modified;

            _message.Value = message.Value;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!MessageExists(message.Id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Messages
        [HttpPost]
        public async Task<ActionResult<MessageDto>> PostMessage(MessageDto messageDto)
        {
            Message message = new Message 
            { 
                Value = messageDto.Value 
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, messageDto);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageDto>> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return MessageToDto(message);
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }

        // Convert message model to DTO
        private static MessageDto MessageToDto(Message message)
        {
            return new MessageDto
            {
                Value = message.Value
            };
        }
    }
}
