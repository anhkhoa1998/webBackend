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

        public AnswersController(AnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost("create")]
        public async Task<Answer> Create([FromQuery] AnswerModel answerModel)
        {
            var userId = string.Empty;
            var role = string.Empty;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                userId = identity.FindFirst(ClaimTypes.Name)?.Value;
                role = identity.FindFirst(ClaimTypes.Role)?.Value;
            }
            answerModel.UserId = userId;
            return await _answerService.Create(answerModel);
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
        [HttpGet("get-qanda")]
        public IActionResult GetQAndAModel(string chapterId)
        {
            var answerns = _answerService.GetListQuestionAndAnswer(chapterId);
            return Ok(answerns);
        }
    }
}
