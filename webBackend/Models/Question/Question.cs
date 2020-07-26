using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace webBackend.Models.Question
{
    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string LessonId { get; set; }
        public string GroupId { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public List<webBackend.Models.Answer.Answer> Answers { get; set; }
        public Question()
        {
            this.Answers = new List<webBackend.Models.Answer.Answer>();
        }
    }
    public class QuestionModel
    {
        public string LessonId { get; set; }
        public string Content { get; set; }
        public string ClassId { get; set; }
     
    }
    public class QuestionResult
    {
        
        public string Id { get; set; }
        public string LessonId { get; set; }
        public string GroupName { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public List<webBackend.Models.Answer.Answer> Answers { get; set; }
       
    }
    public class ResultQuestion
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
    public class ResultAnswers
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
