using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webBackend.Models.Question;
using webBackend.Services;

namespace webBackend.Controllers
{
    [Route("api/v1/[controller]")]
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

        [HttpPost("create")]
        public async Task<Question> Create([FromBody] QuestionModel Model)
        {
            var userId = string.Empty;
            var role = string.Empty;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                userId = identity.FindFirst(ClaimTypes.Name)?.Value;
                role = identity.FindFirst(ClaimTypes.Role)?.Value;
            }
            return await _questionService.Create(Model, userId);
        }
        [HttpGet("get-list-question")]
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