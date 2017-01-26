using ITI.MANA.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITI.MANA.WebApp.Services
{
    public class TaskService
    {
        readonly TaskGateway _taskGateway;

        public TaskService(TaskGateway taskGateway)
        {
            _taskGateway = taskGateway;
        }

        public Result<Task> GetById(int taskId)
        {
            Task task;
            if ((task = _taskGateway.FindById(taskId)) == null) return Result.Failure<Task>(Status.NotFound, "Task not found.");
            return Result.Success(Status.Ok, task);
        }

        public Result<IEnumerable<Task>> GetAll(int userId)
        {
            return Result.Success(Status.Ok, _taskGateway.GetAll(userId));
        }

        public Result<Task> CreateTask(string taskName, int userId, DateTime taskDate, int attributeTo)
        {
            _taskGateway.Create(taskName, userId, taskDate, attributeTo);
            Task task = _taskGateway.FindTask(userId, taskName);
            return Result.Success(Status.Created, task);
        }

        public Result<Task> UpdateTask(int taskId, string taskName, DateTime taskDate,int attributeTo)
        {
            Task task;
            if ((task = _taskGateway.FindById(taskId)) == null)
            {
                return Result.Failure<Task>(Status.NotFound, "Task not found.");
            }

            {
                Task c = _taskGateway.FindById(taskId);
                if (c != null && c.TaskId != task.TaskId) return Result.Failure<Task>(Status.BadRequest, "A task with this name already exists.");
            }

            _taskGateway.Update(taskId, taskName, taskDate, attributeTo);
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
