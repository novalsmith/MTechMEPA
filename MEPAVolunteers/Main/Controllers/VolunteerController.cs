using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volunteer.Interface;
using Volunteer.Model;

namespace Main.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteerController : ControllerBase
    {
        private readonly IVolunteerService _service;
        private readonly ILogger<VolunteerController> _logger;

        public VolunteerController(ILogger<VolunteerController> logger, IVolunteerService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("search")]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> SearchAsync()
        {
            var data = await _service.GetVolunteerAync();
            return Ok(data);
        }

        [HttpPost("updateById")]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateByIdAsync([FromBody] VolunteerDTO item)
        {
            var data = await _service.GetVolunteerAync();
            return Ok(data);
        }
    }
}
