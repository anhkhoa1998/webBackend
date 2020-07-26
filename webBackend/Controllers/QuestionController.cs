using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webBackend.Models.Question;
using webBackend.Services;

namespace webBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _questionService;
        private readonly GroupService _groupService;

        public QuestionController(QuestionService questionService,GroupService groupService)
        {
            _questionService = questionService;
            _groupService = groupService;
        }

        [HttpGet("create")]
        public async Task<Question> Create([FromQuery] QuestionModel Model)
        {
            return await _questionService.Create(Model);
        }
        [HttpPost("get_list_question")]
        public IActionResult GetByLessonId(string id)
        {
            return Ok( _questionService.GetById(id));
        }
        [HttpGet("count-question")]
        public IActionResult GetCount(string groupId)
        {
            if(_groupService.Get(groupId)==null)
            {
                return BadRequest("Can't find group");
            }
            return Ok(_questionService.CountQuestion(groupId));
        }
    }
}