using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webBackend.Models.Answer;
using webBackend.Models.Issue;
using webBackend.Services;

namespace webBackend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IssueService _issueService;
        private readonly AnswerService _answerService;

        public IssuesController(IssueService issueService, AnswerService answerService)
        {
            _issueService = issueService;
            _answerService = answerService;
        }

        [HttpPost("create")]
        public async Task<Issue> Create([FromQuery] IssueModel issueModel)
        {
            var userId = string.Empty;
            var role = string.Empty;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                userId = identity.FindFirst(ClaimTypes.Name)?.Value;
                role = identity.FindFirst(ClaimTypes.Role)?.Value;
            }
            issueModel.UserId = userId;
            return await _issueService.Create(issueModel);
        }
        [HttpGet("get")]
        public async Task<Issue> Get(string id)
        {
            var issue = await _issueService.GetById(id);
            return issue;
        }
        [HttpPut("update")]
        public async Task<IssueUpdateModel> Update(string id, IssueUpdateModel issueUpdate)
        {
            var issue = await _issueService.Update(id, issueUpdate);
            return issue;
        }
        [HttpDelete("delete")]
        public async Task<Issue> Delete(string id)
        {
            var issue = await _issueService.Delete(id);
            return issue;
        }
        [HttpGet("get-list-issue")]
        public IActionResult GetByChapterID(string id)
        {
            List<Issue> issues = _issueService.GetByChapterID(id);
            return Ok(issues);
        }
        [HttpPost("create-answer")]
        public async Task<IActionResult> CreateAnswer(string issueId,string conten)
        {
            AnswerModel answern = new AnswerModel();
            var issue = _issueService.GetId(issueId);
            if(issue==null)
            {
                return BadRequest();
            }
            answern.Content = conten;
            answern.IssueId = issue.Id;
            answern.UserId = issue.UserId;
            await _answerService.Create(answern);
            return Ok(answern);

        }

    }
}
