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
        public string Content { get; set; }
        public string QuestionId { get; set; }
    }
}
