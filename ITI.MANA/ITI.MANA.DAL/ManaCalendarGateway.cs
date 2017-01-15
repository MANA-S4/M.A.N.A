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
    public class ManaCalendarGateway
    {
        readonly string _connectionString;

        public ManaCalendarGateway(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        /// <summary>
        /// Gets the list events.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CalendarEvent> GetListEvents(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<CalendarEvent>(
                        "select e.EventId as EventId,e.UserId as UserId,e.EventName as EventName, e.EventDate as Date, e.IsPrivate as Private, e.Members as Members from iti.Events e where e.UserId = @UserId",
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
        
        public void CreateEvent(string eventName, DateTime eventDate, string members, bool isPrivate, int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sEventCreate",
                    new { EventName = eventName, EventDate = eventDate, Members = members, IsPrivate = isPrivate, UserId = userId },
                    commandType: CommandType.StoredProcedure);
            }
        }
        /*
        public void DeleteEvent(int studentId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sStudentDelete",
                    new { StudentId = studentId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateEvent(int studentId, string firstName, string lastName, DateTime birthDate, string gitHubLogin, int classId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sStudentUpdate",
                    new { StudentId = studentId, FirstName = firstName, LastName = lastName, BirthDate = birthDate, GitHubLogin = gitHubLogin ?? string.Empty, ClassId = classId },
                    commandType: CommandType.StoredProcedure);
            }
        }*/
    }
}
