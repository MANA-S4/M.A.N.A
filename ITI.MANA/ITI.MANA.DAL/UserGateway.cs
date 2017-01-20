using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace ITI.MANA.DAL
{
    public class UserGateway
    {
        readonly string _connectionString;

        public UserGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<User> GetAll()
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<User>("select u.UserId, u.Email, u.FirstName, u.LastName from iti.Users u;");
            }
        }

        public User FindById(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<User>(
                        "select u.UserId, u.Email, u.FirstName, u.LastName, u.BirthDate from iti.Users u where u.UserId = @UserId",
                        new { UserId = userId })
                    .FirstOrDefault();
            }
        }

        public User FindByEmail(string email)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<User>(
                        "select u.UserId, u.Email, u.FirstName, u.LastName from iti.Users u where u.Email = @Email",
                        new { Email = email })
                    .FirstOrDefault();
            }
        }

        public void CreatePasswordUser(string email, byte[] password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sPasswordUserCreate",
                    new { Email = email, Password = password },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void CreateGoogleUser(string email,string accessToken , string refreshToken, string tokenType, long expireIn)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sGoogleUserCreate",
                    new { Email = email,AccessToken = accessToken, RefreshToken = refreshToken, TokenType = tokenType, ExpireIn = expireIn },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// To Create a Microsoft User
        /// </summary>
        /// <param name="email"></param>
        /// <param name="refreshToken"></param>
        public void CreateMicrosoftUser(string email, string accessToken)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                   "iti.sMicrosoftUserCreate",
                   new { Email = email, AccessToken = accessToken },
                   commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<string> GetAuthenticationProviders(string userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<string>(
                    "select p.ProviderName from iti.vAuthenticationProvider p where p.UserId = @UserId",
                    new { UserId = userId });
            }
        }

        public void Delete(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute("iti.sUserDelete", new { UserId = userId }, commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateUser(int userId, string email,string lastName,string firstName, DateTime birthDate, byte[] password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sCompleteUserUpdate",
                    new { UserId = userId, Email = email, LastName = lastName, FirstName = firstName, BirthDate = birthDate, Password = password },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateUser(int userId, string email, string lastName, string firstName, DateTime birthDate)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sUserInfoUpdate",
                    new { UserId = userId, Email = email, LastName = lastName, FirstName = firstName, BirthDate = birthDate },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateGoogleToken(int userId, string accessToken , string refreshToken, string tokenType, long expireIn)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sGoogleUserUpdate",
                    new { UserId = userId, AccessToken = accessToken, RefreshToken = refreshToken, TokenType = tokenType, ExpireIn = expireIn },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// To Update Microsoft Token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="refreshToken"></param>
        public void UpdateMicrosoftToken(int userId, string accessToken)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sMicrosoftUserUpdate",
                    new { UserId = userId, accessToken = accessToken },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
