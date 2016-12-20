using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace ITI.MANA.DAL
{
    public class GoogleCalendarGateway
    {
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API .NET Quickstart";
        static EventsResource.ListRequest request = service.Events.List("primary");
        readonly string _connectionString;
        static UserCredential credential;
        Events events;

        public GoogleCalendarGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        static CalendarService service = new CalendarService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });

        /// <summary>
        /// Defines the google request parameters.
        /// </summary>
        public void DefineGoogleRequestParameters()
        {
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
        }

        /// <summary>
        /// Gets the list events.
        /// </summary>
        /// <returns></returns>
        public Events GetListEvents()
        {
            return events = request.Execute();
        }
    }
}
