using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ITI.MANA.DAL
{
    public class TaskGateway
    {
        readonly string _connectionString;

        public TaskGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Task> GetAll(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Task>(
                    @"select t.TaskId, t.TaskName, t.TaskDate, t.IsFinish, t.UserId, u.email from iti.Tasks t join iti.users u on u.userId = t.attributeTo where t.UserId = @UserId or t.AttributeTo = @UserId;",
                    new { UserId = userId });
            }
        }

        public Task FindById(int taskId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Task>(
                        @"select t.TaskId
                          from iti.Tasks t
                          where t.Taskid = @TaskId;",
                        new { TaskId = taskId })
                    .FirstOrDefault();
            }
        }

        public Task FindTask(int userId, string taskName)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Task>(
                        @"select t.TaskName, t.UserId
                          from iti.Tasks t
                          where t.UserId = @UserId and t.TaskName = @TaskName;",
                        new { UserId = userId, TaskName = taskName })
                    .FirstOrDefault();
            }
        }

        public void Create(string taskName, int userId, DateTime taskDate, int attributeTo)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sTaskCreate",
                    new { TaskName = taskName, UserId = userId, TaskDate = taskDate, AttributeTo = attributeTo },
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

        public void Update(int taskId, string taskName, DateTime taskDate, int attributeTo)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "iti.sTaskUpdate",
                    new { TaskId = taskId, TaskName = taskName, TaskDate = taskDate, AttributeTo = attributeTo },
                    commandType: CommandType.StoredProcedure);
            }
        }
    } 
}
