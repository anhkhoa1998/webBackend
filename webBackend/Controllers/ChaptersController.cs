using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webBackend.Models.Chapter;
using webBackend.Services;

namespace webBackend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly ChapterService _chapterService;

        public ChaptersController(ChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpPost("create")]
        public async Task<Chapter> Create([FromQuery] ChapterModel chapterModel)
        {
            return await _chapterService.Create(chapterModel);
        }
        [HttpGet("get")]
        public async Task<Chapter> Get(string id)
        {
            var chapter = await _chapterService.GetById(id);
            return chapter;
        }
        [HttpPut("update")]
        public async Task<ChapterUpdateModel> Update(string id, ChapterUpdateModel chapterUpdate)
        {
            var chapter = await _chapterService.Update(id, chapterUpdate);
            return chapter;
        }
        [HttpDelete("delete")]
        public async Task<Chapter> Delete(string id)
        {
            var chapter = await _chapterService.Delete(id);
            return chapter;
        }
        [HttpGet("getlist")]
        public IActionResult GetListByLessonId(string id)
        {
            List<Chapter> list = _chapterService.GetListByLessonId(id);
            return Ok(list);
        }
    }
}
