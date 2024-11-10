
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.JwtHelpers;
using Microsoft.AspNetCore.Authorization;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings jwtSettings;
        public AccountController(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        private IEnumerable<Users> Users = new List<Users>()
        {
            new Users()
            {
                Id = Guid.NewGuid(),
                EmailId = "kh@hotmail.com",
                UserName ="Admin",
                Password="Admin",
            },
            new Users()
            {
                Id = Guid.NewGuid(),
                EmailId = "kh@hotmail.com",
                UserName ="khaled",
                Password="123@123",
            }
        };



        [HttpPost]
        public IActionResult Login(UserLogins userLogins)
        {
            //login action
            try
            {
                var Token = new UserTokens();
                var Valid = Users.Any(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                if (Valid)
                {
                    var user = Users.FirstOrDefault(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                    // create token for this user
                    Token = JwtHelpers.GenTokenkey(new UserTokens()
                    {
                        EmailId = user.EmailId,
                        GuidId = Guid.NewGuid(),
                        UserName = user.UserName,
                        Id = user.Id,

                    }, jwtSettings);
                }
                else
                {
                    return BadRequest($"wrong password");
                }
                return Ok(Token);
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetList()
        {
            return Ok(Users);
        }





    }
}
