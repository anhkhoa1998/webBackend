using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webBackend.Models;
using webBackend.Models.Answer;
using webBackend.Models.Group;
using webBackend.Models.Question;
using webBackend.Models.Schedule;

namespace webBackend.Services
{
    public interface IScheduleService
    {
        GroupShedule Create(GroupSheduleModel groupSheduleModel);
        void Delete(string groupSheduleModel);


    }
    public class ScheduleService: IScheduleService
    {
        private readonly IMongoCollection<GroupShedule> _groupShedule;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;
        public ScheduleService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _groupShedule = database.GetCollection<GroupShedule>(settings.ScheduleCollectionName);
            _mapper = mapper;
        }
        public GroupShedule Create(GroupSheduleModel groupSheduleModel)
        {
            GroupShedule groupShedule = _groupShedule.Find(g => g.GroupId == groupSheduleModel.GroupId).FirstOrDefault();
            GroupShedule shedule = new GroupShedule();
            shedule.GroupId = groupSheduleModel.GroupId;
            shedule.sheduleDeatails = groupSheduleModel.sheduleDeatails;
            if (groupShedule==null)
            {
                
                _groupShedule.InsertOne(shedule);
                return shedule;
            }
            _groupShedule.DeleteOne(g=>g.Id==groupShedule.Id);
            _groupShedule.InsertOne(shedule);
            return shedule;
        }
        public void Delete(string groupSheduleModel)
        {
            _groupShedule.DeleteOne(g => g.Id == groupSheduleModel);

        }
    }
}
