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
                return con.Query<User>("select u.UserId, u.Email, u.[Password], u.GoogleRefreshToken, u.MicrosoftRefreshToken from iti.vUser u;");
            }
        }

        public User FindById(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<User>(
                        "select u.UserId, u.Email, u.[Password], u.MicrosoftAccessToken, u.GoogleRefreshToken from iti.vUser u where u.UserId = @UserId",
                        new { UserId = userId })
                    .FirstOrDefault();
            }
        }

        public User FindByEmail(string email)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<User>(
                        "select u.UserId, u.Email, u.[Password], u.MicrosoftAccessToken, u.GoogleRefreshToken from iti.vUser u where u.Email = @Email",
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

        public void UpdateEmail(int userId, string email)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sUserUpdate",
                    new { UserId = userId, Email = email },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdatePassword(int userId, byte[] password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sPasswordUserUpdate",
                    new { UserId = userId, Password = password },
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

        public void AddPassword(int userId, byte[] password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sUserAddPassword",
                    new { UserId = userId, Password = password },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void AddGoogleToken(int userId, string refreshToken)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sUserAddGoogleToken",
                    new { UserId = userId, RefreshToken = refreshToken },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// To Add Microsoft Token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="refreshToken"></param>
        public void AddMicrosoftToken(int userId, string refreshToken)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sUserAddMicrosoftToken",
                    new { UserId = userId, RefreshToken = refreshToken },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
