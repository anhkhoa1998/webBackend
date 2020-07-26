using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webBackend.Models.Chapter
{
    public class Chapter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Theory { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int No { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string LessonId { get; set; }
    }
}
