using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webBackend.Models.Group;
using webBackend.Services;

namespace webBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly GroupService _groupService;
        public GroupsController(GroupService group)
        {
            _groupService = group;
        }
        [HttpPost]
        public IActionResult Create(GroupAdd group)
        {
            if(string.IsNullOrEmpty(group.Name)|| string.IsNullOrEmpty(group.ClassId)||group.IdUser.Count==0)
            {
                return BadRequest("input null!!!!");
            }
            _groupService.Create(group);
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var group = _groupService.GetById(id);
            if(group==null)
            {
                return NotFound();
            }
                _groupService.Delete(id);
            return Ok();
        }
        [HttpDelete("user")]
        public IActionResult DeleteUser(string id,string groupId)
        {
            if (_groupService.GetById(groupId) == null)
            {
                return NotFound();
            }
            if (_groupService.DeleteUser(id, groupId) == null)
            {
                return BadRequest("User Not Found");
            }
            return Ok();
        }
        [HttpGet("{id}")]

        public IActionResult GetById(string id)
        {
            var group = _groupService.GetById(id);
            if(group==null)
            {
                return NotFound();
            }
            return Ok(group);
        }
        [HttpGet]

        public IActionResult Get()
        {
            var group = _groupService.Get();
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }
        [HttpPut("InsertUser")]
        public IActionResult InsertUser(string id, string GroupId)
        {
            if(_groupService.GetById(GroupId)==null)
            {
                return NotFound();
            }
           if( _groupService.InsertUser(id, GroupId)==null)
            {
                return BadRequest("User Exists");
            }
            return Ok();
        }
    }
}