using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webBackend.Models.Lesson
{
    public class Lesson
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ClassId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public List<Question> Questions { get; set; }
      

    }
    public class Question
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public List<Answer> Answers { get; set; }
    }
    public class Answer
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
    }
}
