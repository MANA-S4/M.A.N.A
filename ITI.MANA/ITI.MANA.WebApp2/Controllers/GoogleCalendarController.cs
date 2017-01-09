using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ITI.MANA.WebApp.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.MANA.WebApp.Controllers
{
    public class GoogleCalendarController : Controller
    {
        readonly GoogleCalendarService _googleCalendarService;

        public GoogleCalendarController(GoogleCalendarService googleCalendarService)
        {
            _googleCalendarService = googleCalendarService;
        }


    }
}
