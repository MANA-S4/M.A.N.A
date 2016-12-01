using System;
using Microsoft.AspNetCore.Builder;
using ITI.MANA.WebApp.Authentification;

/// <summary>
/// To verify (contains one error)
/// </summary>
namespace ITI.MANA.WebApp.Authentification
{
    public static class MicrosoftAppBuilderExtensions
    {
        public static IApplicationBuilder UseMicrosoftAuthentication(this IApplicationBuilder app, Action<MicrosoftAccountOptions> configuration)
        {
            MicrosoftAccountOptions options = new MicrosoftAccountOptions();
            configuration(options);
            app.UseMicrosoftAuthentication(configuration);
            return app;
        }
    }
}
