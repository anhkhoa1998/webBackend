using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webBackend.Models;
using webBackend.Models.Lesson;

namespace webBackend.Services
{
    public interface ILessonService
    {
        Task<Lesson> GetById(string id);
        Task<Lesson> Create(LessonModel lessonModel);
        Task<LessonUpdateModel> Update(string id, LessonUpdateModel p);
        Task<Lesson> Delete(string id);
    }
    public class LessonService : ILessonService
    {
        private readonly IMongoCollection<Lesson> _lesson;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;

        public LessonService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _lesson = database.GetCollection<Lesson>(settings.LessonsCollectionName);
            _mapper = mapper;
        }
        public Task<Lesson> GetById(string id)
        {
            return _lesson.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Lesson> Create(LessonModel lessonModel)
        {
            var lesson = _mapper.Map<Lesson>(lessonModel);
            await _lesson.InsertOneAsync(lesson);
            return lesson;
        }
        public async Task<LessonUpdateModel> Update(string id, LessonUpdateModel p)
        {
            var lesson = await GetById(id);
            _mapper.Map(p, lesson);
            await _lesson.ReplaceOneAsync(p => p.Id == id, lesson);
            return p;
        }
        public async Task<Lesson> Delete(string id)
        {
            var lesson = await GetById(id);
            await _lesson.DeleteOneAsync(p => p.Id == id);
            return lesson;
        }
    }
}
