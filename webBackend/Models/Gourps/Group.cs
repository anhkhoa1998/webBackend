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
        public List<Users> ListUser { get; set; }
        public Groups() { this.ListUser = new List<Users>(); }
    }
}
