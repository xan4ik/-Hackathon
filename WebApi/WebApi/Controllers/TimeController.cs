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

    //TODO: доделать инфу по последнему сеансу

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimeController : ControllerBase
    {
        private ITimeEntityService _service;

        public TimeController(ITimeEntityService service)
        {
            _service = service;
        }

        [HttpGet("ion_time")]
        public IActionResult CountTotalTimeByIon()
        {
            var result = _service.CountTotalTimeByIon();
            return Ok(result);
        }


        [HttpGet("session_report/{sessionNumber:int}")]
        public IActionResult GetSessionReport(int sessionNumber)
        {
            if (_service.HasNotSession(sessionNumber)) 
            {
                return BadRequest();
            }

            var result = _service.GetSessionReport(sessionNumber);
            return Ok(result);
        }

        [HttpGet("session_begin/{sessionNumber:int}")]
        public IActionResult GetSessionBegin(int sessionNumber)
        {
            if (_service.HasNotSession(sessionNumber)) 
            {
                return BadRequest();
            }

            var result = _service.GetSessionBegin(sessionNumber);
            return Ok(result);
        }

        [HttpGet("contracts_begin")]
        public IActionResult GetContractsBegin()
        {
            var result = _service.GetContractsBegin();
            return Ok(result);
        }

        [HttpGet("totalTB")]
        public IActionResult GetTBTotalTime()
        {
            var result = _service.GetTBTotalTime();
            return Ok(result);
        }

        [HttpGet("contracts_timework/{ionName}")]
        public IActionResult GetContractTimeWorksByIonName(string ionName) 
        {
            if (_service.HasNotIon(ionName)) 
            {
                return BadRequest();
            }

            var result = _service.GetContractTimeWorksByIonName(ionName);
            return Ok(result);
        }

        [HttpGet("session_count")]
        public IActionResult GetSessionCount() 
        {
            var result = _service.GetSessionCount();
            return Ok(result);
        }
    }
}
