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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.MANA.WebApp.Controllers
{
    [Route("api/calendars")]
    public class GoogleCalendarController : Controller
    {
        readonly GoogleCalendarService _googleCalendarService;

        public GoogleCalendarController(GoogleCalendarService googleCalendarService)
        {
            _googleCalendarService = googleCalendarService;
        }

        [HttpGet]
        public IActionResult GetEventsList()
        {
            Result<IEnumerable<GoogleCalendarEvents>> result = _googleCalendarService.GetListEvents(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return this.CreateResult<IEnumerable<GoogleCalendarEvents>, IEnumerable<EventsViewModel>>(result, o =>
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
