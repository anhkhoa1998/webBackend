using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using webBackend.Services;

namespace webBackend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImgsController : ControllerBase
    {
        private readonly StoreImage _imgService;

        public ImgsController(StoreImage imgService)
        {
            _imgService = imgService;
        }
        [HttpPost]
        public ActionResult AddImage(IFormFile file)
        {

            var apiRep = _imgService.UploadedFile(file);

            return Ok(apiRep);
        }

        [HttpGet]
        public ActionResult GetImage(string id)
        {

            var apiRep =  _imgService.DownloadFile(id);

            return Ok(apiRep);
        }
    }
}