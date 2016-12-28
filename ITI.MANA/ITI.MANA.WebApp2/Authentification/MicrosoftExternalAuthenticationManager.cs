using ITI.MANA.DAL;
using ITI.MANA.WebApp.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using ITI.MANA.WebApp.Authentication;

namespace ITI.MANA.WebApp.Authentification
{
    public class MicrosoftExternalAuthenticationManager : IExternalAuthenticationManager
    {
        readonly UserService _userService;

        /// <summary>
        /// Constructor MicrosoftExternalAuthenticationManager
        /// </summary>
        /// <param name="userService"></param>
        public MicrosoftExternalAuthenticationManager(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Allow to create or update a user when his connect
        /// </summary>
        /// <param name="context"></param>
        public void CreateOrUpdateUser(OAuthCreatingTicketContext context)
        {
            if (_userService.FindUser(context.GetEmail()) != null)
            {
                if (context.AccessToken != null)
                {
                    _userService.CreateOrUpdateMicrosoftUser(context.GetEmail(), context.AccessToken);
                }
            }
            else
            {
                _userService.CreateOrUpdateMicrosoftUser(context.GetEmail(), context.AccessToken);
                MailService mail = new MailService(context.GetEmail());
                mail.SendConfirmationMail(context.GetEmail());
            }
        }

        /// <summary>
        /// Allow to find a user with his account
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public User FindUser(OAuthCreatingTicketContext context)
        {
            return _userService.FindUser(context.GetEmail());
        }
    }
}
