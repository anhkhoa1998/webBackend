using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webBackend.Models.Class
{
    public class ClassUpdateModel
    {
        public string Name { get; set; }
        public string No { get; set; }
        public List<string> UsersId { get; set; }
        public string TeacherId { get; set; }
    }
}
