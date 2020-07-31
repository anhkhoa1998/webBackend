using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webBackend.Models;
using webBackend.Models.Group;
using webBackend.Services;

namespace webBackend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WorkingController : ControllerBase
    {
        private readonly WorkingService _working;
        public WorkingController(WorkingService group)
        {
            _working = group;
        }
        [HttpPost]
        public IActionResult Create(List<string> Todo, string ClassId)
        {


            return Ok(_working.Creaate(Todo,ClassId));
        }
        [HttpPost("WorkingModel")]
        public IActionResult CreateWorkingModel(string WorkingId, Work work)
        {


            return Ok(_working.CreaateWorkingModel(WorkingId, work));
        }
        [HttpGet]
        public IActionResult Get(string WorkingId, int WorkingModelId)
        {
            var userId = string.Empty;

            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                userId = identity.FindFirst(ClaimTypes.Name)?.Value;

            }
            return Ok(_working.WorkingModelResult(userId,
                WorkingId, WorkingModelId));
        }
    }
}