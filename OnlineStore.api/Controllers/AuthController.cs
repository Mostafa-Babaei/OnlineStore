using Application.Common.Model;
using infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineStore.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginApi user)
        {
            if (user is null)
            {
                return BadRequest("Invalid client request");
            }
            if (user.UserName == "string" && user.Password == "string")
            {
                if (configuration["Jwt:Key"] == null)
                    return Unauthorized();

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(tokenString);
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public ApiResult Registe([FromBody] LoginApi user)
        {
            return ApiResult.ToSuccessModel("");
        }

        [HttpPost("forget-password")]
        public ApiResult ForgetPassword([FromBody] LoginApi user)
        {
            return ApiResult.ToSuccessModel("");
        }

        [HttpPost("change-password")]
        public ApiResult ChangePassword([FromBody] LoginApi user)
        {
            return ApiResult.ToSuccessModel("");
        }


    }
}
