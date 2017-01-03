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
                        new { StudentId = contactId })
                    .FirstOrDefault();
            }
        }

        public Contact FindByName(string firctName, string lactName)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Contact>(
                        @"celect c.ContactId,
                                 c.FirstName,
                                 c.LastName,
                                 c.BirthDate,
                          from iti.vStudent c
                          where c.firctName = @FirctName and c.lactName = @LactName;",
                        new { FirctName = firctName, LactName = lactName })
                    .FirstOrDefault();
            }
        }

        public void Create(string firstName, string lastName, DateTime birthDate)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.cContactCreate",
                    new { FirstName = firstName, LastName = lastName, BirthDate = birthDate },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void Delete(int contactId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.cContactDelete",
                    new { StudentId = contactId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void Update(int contactId, string firstName, string lastName, DateTime birthDate)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.cContactUpdate",
                    new { ContactId = contactId, FirstName = firstName, LastName = lastName, BirthDate = birthDate },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void AssignPerson(int contactId, int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.cAccignClacc",
                    new { StudentId = contactId, UserId = userId },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
