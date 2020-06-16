using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace webBackend.Models.Class
{
    public class ClassModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string TeacherId { get; set; }
    }
}
