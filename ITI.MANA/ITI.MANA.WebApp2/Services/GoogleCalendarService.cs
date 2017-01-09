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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.MANA.WebApp.Services
{
    public class GoogleCalendarService
    {
        readonly GoogleCalendarGateway _googleCalendarGateway;
        readonly IOptions<GoogleCalendarServiceOptions> _options;
        TokenResponse _token;
        Initializer _initializer;
        GoogleAuthorizationCodeFlow _flow;
        UserCredential _credential;
        CalendarService _service;
        string _applicationName = "M.A.N.A";


        public GoogleCalendarService(GoogleCalendarGateway googleCalendarGateway, IOptions<GoogleCalendarServiceOptions> options)
        {
            _googleCalendarGateway = googleCalendarGateway;
            _options = options;
        }

        public void CreateTokenResponse(int userId)
        {
            _token = _googleCalendarGateway.GetResponseToken(userId);
        }

        public Events CreateAndExecuteGoogleCalendarRequest(IOptions<GoogleCalendarServiceOptions> googleOptions)
        {
            _initializer = new Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = googleOptions.Value.ClientId,
                    ClientSecret = googleOptions.Value.ClientSecret
                }
            };

            _flow = new GoogleAuthorizationCodeFlow(_initializer);

            _credential = new UserCredential(_flow, "user", _token);

            _service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = _applicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = _service.Events.List("primary");
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            return request.Execute();
        }
    }

    public class GoogleCalendarServiceOptions
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }

}
