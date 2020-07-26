using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webBackend.Models.Answer;
using webBackend.Models.Schedule;
using webBackend.Services;

namespace webBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheduleController : ControllerBase
    {
        private readonly ScheduleService _scheduleService;

        public SheduleController(ScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
           
        }
        [HttpPost]
        public IActionResult Create(GroupSheduleModel groupSheduleModel)
        {
            return Ok(_scheduleService.Create(groupSheduleModel));
        }
        [HttpDelete]
        public IActionResult Delete(string ScheduleId)
        {
            _scheduleService.Delete(ScheduleId);
            return Ok();
        }
    }
}