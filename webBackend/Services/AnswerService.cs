using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webBackend.Models;
using webBackend.Models.Answer;
using webBackend.Models.Class;
using webBackend.Models.Group;
using webBackend.Models.Lesson;
using webBackend.Models.Question;
using webBackend.Models.User;

namespace webBackend.Services
{
    public interface IAnswerService
    {
        Task<Answer> GetById(string id);
        Task<Answer> Create(AnswerModel answerModel, string userId);
        Task<AnswerUpdateModel> Update(string id, AnswerUpdateModel p);
        Task<Answer> Delete(string id);
        List<Answer> GetListByIssueId(string id);
        ResultAnswers CountAnser(string groupid);
    }
    public class AnswerService : IAnswerService
    {
        private readonly IMongoCollection<Answer> _answers;
        private readonly IMongoCollection<Question> _questions;
        private readonly IMongoCollection<Groups> _group;
        private readonly IMongoCollection<Class> _classes;
        private readonly IMongoCollection<Lesson> _lessons;
        private readonly IMongoCollection<User> _users;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;

        public AnswerService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _answers = database.GetCollection<Answer>(settings.AnswersCollectionName);
            _group = database.GetCollection<Groups>(settings.GroupsCollectionName);
            _questions = database.GetCollection<Question>(settings.QuestionCollectionName);
            _classes = database.GetCollection<Class>(settings.ClassesCollectionName);
            _lessons = database.GetCollection<Lesson>(settings.LessonsCollectionName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);
            _mapper = mapper;
        }
        public Task<Answer> GetById(string id)
        {
            return _answers.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Answer> Create(AnswerModel answerModel,string userId)
        {
            var user = _users.Find(u => u.Id == userId).FirstOrDefault();
            var _question = _questions.Find(l => l.Id == answerModel.QuestionId).FirstOrDefault();
            var lesson = _lessons.Find(l => l.Id == _question.LessonId).FirstOrDefault();
            var group = _group.Find(g => g.ListUser.Contains(user) && g.ClassId == lesson.ClassId).FirstOrDefault();
            Answer answer = new Answer();
            answer.Content = answerModel.Content;
            answer.GroupId = group.Id;
            answer.CreateAt = DateTime.Now;
            await _answers.InsertOneAsync(answer);
            Question question = new Question();
            question= _questions.Find(q => q.Id == answerModel.QuestionId).FirstOrDefault();
            if(question.Answers==null)
            {
                question.Answers = new List<Answer>();
            }
           
            question.Answers.Add(answer);
            _questions.ReplaceOne(q => q.Id == answerModel.QuestionId, question);
            return answer;
        }
        public async Task<AnswerUpdateModel> Update(string id, AnswerUpdateModel p)
        {
            var answer = await GetById(id);
            _mapper.Map(p, answer);
            await _answers.ReplaceOneAsync(p => p.Id == id, answer);
            return p;
        }
        public async Task<Answer> Delete(string id)
        {
            var answer = await GetById(id);
            await _answers.DeleteOneAsync(p => p.Id == id);
            return answer;
        }
        public List<Answer> GetListByIssueId(string id)
        {
            List<Answer> answerns = _answers.Find(x =>true).ToList();
            return answerns;
        }
        public ResultAnswers CountAnser(string groupid)
        {
            ResultAnswers result = new ResultAnswers();
            var ans = _answers.Find(q => q.GroupId == groupid).ToList();
            result.Count = ans.Count;
            var group = _group.Find(g => g.Id == groupid).FirstOrDefault();
            result.Name = group.Name;
            return result;
        }
    }
}
