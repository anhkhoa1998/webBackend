using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using webBackend.Models.User;

namespace webBackend.Models.Group
{
    public class Groups
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ClassId { get; set; }
        public List<webBackend.Models.User.User> ListUser { get; set; }
        public Groups() { this.ListUser = new List<webBackend.Models.User.User>(); }
        
    }
}
