using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ITI.MANA.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using ITI.MANA.WebApp.Authentification;
using Google.Apis.Calendar.v3.Data;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.MANA.WebApp.Controllers
{
    [Route("api/calendar")]
    public class GoogleCalendarController : Controller
    {
        readonly GoogleCalendarService _googleCalendarService;

        public GoogleCalendarController(GoogleCalendarService googleCalendarService)
        {
            _googleCalendarService = googleCalendarService;
        }

        [HttpGet]
        [Authorize(ActiveAuthenticationSchemes = CookieAuthentication.AuthenticationScheme)]
        public Events ExportEventsList()
        {
            return _googleCalendarService.CreateTokenResponseAndCallRequest(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }
    }
}
