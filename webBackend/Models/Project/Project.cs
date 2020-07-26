using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webBackend.Models.Project
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public List<Todo> Todo { get; set; }
        public List<Todo> Doing { get; set; }
        public List<Todo> Done { get; set; }
        public string userId { get; set; }
    }
}
