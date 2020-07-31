using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webBackend.Models;
using webBackend.Models.Lesson;
using webBackend.Models.User;

namespace webBackend.Services
{
    public interface ILessonService
    {
        Task<Lesson> GetById(string id);
        Lesson Create(LessonModel lessonModel);
        Task<LessonUpdateModel> Update(string id, LessonUpdateModel p);
        Lesson CreateQuestion(string LessonId, string UserId, string Content);
        Lesson CreateAnswer(string LessonId, int QuestionId, string UserId, string Content);
        Task<Lesson> Delete(string id);
        List<Lesson> GetByClassId(string id);
    }
    public class LessonService : ILessonService
    {
        private readonly IMongoCollection<Lesson> _lesson;
        private readonly IMongoCollection<User> _user;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;

        public LessonService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _lesson = database.GetCollection<Lesson>(settings.LessonsCollectionName);
            _user = database.GetCollection<User>(settings.UsersCollectionName);
            _mapper = mapper;
        }
        public Lesson CreateQuestion(string LessonId, string UserId, string Content)
        {
            var lesson = _lesson.Find(x => x.Id == LessonId).FirstOrDefault();
            if(lesson.Questions==null)
            {
                lesson.Questions = new List<Question>();
                Question question = new Question();
                question.Id = 0;
                question.Content = Content;
                question.UserName = _user.Find(x => x.Id == UserId).FirstOrDefault().Username;
                lesson.Questions.Add(question);
            }    
            else
            {
                Question question = new Question();
                question.Id = lesson.Questions.Count;
                question.Content = Content;
                question.UserName = _user.Find(x => x.Id == UserId).FirstOrDefault().Username;
                lesson.Questions.Add(question);
            }
            _lesson.ReplaceOneAsync(p => p.Id == LessonId, lesson);
            return lesson;
        }
        public Lesson CreateAnswer(string LessonId, int QuestionId, string UserId, string Content)
        {
            var lesson = _lesson.Find(x => x.Id == LessonId).FirstOrDefault();
            foreach(Question item in lesson.Questions)
            {
                if(item.Id==QuestionId)
                {
                    if(item.Answers==null)
                    {
                        item.Answers = new List<Answer>();
                        Answer answer = new Answer();
                        answer.Content = Content;
                        answer.Id = 0;
                        answer.UserName = _user.Find(x => x.Id == UserId).FirstOrDefault().Username;
                        item.Answers.Add(answer);
                    }    
                    else
                    {
                        Answer answer = new Answer();
                        answer.Content = Content;
                        answer.Id = item.Answers.Count;
                        answer.UserName = _user.Find(x => x.Id == UserId).FirstOrDefault().Username;
                        item.Answers.Add(answer);
                    }    
                }    
            }    
            _lesson.ReplaceOneAsync(p => p.Id == LessonId, lesson);
            return lesson;
        }
        public Task<Lesson> GetById(string id)
        {
            return _lesson.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public Lesson Create(LessonModel lessonModel)
        {
            var lesson = _mapper.Map<Lesson>(lessonModel);
             _lesson.InsertOneAsync(lesson);
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
        public List<Lesson> GetByClassId(string id)
        {
            List<Lesson> list =  _lesson.Find(b => b.ClassId == id).ToList();
            return list;

        }
    }
}
