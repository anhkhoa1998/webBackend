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

        Groups Create(GroupAdd group);
        Groups DeleteUser(string userID, string groupId);
        void Delete(string id);
        List<Groups> Get();
        Groups GetById(string id);
        Groups InsertUser(string UserId, string GroupId);
        void Update(string id,Groups groups);
    }
    public class GroupService : IGroupService
    {
        private readonly IMongoCollection<Groups> _group;
            private readonly IMongoCollection<Users> _users;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;
        public GroupService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _group = database.GetCollection<Groups>(settings.GroupsCollectionName);
            _users = database.GetCollection<Users>(settings.UsersCollectionName);
            _mapper = mapper;
        }
        public Groups Create(GroupAdd group)
        {
            List<Users> users = new List<Users>();
            
            foreach(string userId in group.IdUser)
            {
                users.Add(_users.Find(u => u.Id == userId).FirstOrDefault());
            }
            var groups = new Groups();
            groups.ListUser = users;
            _group.InsertOne(groups);
            return groups;
        }
        public Groups InsertUser(string UserId, string GroupId)
        {
            Groups groups = this.GetById(GroupId);
            if(!groups.ListUser.Any(x=>x.Id==UserId))
            {
                Users users = _users.Find(u => u.Id == UserId).FirstOrDefault();
                groups.ListUser.Add(users);
                this.Update(groups.Id, groups);
                return groups;

            }
            return null;

        }
        public Groups DeleteUser(string userID, string groupId)
        {
            Groups groups = this.GetById(groupId);
            if (groups.ListUser.Any(x => x.Id == userID))
            {
                Users users = _users.Find(u => u.Id == userID).FirstOrDefault();
                groups.ListUser.Remove(users);
                this.Update(groups.Id, groups);
                return groups;

            }
            return null;
        }
        public void Update(string id,Groups groups) =>   _group.ReplaceOne(p => p.Id == id, groups);
      
        public void Delete(string id) => _group.DeleteOne(g => g.Id == id);
      
        public List<Groups> Get()
        {
            var listGroups = _group.Find(x => true).ToList();
            return listGroups;
        }
        public Groups GetById(string id)
        {
            var group = _group.Find(x => x.Id == id).FirstOrDefault();
            return group;
        }
    }
}
