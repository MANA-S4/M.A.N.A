using System;
using Microsoft.AspNetCore.Builder;
using ITI.MANA.WebApp.Authentification;

namespace ITI.MANA.WebApp.Authentification
{
    /// <summary>
    /// This class allow to use Microsoft Authentication
    /// </summary>
    public static class MicrosoftAppBuilderExtensions
    {
        /// <summary>
        /// UseMicrosoftAccountAuthentication is the best method to use app.UseMicrosoftAccountAuthentication(options);
        /// Works
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMicrosoftAccountAuthentication(this IApplicationBuilder app, Action<MicrosoftAccountOptions> configuration)
        {
            MicrosoftAccountOptions options = new MicrosoftAccountOptions();
            configuration(options);
            app.UseMicrosoftAccountAuthentication(options);
            return app;
        }
    }
}
