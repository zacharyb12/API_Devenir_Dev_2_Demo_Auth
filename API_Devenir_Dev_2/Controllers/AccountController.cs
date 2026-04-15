using Application_Devenir_Dev_2.DTOS;
using Application_Devenir_Dev_2.Services.Interfaces;
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
    public class AccountController(IAccountService _service) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Login(LoginForm loginRequest)
        {
            try
            {
                string? token = await _service.Login(loginRequest);

                if(token == null)
                {
                    return BadRequest();
                }

                return Ok(token);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
