using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ITI.MANA.WebApp.Authentification;
using Microsoft.Extensions.Options;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Calendar.v3;

namespace ITI.MANA.WebApp.Services
{
    public class TokenService
    {
        readonly TokenProviderOptions _options;

        public TokenService(IOptions<TokenProviderOptions> options)
        {
            _options = options.Value;
        }

        public Token GenerateToken(string userId, string email)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim( JwtRegisteredClaimNames.Iat, ( ( int )( now - new DateTime( 1970, 1, 1 ) ).TotalSeconds).ToString(), ClaimValueTypes.Integer64 )
            };

            var jwt = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(_options.Expiration),
                    signingCredentials: _options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new Token(encodedJwt, (int)_options.Expiration.TotalSeconds);
        }
    }

    public class Token
    {
        public Token(string accessToken, int expiresIn)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
        }

        public string AccessToken { get; }

        public int ExpiresIn { get; }
    }

    public class GoogleToken
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