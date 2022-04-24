using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class AuthController : ControllerBase
    {

        public const string BotRole = "Bot";
        public const string HumanRole = "Human";

        [HttpPost("sign_in")]
        public async Task<IActionResult> SignInAsync([FromBody] UserProfile profile) 
        {
            if (Permission.Contains(profile))
            {
                await Authenticate(profile);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("sign_out")]
        public async Task<IActionResult> SignOutAsync() 
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }


        private async Task Authenticate(UserProfile profile)
        {
            // создаем один claim
            var claims = new List<Claim>()
            {
                new Claim("Email", profile.Login),
                new Claim("ProfileType", profile.IsBot ? BotRole: HumanRole)
            };
                // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie");
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }

    public partial class AuthController 
    { 
        private static class Permission
        {
            private static readonly UserProfile[] allowedUsers;
            static Permission()
            {
                allowedUsers = new UserProfile[]
                {
                    new UserProfile{Login = "BOT", Password = "12345", IsBot = true },
                    new UserProfile{Login = "Admin", Password = "12345", IsBot =  false }
                };
            }

            public static bool Contains(UserProfile profile) 
            {
                foreach (var item in allowedUsers)
                {
                    bool areEqual = item.Login == profile.Login && item.Password == profile.Password && item.IsBot == profile.IsBot;
                    if (areEqual) 
                    {
                        return true;                        
                    }
                }

                return false;
            }
        }
    }
}
