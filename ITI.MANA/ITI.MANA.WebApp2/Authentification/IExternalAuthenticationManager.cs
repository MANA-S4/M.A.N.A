using ITI.MANA.DAL;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace ITI.MANA.WebApp.Authentification
{
    public interface IExternalAuthenticationManager
    {
        void CreateOrUpdateUser(OAuthCreatingTicketContext context);

        User FindUser(OAuthCreatingTicketContext context);
    }
}