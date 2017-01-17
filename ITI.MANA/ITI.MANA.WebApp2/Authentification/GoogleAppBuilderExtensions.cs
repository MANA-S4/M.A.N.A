using System;
using Microsoft.AspNetCore.Builder;
using Google.Apis.Calendar.v3;

namespace ITI.MANA.WebApp.Authentification
{
    public static class GoogleAppBuilderExtensions
    {
        public static IApplicationBuilder UseGoogleAuthentication(this IApplicationBuilder app, Action<GoogleOptions> configuration)
        {
            GoogleOptions options = new GoogleOptions();
            options.Scope.Add(CalendarService.Scope.CalendarReadonly);
            configuration(options);
            app.UseGoogleAuthentication(options);
            return app;
        }
    }
}