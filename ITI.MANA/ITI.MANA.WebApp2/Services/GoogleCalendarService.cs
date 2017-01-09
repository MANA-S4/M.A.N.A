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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.MANA.WebApp.Services
{
    public class GoogleCalendarService
    {
        readonly GoogleCalendarGateway _googleCalendarGateway;

        public GoogleCalendarService(GoogleCalendarGateway googleCalendarGateway)
        {
            _googleCalendarGateway = googleCalendarGateway;
        }

        public TokenResponse CreateTokenResponse(int userId)
        {
            return _googleCalendarGateway.GetResponseToken(userId);
        }        
    }
}
