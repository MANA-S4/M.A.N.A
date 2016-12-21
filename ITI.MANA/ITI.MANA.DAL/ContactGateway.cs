using ITI.MANA.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace ITI.MANA.DAL
{
    public class ContactGateway
    {
        readonly string _connectionString;

        public ContactGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Contact> GetAll(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Contact>(
                    @"select c.UserRelationId, 
                        u.Email, 
                        c.RelationType
                        from iti.Contacts c 
                        join iti.Users u 
                        on c.UserRelationId = u.UserId 
                        where c.UserId = @UserId;",
                    new { UserId = userId });
            }
        }

        public Contact FindById(int contactId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Contact>(
                        @"select c.ContactId
                          from iti.Contacts c
                          where c.ContactId = @ContactId;",
                        new { ContactId = contactId })
                    .FirstOrDefault();
            }
        }

        public Contact FindByMail(string email)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Contact>( //Changer la commande
                        @"select u.UserId
                          from iti.Users u
                          where u.email = @Email",
                        new { Email = email })
                    .FirstOrDefault();
            }
        }

        public int FindIdByMail(string email)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<int>( //Changer la commande
                        @"select u.UserId
                          from iti.Users u
                          where u.email = @Email",
                        new { Email = email })
                    .FirstOrDefault();
            }
        }

        public void Create(int userId, string relationType, int userRelationId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sContactCreate",
                    new { UserId = userId, RelationType = relationType, UserRelationId = userRelationId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void Delete(int contactId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sContactDelete",
                    new { ContactId = contactId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void Update(int contactId, string relationType)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sContactUpdate",
                    new { ContactId = contactId, RelationType = relationType },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void AssignPerson(int contactId, int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sAssignPerson",
                    new { ContactId = contactId, UserId = userId },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
