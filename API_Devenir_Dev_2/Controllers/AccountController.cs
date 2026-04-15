using Application_Devenir_Dev_2.DTOS;
using Application_Devenir_Dev_2.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API_Devenir_Dev_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IOptions<JwtSettings> _jwtOptions;
        public AccountController(IOptions<JwtSettings> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        [HttpPost]
        public IActionResult Login(LoginForm loginRequest)
        {
            //Version sans db
            if(loginRequest.Login=="Mike" &&  loginRequest.Password== "test")
            {
                //Retourner le token en cas de login correct
                JwtHelpers helpers = new JwtHelpers(_jwtOptions);
                string jwttoken = helpers.GenerateJwtToken(loginRequest.Login, "Admin");

                return Ok(new { token = jwttoken });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
