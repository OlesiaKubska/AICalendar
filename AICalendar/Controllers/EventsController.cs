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

        // GET: /api/v1/events/next-week
        [HttpGet("next-week")]
        public IActionResult GetEventsForNextWeek()
        {
            var today = DateTime.Today;
            var nextWeek = today.AddDays(7);
            var nextWeekEvents = Events.Where(e => e.Start >= today && e.Start < nextWeek).ToList();
            return Ok(nextWeekEvents);
        }

        // GET: /api/v1/events/next-week/table
        [HttpGet("next-week/table")]
        public ContentResult GetEventsForNextWeekAsTable()
        {
            var today = DateTime.Today;
            var nextWeek = today.AddDays(7);
            var nextWeekEvents = Events.Where(e => e.Start >= today && e.Start < nextWeek).ToList();

            var html = "<table border='1'><tr><th>Title</th><th>Description</th><th>Participants</th><th>Start</th><th>End</th></tr>";
            foreach (var ev in nextWeekEvents)
            {
                html += $"<tr>"
                    + $"<td>{System.Net.WebUtility.HtmlEncode(ev.Title)}</td>"
                    + $"<td>{System.Net.WebUtility.HtmlEncode(ev.Description)}</td>"
                    + $"<td>{string.Join(", ", ev.Participants.Select(System.Net.WebUtility.HtmlEncode))}</td>"
                    + $"<td>{ev.Start}</td>"
                    + $"<td>{ev.End}</td>"
                    + "</tr>";
            }
            html += "</table>";
            return Content(html, "text/html");
        }
    }
}