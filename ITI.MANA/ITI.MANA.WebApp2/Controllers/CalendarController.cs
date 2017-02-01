using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ITI.MANA.WebApp.Services;
using System.Security.Claims;
using ITI.MANA.DAL;
using ITI.MANA.WebApp.Models.CalendarViewModel;

namespace ITI.MANA.WebApp.Controllers
{
    [Route("api/calendars")]
    public class CalendarController : Controller
    {
        readonly GoogleCalendarService _googleCalendarService;
        readonly ManaCalendarService _manaCalendarService;

        public CalendarController(ManaCalendarService manaCalendarService, GoogleCalendarService googleCalendarService)
        {
            _manaCalendarService = manaCalendarService;
            _googleCalendarService = googleCalendarService;
        }

        [HttpGet("export")]
        public IActionResult ExportEventsList()
        {
            Result result = _googleCalendarService.CreateTokenResponseAndCallRequest(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return this.CreateResult(result);
        }

        [HttpGet]
        public IActionResult GetEventsList()
        {
            Result<IEnumerable<CalendarEvent>> result = _manaCalendarService.GetListEvents(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return this.CreateResult<IEnumerable<CalendarEvent>, IEnumerable<EventsViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToEventsViewModel());
            });
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] EventsViewModel model)
        {
            Result<CalendarEvent> result = _manaCalendarService.CreateEvent(model.EventName, model.EventDate, model.Members, model.IsPrivate, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return this.CreateResult<CalendarEvent, EventsViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToEventsViewModel();
                o.RouteName = "";
                o.RouteValues = s => new { id = s.EventId };
            } );
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] EventsViewModel model)
        {
            Result<CalendarEvent> result = _manaCalendarService.UpdateEvent(id, model.EventName, model.EventDate, model.Members, model.IsPrivate);
            return this.CreateResult<CalendarEvent, EventsViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToEventsViewModel();
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            Result<int> result = _manaCalendarService.DeleteEvent(id);
            return this.CreateResult(result);
        }
        
    }
}
