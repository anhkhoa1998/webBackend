using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using webBackend.Models;
using webBackend.Models.Answer;
using webBackend.Models.Group;
using webBackend.Models.User;

namespace webBackend.Services
{
    public interface IGroupService
    {
         Working Creaate(List<string> Todo, string ClassId);
        Working CreaateWorkingModel(string Id, Work workingModels);
        WorkingModelResult WorkingModelResult(string UserId, string WorkingId, int WorkingModelId);

    }
    public class WorkingService : IGroupService
    {
        private readonly IMongoCollection<Working> _working;
        private readonly IMongoCollection<webBackend.Models.User.User> _users;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;
        public WorkingService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _working = database.GetCollection<Working>(settings.WorkingCollectionName);
            _users = database.GetCollection<webBackend.Models.User.User>(settings.UsersCollectionName);
            _mapper = mapper;
        }
        public Working Creaate(List<string> Todo, string ClassId)
        {
            var working = new Working();
            working.Todo = Todo;
            working.ClassId = ClassId;
            _working.InsertOne(working);
            return working;
        }
        public Working CreaateWorkingModel(string Id, Work workingModels)
        {
            var c = _working.Find(x => x.Id == Id).FirstOrDefault();
            if(c.Work==null)
            {
                c.Work = new List<WorkingModel>();
                var a = new WorkingModel();
                a.Content = workingModels.Content;
                a.EndTime = workingModels.EndTime;
                a.StartTime = workingModels.StartTime;
                a.Id = 0;
                a.ListUser = workingModels.ListUser;
                c.Work.Add(a);


            }
            else
            {
                var a = new WorkingModel();
                a.Content = workingModels.Content;
                a.EndTime = workingModels.EndTime;
                a.StartTime = workingModels.StartTime;
                a.Id = c.Work.Count;
                a.ListUser = workingModels.ListUser;
                c.Work.Add(a);
            }
            _working.ReplaceOne(x => x.Id == Id, c);
            return c;
        }
        public WorkingModelResult WorkingModelResult(string UserId, string WorkingId, int WorkingModelId)
        {
           var a = new WorkingModelResult();
           var c= _working.Find(x => x.Id == WorkingId).FirstOrDefault();
            foreach (WorkingModel item in c.Work)
            {
                if(item.Id== WorkingModelId)
                {
                    foreach(string id in item.ListUser)
                    {
                        if(id==UserId)
                        {
                            a.StartTime = item.StartTime;
                            a.EndTime = item.EndTime;
                            a.IsSubmit = true;
                            a.Content = item.Content;
                            a.Id = item.Id;
                            return a;
                        }
                    }
                   
                        a.StartTime = item.StartTime;
                        a.EndTime = item.EndTime;
                        a.IsSubmit = false;
                        a.Content = item.Content;
                        a.Id = item.Id;
                        return a;
                    
                }
                
            }
            return a;
        }
    }
}
