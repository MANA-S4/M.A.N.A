using System;
using System.Collections.Generic;
using System.Linq;
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

        public CalendarEvent FindEvent(string eventName, DateTime eventDate, int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<CalendarEvent>(
                        "select e.eventId,e.eventName, e.eventDate, e.userId from iti.Events e where e.eventName = @EventName and e.eventDate = @EventDate and e.userId = @UserId",
                        new { EventName = eventName, EventDate = eventDate, UserId = userId })
                    .FirstOrDefault();
            }
        }

        public void UpdateEvent(int eventId, string eventName, DateTime eventDate, string members, bool isPrivate)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sEventUpdate",
                    new { EventId = eventId, EventName = eventName, EventDate = eventDate, Members = members, IsPrivate = isPrivate },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.Query<GoogleCalendarEvents>("select * from iti.GoogleEvents where EventId = @EventId", new { EventId = id }).FirstOrDefault() != null)
                {
                    con.Query(
                        "delete from iti.GoogleEvents where EventId = @EventId",
                        new { EventId = id }
                        );
                }

                con.Execute(
                    "iti.sDeleteEvent",
                    new { EventId = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public CalendarEvent FindById(object eventId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<CalendarEvent>(
                        "select * from iti.Events where EventId = @EventId",
                        new { EventId = eventId })
                    .FirstOrDefault();
            }
        }

        public void ExportEventsFromGoogle(int userId, string eventsJson)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sExportEventsFromGoogle",
                    new { UserId = userId, Json = eventsJson },
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
