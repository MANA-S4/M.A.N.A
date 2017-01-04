using ITI.MANA.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MANA.WebApp.Services
{
    public class TaskService
    {
        readonly TaskGateway _taskGateway;

        public TaskService(TaskGateway taskGateway)
        {
            _taskGateway = taskGateway;
        }

        public Result<DAL.Task> GetById(int taskId)
        {
            DAL.Task task;
            if ((task = _taskGateway.FindById(taskId)) == null) return Result.Failure<DAL.Task>(Status.NotFound, "Task not found.");
            return Result.Success(Status.Ok, task);
        }

        public Result<IEnumerable<DAL.Task>> GetAll(int taskId)
        {
            return Result.Success(Status.Ok, _taskGateway.GetAll(taskId));
        }

        public Result<DAL.Task> CreateContact(int taskId)
        {
            _taskGateway.Create(taskId);
            DAL.Task task = _taskGateway.FindById(taskId);
            return Result.Success(Status.Created, task);
        }

        public Result<DAL.Task> UpdateTask(int taskId)
        {
            DAL.Task task;
            if ((task = _taskGateway.FindById(taskId)) == null)
            {
                return Result.Failure<DAL.Task>(Status.NotFound, "Task not found.");
            }

            {
                DAL.Task c = _taskGateway.FindById(taskId);
                if (c != null && c.TaskId != task.TaskId) return Result.Failure<DAL.Task>(Status.BadRequest, "A task with this name already exists.");
            }

            _taskGateway.Update(taskId);
            task = _taskGateway.FindById(taskId);
            return Result.Success(Status.Ok, task);
        }

        public Result<int> DeleteTask(int taskId)
        {
            if (_taskGateway.FindById(taskId) == null) return Result.Failure<int>(Status.NotFound, "Task not found");
            _taskGateway.Delete(taskId);
            return Result.Success(Status.Ok, taskId);
        }

        //bool IsMailValid(string email) => !string.IsNullOrWhiteSpace(email);
    }
}
