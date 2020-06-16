using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webBackend.Models.Issue;
using webBackend.Services;

namespace webBackend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IssueService _issueService;

        public IssuesController(IssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpPost("create")]
        public async Task<Issue> Create([FromQuery] IssueModel issueModel)
        {
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
    }
}
