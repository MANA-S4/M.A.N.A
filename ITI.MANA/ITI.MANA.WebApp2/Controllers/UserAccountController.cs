using ITI.MANA.WebApp.Authentification;
using ITI.MANA.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.MANA.WebApp.Authentification;
using ITI.MANA.DAL;
using ITI.MANA.WebApp.Models.AccountViewModels;
using ITI.MANA.WebApp.Services;
using System.Security.Claims;

namespace ITI.MANA.WebApp.Controllers
{
    [Route("api/users")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class UserAccountController : Controller
    {
        readonly UserService _userService;
        readonly TokenService _tokenService;

        public UserAccountController (UserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost]
        public void UpdateUser(string email, string lastName, string firstName, DateTime birthDate, byte[] password)
        {
            if (password == null)
            {
                _userService.UpdateUserInfo(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), email, lastName, firstName, birthDate);
            }
            else
            {
                _userService.UpdateUserComplete(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), email, lastName, firstName, birthDate, password);
            }
        }

        [HttpGet("get")]
        public IActionResult FindUser()
        {
            Result<User> result = _userService.FindUserById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return this.CreateResult<User, UserAccountViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToUserAccountViewModel();
                o.RouteName = "";
                o.RouteValues = s => new { id = s.UserId };
            });
        }
    }
}
