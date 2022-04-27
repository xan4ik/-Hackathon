using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentDataController : ControllerBase
    {
        private IDocumentService _service;

        public DocumentDataController(IDocumentService service)
        {
            _service = service;
        }


        [HttpGet("standart/{id:int}")]
        public IActionResult GetStandartDocumentData(int id)
        {
            if (_service.ContainsInfo(id))
            {
                return Ok(_service.GetStandartDoumentData(id));
            }
            return BadRequest();
        }

        [HttpGet("nonstandart/{id:int}")]
        public IActionResult GetNonStandartDocumentData(int id)
        {
            if (_service.ContainsInfo(id))
            {
                return Ok(_service.GetNonStandartDoumentData(id));
            }
            return BadRequest();
        }

        [HttpGet("allowance/{id:int}")]
        public IActionResult GetAllowanceDocumentData(int id)
        {
            if (_service.ContainsInfo(id))
            {
                return Ok(_service.GetAllowanceDoumentData(id));
            }
            return BadRequest();
        }

    }
}
