using DevIntel.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevIntel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _files;

        public FileController(IFileService files)
        {
            _files = files;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            if (file == null)
                return BadRequest("No file sent.");

            var path = await _files.SaveAsync(file);
            return Ok(new { imagePath = path });
        }
    }
}
