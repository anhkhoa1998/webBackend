using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webBackend.Models.Class;
using webBackend.Services;

namespace webBackend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly ClassService _classService;

        public ClassesController(ClassService classService)
        {
            _classService = classService;
        }

        [HttpPost("create")]
        public async Task<Class> Create(ClassModel classModel)
        {
            return await _classService.Create(classModel);
        }

        [HttpGet("get")]
        public async Task<Class> Get(string id)
        {
            var classs = await _classService.GetById(id);
            return classs;
        }

        [HttpGet("get-list-class")]
        public async Task<List<Class>> GetListClass()
        {
            var userId = string.Empty;
            var role = string.Empty;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                userId = identity.FindFirst(ClaimTypes.Name)?.Value;
                role = identity.FindFirst(ClaimTypes.Role)?.Value;
            }

            var list = await _classService.GetListClassById(userId);
            return list;
        }

        [HttpPut("update")]
        public async Task<ClassUpdateModel> Update(string id, ClassUpdateModel classUpdate)
        {
            var classs = await _classService.Update(id, classUpdate);
            return classs;
        }

        [HttpDelete("delete")]
        public async Task<Class> Delete(string id)
        {
            var classs = await _classService.Delete(id);
            return classs;
        }

    }
}
