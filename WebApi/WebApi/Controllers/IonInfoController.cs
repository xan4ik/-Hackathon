using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Services;

namespace WebAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IonInfoController : ControllerBase
    {
        private IIonInfoService _service;

        public IonInfoController(IIonInfoService service)
        {
            _service = service;
        }

        [HttpGet("short_info")]
        public IActionResult GetIonShortInfo()
        {
            var result = _service.GetIonShortInfo();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetIonNames() 
        {
            var result = _service.GetIonNames();
            return Ok(result);
        }

    }
}
