using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MANA.DAL
{
    public class TaskGateway
    {
        readonly string _connectionString;

        public TaskGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Task> GetAll(int taskId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Task>(
                    @"select t.TaskId, t.Name, t.Date, t.IsFinish from iti.Tasks t;");
            }
        }

        public Task FindById(int taskId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Task>(
                        @"select c.TaskId
                          from iti.Tasks t
                          where c.Taskid = @TaskId;",
                        new { TaskId = taskId })
                    .FirstOrDefault();
            }
        }

        ////
        public void Create(int taskId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sTaskCreate",
                    new { TaskId = taskId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void Delete(int taskId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sTaskDelete",
                    new { TaskId = taskId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void Update(int taskId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sTaskUpdate",
                    new { TaskId = taskId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void AssignPerson(int taskId, int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sAssignPersonTask",
                    new { TaskId = taskId, UserId = userId },
                    commandType: CommandType.StoredProcedure);
            }
        }
    } 
}
