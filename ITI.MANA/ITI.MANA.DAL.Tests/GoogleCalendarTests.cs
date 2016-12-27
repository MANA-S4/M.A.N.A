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
            //TokenResponse token = sut.GetResponseToken(13);

            var token = new TokenResponse()
            {
                AccessToken = "ya29.Ci_BA7Zow8HvrWyuo_vApqYuTh_3iJTnzwZlUGYz73yJc33KTsn78PvpSDONZjOXDA",
                RefreshToken = "1/okxG33eetRNmIYFBBBqLD7plCza9DC1xai2NxzYC6ac3Vq2FX4Rbjgru9j8WOhWG",
                TokenType = "Bearer"
            };

            var initializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "847663728233-9a1fjnu2dui4o9drhc8dtjgnvkck51kc.apps.googleusercontent.com",
                    ClientSecret = "G3mSLUh3l4u9lO1IqC8NBiBx",
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
        }
    }
}
