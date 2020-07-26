using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using webBackend.Models.Answer;
using System.Collections.Generic;

namespace webBackend.Models.Issue
{
    public class Issue
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ChapterId { get; set; }
        public string Question { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public string UserName { get; set; }
      //  public List<Answern> Answers { get; set; }
    }
}
