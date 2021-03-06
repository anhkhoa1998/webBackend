﻿using MongoDB.Bson;
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
        public string Code { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string TeacherId { get; set; }
    }
}
