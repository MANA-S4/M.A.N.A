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

        public IEnumerable<Contact> GetAll()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Contact>(
                    @"select c.ContactId,
                             c.UserId,
                             c.Type
                      from iti.vContact c
                      where c.UserId = @UserId;");
            }
        }

        public Contact FindById(int contactId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Contact>(
                        @"select c.ContactId,
                                 c.FirstName,
                                 c.LastName,
                                 c.BirthDate
                          from iti.vContact c
                          where c.ContactId = @ContactId;",
                        new { ContactId = contactId })
                    .FirstOrDefault();
            }
        }

        public Contact FindByMail(string email)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Contact>(
                        @"select c.ContactId,
                                 c.Email
                          from iti.vContact c
                          where c.email = @Email",
                        new { Email = email })
                    .FirstOrDefault();
            }
        }

        public void Create(string email, string link)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sContactCreate",
                    new { Link = link },
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

        public void Update(int contactId, string email, string link)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sContactUpdate",
                    new { ContactId = contactId, Email = email, Link = link },
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
