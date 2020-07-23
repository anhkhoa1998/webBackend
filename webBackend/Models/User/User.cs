using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace webBackend.Models.User
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AccountType Type { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public List<string> ListClass { get; set; }
    }
    public enum AccountType
    {
        Student,
        Teacher
    }
}
