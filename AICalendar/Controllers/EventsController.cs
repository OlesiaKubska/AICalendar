using Microsoft.AspNetCore.Mvc;
using AICalendar.Models;
using AICalendar.Services;

namespace AICalendar.Controllers
{
    [ApiController]
    [Route("api/v1/events")]
    public class EventsController : ControllerBase
    {
        private static readonly List<CalendarEvent> Events = new();

        // GET: /api/v1/events
        [HttpGet]
        public IActionResult GetAll() => Ok(Events);

        // POST: /api/v1/events
        [HttpPost]
        public IActionResult Create([FromBody] CalendarEvent ev)
        {
            Events.Add(ev);
            return Ok(ev);
        }

        // GET: /api/v1/events/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var ev = Events.FirstOrDefault(e => e.Id == id);
            if (ev == null) return NotFound();

            return Ok(ev);
        }

        // PUT: /api/v1/events/{id}
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] CalendarEvent updated)
        {
            var ev = Events.FirstOrDefault(e => e.Id == id);
            if (ev == null) return NotFound();

            ev.Title = updated.Title;
            ev.Description = updated.Description;
            ev.Participants = updated.Participants;
            ev.Start = updated.Start;
            ev.End = updated.End;

            return Ok(ev);
        }

        // DELETE: /api/v1/events/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var ev = Events.FirstOrDefault(e => e.Id == id);
            if (ev == null) return NotFound();

            Events.Remove(ev);
            return Ok("Deleted");
        }

        [HttpPost("find-slot")]
        public IActionResult FindFreeSlot([FromBody] SlotRequest request)
        {
            var result = TimeSlotFinderService.FindFreeSlot(
                Events,
                request.Participants,
                request.From,
                request.To,
                request.Duration);

            if (result == null)
                return NotFound("No available slot");

            return Ok(new { start = result });
        }
    }
}