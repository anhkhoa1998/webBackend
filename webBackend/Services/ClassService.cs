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
            var c = new Class();
            c.Name = classModel.Name;
            c.No = classModel.No;
            c.TeacherId = classModel.TeacherId;
            c.ListUser = classModel.UsrId;
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

            var Class = _classes.Find(g =>true).ToList();
            List<ClassResult> classResults = new List<ClassResult>();
           foreach (Class item in Class)
            {
                foreach(string userid in item.ListUser)
                {
                    if(userid==userId)
                    {
                        var c = _classes.Find(x => x.Id == item.Id).FirstOrDefault();
                        ClassResult classResult = new ClassResult();
                        classResult.Name = c.Name;
                        classResult.Id = c.Id;
                        classResult.No = c.No;
                        classResults.Add(classResult);
                    }    
                }    
            }    
            UserInformationResult userInformation = new UserInformationResult();
            userInformation.Id = user.Id;
            userInformation.UserName = user.Username;
            userInformation.FirstName = user.FirstName;
            userInformation.LastName = user.LastName;
            userInformation.ListClass = classResults;
            return userInformation;


    }
        

    }
}
