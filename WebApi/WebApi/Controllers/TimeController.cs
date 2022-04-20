using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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


        [HttpGet("session_report")]
        public IActionResult GetSessionReport(int sessionNumber)
        {
            var result = _service.GetSessionReport(sessionNumber);
            return Ok(result);
        }

        [HttpGet("session_begin")]
        public IActionResult GetSessionBegin(int sessionNumber)
        {
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

        [HttpGet("contracts_timework")]
        public IActionResult GetContractTimeWorksByIonName(string ionName) 
        {
            var result = _service.GetContractTimeWorksByIonName(ionName);
            return Ok(result);
        }
    }
}
