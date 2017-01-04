using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ITI.MANA.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace ITI.MANA.WebApp.Authentification
{
    public class ExternalAuthenticationEvents
    {
        readonly IExternalAuthenticationManager _userManager;

        public ExternalAuthenticationEvents(IExternalAuthenticationManager userManager)
        {
            _userManager = userManager;
        }

        public System.Threading.Tasks.Task OnCreatingTicket(OAuthCreatingTicketContext context)
        {
            _userManager.CreateOrUpdateUser(context);
            User user = _userManager.FindUser(context);
            ClaimsPrincipal principal = CreatePrincipal(user);
            context.Ticket = new AuthenticationTicket(principal, context.Ticket.Properties, CookieAuthentication.AuthenticationScheme);
            return System.Threading.Tasks.Task.CompletedTask;
        }

        ClaimsPrincipal CreatePrincipal(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim( ClaimTypes.NameIdentifier, user.UserId.ToString(), ClaimValueTypes.String ),
                new Claim( ClaimTypes.Email, user.Email )
            };
            ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", ClaimTypes.Email, string.Empty));
            return principal;
        }
    }
}