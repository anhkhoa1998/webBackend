using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [HttpPost]
        public IActionResult Create(LessonModel lessonModel)
        {
            return Ok(_lessonService.Create(lessonModel));
        }
        [HttpPost("create-question")]
        public IActionResult CreateQuestion(string LessonId,string Content)
        {
            var userId = string.Empty;

            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                userId = identity.FindFirst(ClaimTypes.Name)?.Value;

            }
            return Ok(_lessonService.CreateQuestion(LessonId, userId, Content));
        }
        [HttpPost("create-answer")]
        public IActionResult CreateAnswer(string LessonId,int QuestionId, string Content)
        {
            var userId = string.Empty;

            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                userId = identity.FindFirst(ClaimTypes.Name)?.Value;

            }
            return Ok(_lessonService.CreateAnswer(LessonId, QuestionId, userId, Content));
        }
        //[HttpGet("get")]
        //public async Task<Lesson> Get(string id)
        //{
        //    var lesson = await _lessonService.GetById(id);
        //    return lesson;
        //}
        //[HttpPut("update")]
        //public async Task<LessonUpdateModel> Update(string id, LessonUpdateModel lessonUpdate)
        //{
        //    var lesson = await _lessonService.Update(id, lessonUpdate);
        //    return lesson;
        //}
        //[HttpDelete("delete")]
        //public async Task<Lesson> Delete(string id)
        //{
        //    var lesson = await _lessonService.Delete(id);
        //    return lesson;
        //}
        //[HttpGet("getlistlesson")]
        //public ActionResult GetListByClassId(string id)
        //{
        //    List<Lesson> lessons =  _lessonService.GetByClassId(id);
        //    return Ok(lessons);
        //}
    }
}
