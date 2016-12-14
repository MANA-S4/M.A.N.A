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
                    @"celect c.ContactId,
                             c.FirstName,
                             c.LastName,
                             c.BirthDate,
                      from iti.vContact c;");
            }
        }

        public Contact FindById(int contactId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Contact>(
                        @"celect c.ContactId,
                                 c.FirstName,
                                 c.LastName,
                                 c.BirthDate,
                          from iti.vContact c
                          where c.ContactId = @ContactId;",
                        new { ContactId = contactId })
                    .FirstOrDefault();
            }
        }

        public Contact FindByName(string firstName, string lastName)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Contact>(
                        @"celect c.ContactId,
                                 c.FirstName,
                                 c.LastName,
                                 c.BirthDate,
                          from iti.vContact c
                          where c.firstName = @FirstName and c.lastName = @LastName;",
                        new { FirstName = firstName, LastName = lastName })
                    .FirstOrDefault();
            }
        }

        public void Create(string firstName, string lastName, DateTime birthDate)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sContactCreate",
                    new { FirstName = firstName, LastName = lastName, BirthDate = birthDate },
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

        public void Update(int contactId, string firstName, string lastName, DateTime birthDate)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sContactUpdate",
                    new { ContactId = contactId, FirstName = firstName, LastName = lastName, BirthDate = birthDate },
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
