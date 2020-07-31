using Microsoft.AspNetCore.Http;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace webBackend.Models
{
   
    public class Imagemodel
    {
        public IFormFile[] Files { get; set; }
    }

    public class Image
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public List<string> Pictures { get; set; }
    }
}
