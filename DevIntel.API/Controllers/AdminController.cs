using DevIntel.Application.DTO;
using DevIntel.Application.Interfaces;
using DevIntel.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevIntel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AdminController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("promote")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PromoteUser([FromBody] PromoteUserDto dto)
        {
            var result = await _authService.PromoteUserAsync(dto);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
