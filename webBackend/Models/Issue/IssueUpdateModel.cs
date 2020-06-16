using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webBackend.Models.Issue
{
    public class IssueUpdateModel
    {
        public string Question { get; set; }
    }
}
