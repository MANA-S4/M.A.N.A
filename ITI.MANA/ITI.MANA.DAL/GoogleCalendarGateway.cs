using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2.Responses;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace ITI.MANA.DAL
{
    public class GoogleCalendarGateway
    {
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "M.A.N.A";
        static EventsResource.ListRequest request = service.Events.List("primary");
        readonly string _connectionString;
        static UserCredential credential;
        Events events;

        public GoogleCalendarGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        static CalendarService service = new CalendarService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });

        /// <summary>
        /// Defines the google request parameters.
        /// </summary>
        public void DefineGoogleRequestParameters()
        {
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
        }

        /// <summary>
        /// Gets the list events.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Events> GetListEvents(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Events>(
                        "select e.EventName, e.EventDate, e.IsFinish, e.IsPrivate, E.Members from iti.Events e where e.UserId = @UserId",
                        new { UserId = userId });
            }
        }

        public TokenResponse GetResponseToken(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<TokenResponse>(
                        "select g.AccessToken, g.RefreshToken, g.TokenType from iti.GoogleUser g where g.UserId = @UserId",
                        new { UserId = userId })
                    .FirstOrDefault();
            }
        }

        public void ExportEventsFromGoogle(int userId,string eventsJson)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sExportEventsFromGoogle",
                    new { UserId = userId,Json = eventsJson },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
