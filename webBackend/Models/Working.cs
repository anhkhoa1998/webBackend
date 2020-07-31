using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace webBackend.Models
{
    public class Working
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ClassId { get; set; }
        public List<string> Todo { get; set; }
        public List<WorkingModel> Work { get; set; }

    }
    public class WorkingModel
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Content { get; set; }
        public List<string> ListUser { get; set; }

    }
    public class Work
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Content { get; set; }
        public List<string> ListUser { get; set; }
    }
    public class WorkingModelResult
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Content { get; set; }
        public bool IsSubmit { get; set; }
    }
}
