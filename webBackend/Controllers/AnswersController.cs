using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webBackend.Models.Answer;
using webBackend.Services;

namespace webBackend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly AnswerService _answerService;
        private readonly GroupService _groupService;

        public AnswersController(AnswerService answerService, GroupService groupService)
        {
            _answerService = answerService;
            _groupService = groupService;
        }

        [HttpPost("create")]
        public async Task<Answern> Create([FromQuery] AnswerModel answerModel,string id)
        {
            return await _answerService.Create(answerModel,id);
        }
        [HttpGet("get")]
        public async Task<Answer> Get(string id)
        {
            var answer = await _answerService.GetById(id);
            return answer;
        }
        [HttpPut("update")]
        public async Task<AnswerUpdateModel> Update(string id, AnswerUpdateModel answerUpdate)
        {
            var answer = await _answerService.Update(id, answerUpdate);
            return answer;
        }
        [HttpDelete("delete")]
        public async Task<Answer> Delete(string id)
        {
            var answer = await _answerService.Delete(id);
            return answer;
        }
        [HttpGet("get-list-answer")]
        public IActionResult GetByLessonId(string id)
        {
            var answerns = _answerService.GetListQuestionAndAnswer(chapterId);
            return Ok(answerns);
        }
        [HttpGet("count-answer")]
        public IActionResult GetCount(string groupId)
        {
            if (_groupService.Get(groupId) == null)
            {
                return BadRequest("Can't find group");
            }
            return Ok(_answerService.CountAnser(groupId));
        }
    }
}
