using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webBackend.Models;
using webBackend.Models.Class;

namespace webBackend.Services
{
    public interface IClassService
    {
        Task<Class> GetById(string id);
        Task<Class> Create(ClassModel classModel);
        Task<ClassUpdateModel> Update(string id, ClassUpdateModel p);
        Task<Class> Delete(string id);
        Task<List<Class>> GetListClassById(List<string> IdClass);
    }
    public class ClassService : IClassService
    {
        private readonly IMongoCollection<Class> _classes;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;

        public ClassService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _classes = database.GetCollection<Class>(settings.ClassesCollectionName);
            _mapper = mapper;
        }
        public Task<Class> GetById(string id)
        {
            return _classes.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Class> Create(ClassModel classModel)
        {
            var classs = _mapper.Map<Class>(classModel);
            await _classes.InsertOneAsync(classs);
            return classs;
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
        public async Task<List<Class>> GetListClassById(List<string> IdClass)
        {
            List<Class> ListClass = new List<Class>();
            foreach(string Id in IdClass)
            {
                var classs = await GetById(Id);
                ListClass.Add(classs);
            }
            return ListClass;
        }
        

    }
}
