using ITI.MANA.WebApp.Models.TaskViewModels;
using ITI.MANA.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ITI.MANA.WebApp.Authentification;
using ITI.MANA.DAL;

namespace ITI.MANA.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class TaskController : Controller
    {
        readonly TaskService _taskService;
        readonly UserService _userService;

        public TaskController(TaskService taskService, UserService userService)
        {
            _taskService = taskService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetTaskList()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<IEnumerable<Task>> result = _taskService.GetAll(userId);
            return this.CreateResult<IEnumerable<Task>, IEnumerable<TaskViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToTaskViewModel());
            });
        }

        [HttpGet("{taskId}", Name = "GetTask")]
        public IActionResult GetTaskById(int taskId)
        {
            Result<Task> result = _taskService.GetById(taskId);
            return this.CreateResult<Task, TaskViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToTaskViewModel();
            });
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskViewModel model)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            User attributeToUserId = _userService.FindUser(model.AttributeTo);
            Result<Task> result = _taskService.CreateTask(model.TaskName, userId, model.TaskDate, attributeToUserId.UserId);
            return this.CreateResult<Task, TaskViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToTaskViewModel();
                o.RouteName = "";
                o.RouteValues = s => new { id = s.TaskId };
            });
        }

        [HttpPut("{taskId}")]
        public IActionResult UpdateTask(int taskId, string taskName, [FromBody] TaskViewModel model)
        {
            Result<Task> result = _taskService.UpdateTask(taskId, model.TaskName, model.TaskDate);
            return this.CreateResult<Task, TaskViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToTaskViewModel();
            });
        }

        [HttpDelete("{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            Result<int> result = _taskService.DeleteTask(taskId);
            return this.CreateResult(result);
        }
    }
}
