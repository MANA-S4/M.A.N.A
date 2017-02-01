using ITI.MANA.WebApp.Authentification;
using ITI.MANA.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ITI.MANA.DAL;
using ITI.MANA.WebApp.Models.AccountViewModels;
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

        [HttpPut]
        public void UpdateUser([FromBody] UserAccountViewModel model)
        {
            if (model.Password == null)
            {
                _userService.UpdateUserInfo(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), model.Email, model.LastName, model.FirstName, model.BirthDate);
            }
            else
            {
                _userService.UpdateUserComplete(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), model.Email, model.LastName, model.FirstName, model.BirthDate, model.Password);
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
