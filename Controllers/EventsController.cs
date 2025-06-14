using Microsoft.AspNetCore.Mvc;
using AICalendar.Models;

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
    }
}