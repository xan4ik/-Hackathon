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
