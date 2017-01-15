using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Calendar.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using ITI.MANA.DAL;
using Microsoft.Extensions.Options;
using static Google.Apis.Auth.OAuth2.Flows.GoogleAuthorizationCodeFlow;
using Google.Apis.Services;
using Google.Apis.Calendar.v3.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.MANA.WebApp.Services
{
    public class ManaCalendarService
    {
        readonly ManaCalendarGateway _manaCalendarGateway;

        public ManaCalendarService(ManaCalendarGateway manaCalendarGateway)
        {
            _manaCalendarGateway = manaCalendarGateway;
        }

        public Result<IEnumerable<CalendarEvent>> GetListEvents(int userId)
        {
            return Result.Success(Status.Ok, _manaCalendarGateway.GetListEvents(userId));
        }

        public void CreateEvent(string eventName, DateTime eventDate, string members, bool isPrivate, int userId)
        {
            _manaCalendarGateway.CreateEvent(eventName, eventDate, members, isPrivate, userId);
        }
    }
}
