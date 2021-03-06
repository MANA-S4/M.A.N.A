﻿using System.Text;
using ITI.MANA.DAL;
using ITI.MANA.WebApp.Authentification;
using ITI.MANA.WebApp.Services;
using Microsoft.AspNetCore.Authentification.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependancyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ITI.MANA.WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuation = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            string secretKey = Configuration["JwtBearer:SigningKey"];
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            services.Configure<TokenProviderOptions>(o =>
            {
                o.Audience = Configuration["JwtBearer:Audience"];
                o.Issuer = Configuration["JwtBearer:Issuer"];
                o.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddMvc();
            services.AddSingleton(_ => new UserGateway(Configuration["ConnectionStrings:PrimarySchoolDB"]));
            services.AddSingleton<PasswordHasher>();
            services.AddSingleton<UserService>();
            services.AddSingleton<TokenService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            string secretKey = Configuration["JwtBearer:SigningKey"];
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            app.UseMiddleware<TokenProviderMiddleware>();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AuthenticationScheme = JwtBearerAuthentication.AuthenticationScheme,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,

                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JwtBearer:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = Configuration["JwtBearer:Audience"]
                }
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = CookieAuthentication.AuthenticationScheme
            });

            ExternalAuthenticationEvents googleAuthenticationEvents = new ExternalAuthenticationEvents(
                new GoogleExternalAuthenticationManager(app.ApplicationServices.GetRequiredService<UserService>()));

            app.UseGoogleAuthentication(c =>
            {
                c.SignInScheme = CookieAuthentication.AuthenticationScheme;
                c.ClientId = Configuration["Authentication:Google:ClientId"];
                c.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                c.Events = new OAuthEvents
                {
                    OnCreatingTicket = googleAuthenticationEvents.OnCreatingTicket
                };
                c.AccessType = "offline";
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "spa-fallback",
                    template: "Home/{*anything}",
                    defaults: new { controller = "Home", action = "Index" });
            });

            app.UseStaticFiles();
        }
    }
}