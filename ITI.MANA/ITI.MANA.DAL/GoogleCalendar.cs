using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Text;
using System.Threading;
using System.IO;

namespace ITI.MANA.DAL
{
    public class GoogleCalendar
    {
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API .NET Quickstart";
        static EventsResource.ListRequest request = service.Events.List("primary");

        static UserCredential credential;

        static CalendarService service = new CalendarService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });


        // Define parameters of request.
        public void DefineGoogleRequestParameters()
        {
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
        }


        // List events.
        public void GetListEvents()
        {
            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }
            Console.Read();
        }

    }
}
