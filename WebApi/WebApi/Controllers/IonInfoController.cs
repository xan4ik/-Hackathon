using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using WebApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
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

        [HttpGet]
        public IActionResult GetIonShortInfo() 
        {
            var result = _service.GetIonShortInfo();
            return Ok(result);
        }
    }
}
