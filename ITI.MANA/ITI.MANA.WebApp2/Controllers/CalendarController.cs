using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ITI.MANA.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using ITI.MANA.WebApp.Authentification;
using Google.Apis.Calendar.v3.Data;
using System.Security.Claims;
using ITI.MANA.DAL;
using ITI.MANA.WebApp.Models.CalendarViewModel;
using ITI.PrimarySchool.WebApp.Controllers;

namespace ITI.MANA.WebApp.Controllers
{
    [Route("api/calendars")]
    public class CalendarController : Controller
    {
        readonly GoogleCalendarService _googleCalendarService;
        readonly ManaCalendarService _manaCalendarService;

        public CalendarController(ManaCalendarService manaCalendarService,GoogleCalendarService googleCalendarService)
        {
            _manaCalendarService = manaCalendarService;
            _googleCalendarService = googleCalendarService;
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

        /*[HttpGet(Name = "ExportEvents")]
        [Authorize(ActiveAuthenticationSchemes = CookieAuthentication.AuthenticationScheme)]
        public void ExportEventsList()
        {
            _googleCalendarService.CreateTokenResponseAndCallRequest(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }*/
    }
}
