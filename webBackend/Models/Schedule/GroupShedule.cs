using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace webBackend.Models.Schedule
{
    public class GroupShedule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
       public string GroupId { get; set; }
      public  List<SheduleDeatail> sheduleDeatails { get; set; }
     
    }
    public class SheduleDeatail
        {
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
    public class GroupSheduleModel
    {
        public List<SheduleDeatail> sheduleDeatails { get; set; }
        public string GroupId { get; set; }
       
    }
}
