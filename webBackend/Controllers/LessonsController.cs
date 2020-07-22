using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webBackend.Models.Lesson;
using webBackend.Services;

namespace webBackend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly LessonService _lessonService;

        public LessonsController(LessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpPost("create")]
        public async Task<Lesson> Create([FromQuery] LessonModel lessonModel)
        {
            return await _lessonService.Create(lessonModel);
        }
        [HttpGet("get")]
        public async Task<Lesson> Get(string id)
        {
            var lesson = await _lessonService.GetById(id);
            return lesson;
        }
        [HttpPut("update")]
        public async Task<LessonUpdateModel> Update(string id, LessonUpdateModel lessonUpdate)
        {
            var lesson = await _lessonService.Update(id, lessonUpdate);
            return lesson;
        }
        [HttpDelete("delete")]
        public async Task<Lesson> Delete(string id)
        {
            var lesson = await _lessonService.Delete(id);
            return lesson;
        }
        [HttpGet("getlistlesson")]
        public ActionResult GetListByClassId(string id)
        {
            List<Lesson> lessons =  _lessonService.GetByClassId(id);
            return Ok(lessons);
        }
    }
}
