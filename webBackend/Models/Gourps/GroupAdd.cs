using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using webBackend.Models.User;

namespace webBackend.Models.Group
{
    public class GroupAdd
    {
  

        public string Name { get; set; }
        public List<string> IdUser { get; set; }
        public GroupAdd() { this.IdUser = new List<string>(); }
    }
}
