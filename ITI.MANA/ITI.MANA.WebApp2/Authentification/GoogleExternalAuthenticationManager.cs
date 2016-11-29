using ITI.MANA.DAL;
using ITI.MANA.WebApp.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using ITI.MANA.WebApp.Authentication;

namespace ITI.MANA.WebApp.Authentification
{
    public class GoogleExternalAuthenticationManager : IExternalAuthenticationManager
    {
        readonly UserService _userService;

        public GoogleExternalAuthenticationManager (UserService userService)
        {
            _userService = userService;
        }

        public void CreateOrUpdateUser(OAuthCreatingTicketContext context)
        {
            if (context.RefreshToken != null)
            {
                _userService.CreateOrUpdateGoogleUser(context.GetEmail(), context.RefreshToken);
            }
        }

        public User FindUser(OAuthCreatingTicketContext context)
        {
            return _userService.FindUser(context.GetEmail());
        }
    }
}