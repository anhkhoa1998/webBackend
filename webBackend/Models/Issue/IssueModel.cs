using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webBackend.Models.Issue
{
    public class IssueModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ClassId { get; set; }
        public string Question { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
    }
}
