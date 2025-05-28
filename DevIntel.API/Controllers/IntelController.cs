using DevIntel.Application.DTO;
using DevIntel.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevIntel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [AllowAnonymous]
    public class IntelController : ControllerBase
    {
        private readonly IIntelService _intel;

        public IntelController(IIntelService intel)
        {
            _intel = intel;
        }

        // Helper to get current user’s ID from the JWT
        private Guid? CurrentUserId {
            get
            {
                var raw = User?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (Guid.TryParse(raw, out var id))
                    return id;
                return null;
            }

        }
       
      

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] string title,[FromForm] string description,[FromForm] List<string> tags,[FromForm] IFormFile? image)
        {
            // 1) Ensure we have a user
            if (CurrentUserId == null)
                return Unauthorized("No valid user token.");

            // 2) Call service with non-null Guid
            var result = await _intel.CreateFromFormAsync(
                title,
                description,
                tags,
                image,
                CurrentUserId.Value    // unwrap here
            );

            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (CurrentUserId == null)
                return Unauthorized("No valid user token.");

            var result = await _intel.GetAllAsync(CurrentUserId.Value);
            return Ok(result.Data);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (CurrentUserId == null)
                return Unauthorized("No valid user token.");

            var result = await _intel.GetByIdAsync(id, CurrentUserId.Value);
            if (!result.Success) return NotFound(result.Message);
            return Ok(result.Data);
        }


        

    }
}
