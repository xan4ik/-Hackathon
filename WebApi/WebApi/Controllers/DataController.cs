using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Human")]
    public class DataController : ControllerBase
    { 
        private IDataEntityService _service;

        public DataController(IDataEntityService service)
        {
            _service = service;
        }
    }
}
