using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Calendar.v3.Data;

namespace ITI.MANA.DAL.Tests
{
    [TestFixture]
    public class GoogleCalendarTests
    {
        GoogleCalendarGateway sut = new GoogleCalendarGateway(TestHelpers.ConnectionString);
              
        string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        string ApplicationName = "M.A.N.A";


        [Test]
        public void GetTokenResponseReturnValue()
        {
            TokenResponse token = sut.GetResponseToken(13);
            Assert.That(token != null);
        }

        [Test]
        public void EventsCanBeSavedInDatabase()
        {
            UserCredential credential;
            TokenResponse token = sut.GetResponseToken(13);
            
            var initializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "847663728233-mgjdj8ej4t8obpcmad2aoea9qfok65in.apps.googleusercontent.com",
                    ClientSecret = "VQbak4VjBRvTU2H_yaZLs7US",
                },
                Scopes = Scopes
            };

            var flow = new GoogleAuthorizationCodeFlow(initializer);

            credential = new UserCredential(flow, "user", token);

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = request.Execute();
            Assert.That(events != null);
        }
    }
}
