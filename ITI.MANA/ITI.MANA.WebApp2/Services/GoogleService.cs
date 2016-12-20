using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Calendar.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.MANA.WebApp.Services
{
    public class GoogleService
    {
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API .NET Quickstart";
        UserCredential credential = null;

        public TokenResponse CreateTokenResponse(string accessToken, string refreshToken, string tokenType)
        {
            return new TokenResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Issued = DateTime.Now,
                ExpiresInSeconds = 36000000000,
                TokenType = tokenType
            };
        }

        public GoogleAuthorizationCodeFlow.Initializer CreateInitializer(string clientId, string clientSecret)
        {
            return new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                Scopes = Scopes
            };
        }
        public GoogleAuthorizationCodeFlow CreateFlow(GoogleAuthorizationCodeFlow.Initializer initializer)
        {
            return new GoogleAuthorizationCodeFlow(initializer);
        }

        public UserCredential CreateCredential(GoogleAuthorizationCodeFlow flow, string userId, TokenResponse tokenResponse)
        {
            return new UserCredential(flow, userId, tokenResponse);
        }
    }
}
