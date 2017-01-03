using ITI.MANA.DAL;
using ITI.MANA.WebApp.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using ITI.MANA.WebApp.Authentication;
using ITI.MANA.WebApp.Server;

namespace ITI.MANA.WebApp.Authentification
{
    public class GoogleExternalAuthenticationManager : IExternalAuthenticationManager
    {
        readonly UserService _userService;
        string email;

        public GoogleExternalAuthenticationManager(UserService userService)
        {
            _userService = userService;
        }

        public void CreateOrUpdateUser(OAuthCreatingTicketContext context)
        {
            if (context.AccessToken != null)
            {
                _userService.CreateOrUpdateGoogleUser(context.GetEmail(), context.AccessToken, context.RefreshToken, context.TokenType, context.ExpiresIn);
            }

        }

        public User FindUser(OAuthCreatingTicketContext context)
        {
            return _userService.FindUser(context.GetEmail());
        }
    }
}