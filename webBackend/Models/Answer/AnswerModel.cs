using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webBackend.Models.Answer
{
    public class AnswerModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string IssueId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public string Content { get; set; }
    }
}
