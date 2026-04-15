using Application_Devenir_Dev_2.Services.Interfaces;
using Domain_Devenir_Dev_2.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Devenir_Dev_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService _service) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                User? user = await _service.GetByIdAsync(id);
                 
                if(user == null)
                {
                    return NotFound();
                }

                return Ok(user);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
