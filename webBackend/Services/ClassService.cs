using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using webBackend.Models;
using webBackend.Models.Class;
using webBackend.Models.Group;
using webBackend.Models.Result;
using webBackend.Models.User;

namespace webBackend.Services
{
    public interface IClassService
    {
        Task<Class> GetById(string id);
        Task<Class> Create(ClassModel classModel);
        Task<ClassUpdateModel> Update(string id, ClassUpdateModel p);
        Task<Class> Delete(string id);
        UserInformationResult GetListClassById(string userId);
    }
    public class ClassService : IClassService
    {
        private readonly IMongoCollection<Class> _classes;
        private readonly IMongoCollection<Groups> _groups;
        private readonly IMongoCollection<webBackend.Models.User.User> _users;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;

        public ClassService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _classes = database.GetCollection<Class>(settings.ClassesCollectionName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);
            _groups = database.GetCollection<Groups>(settings.GroupsCollectionName);
            _mapper = mapper;
        }
        public Task<Class> GetById(string id)
        {
            return _classes.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Class> Create(ClassModel classModel)
        {
            List<webBackend.Models.User.User> users = new List<webBackend.Models.User.User>();

            foreach (string userId in classModel.TeacherId)
            {
                users.Add(_users.Find(u => u.Id == userId).FirstOrDefault());
            }
            var c = new Class();
            c.Name = classModel.Name;
            c.No = classModel.No;
            c.TeacherId = users;
            _classes.InsertOne(c);
            return c;

        }
        public async Task<ClassUpdateModel> Update(string id, ClassUpdateModel p)
        {
            var classs = await GetById(id);
            _mapper.Map(p, classs);
            await _classes.ReplaceOneAsync(p => p.Id == id, classs);
            return p;
        }
        public async Task<Class> Delete(string id)
        {
            var classs = await GetById(id);
            await _classes.DeleteOneAsync(p => p.Id == id);
            return classs;
        }
        public UserInformationResult GetListClassById(string userId)
        {
            var user = _users.Find(u => u.Id == userId).FirstOrDefault();

            var groups = _groups.Find(g => true).ToList();
            List<Groups> Listgroups = new List<Groups>();
            foreach (Groups item in groups)
            {
                for (int i = 0; i < item.ListUser.Count; i++)
                {
                    if (item.ListUser[i].Id == userId)
                    {
                        Listgroups.Add(item);
                    }
                }
            }
            var groupResults = _mapper.Map<List<Groups>, List<GroupResult>>(Listgroups);
            List<string> classId = new List<string>();
            foreach (Groups item in Listgroups)
            {
                classId.Add(item.ClassId);
            }
            List<ClassResult> classResults = new List<ClassResult>();
            foreach(string id in classId)
            {
                Class c = _classes.Find(c => c.Id == id).FirstOrDefault();
                var temp = _mapper.Map<ClassResult>(c);
                classResults.Add(temp);
            }
            UserInformationResult userInformation = new UserInformationResult();
            userInformation.Id = user.Id;
            userInformation.UserName = user.Username;
            userInformation.FirstName = user.FirstName;
            userInformation.LastName = user.LastName;
            userInformation.ClassResults = classResults;
            userInformation.GroupResults = groupResults;
            return userInformation;


    }
        

    }
}
