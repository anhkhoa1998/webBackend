using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webBackend.Models;
using webBackend.Models.Answer;
using webBackend.Models.Group;
using webBackend.Models.Question;

namespace webBackend.Services
{
    public interface IAnswerService
    {
        Task<Answern> GetById(string id);
        Task<Answern> Create(AnswerModel answerModel, string questionId);
        Task<AnswerUpdateModel> Update(string id, AnswerUpdateModel p);
        Task<Answern> Delete(string id);
        List<Answern> GetListByIssueId(string id);
        ResultAnswers CountAnser(string groupid);
    }
    public class AnswerService : IAnswerService
    {
        private readonly IMongoCollection<Answern> _answers;
        private readonly IMongoCollection<Question> _question;
        private readonly IMongoCollection<Groups> _group;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;

        public AnswerService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _answers = database.GetCollection<Answern>(settings.AnswersCollectionName);
            _group = database.GetCollection<Groups>(settings.GroupsCollectionName);
            _question = database.GetCollection<Question>(settings.QuestionCollectionName);
            _mapper = mapper;
        }
        public Task<Answern> GetById(string id)
        {
            return _answers.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Answern> Create(AnswerModel answerModel,string questionId)
        {
            Answern answer = new Answern();
            answer.Content = answerModel.Content;
            answer.GroupId = answerModel.GroupId;
            answer.CreateAt = DateTime.Now;
            await _answers.InsertOneAsync(answer);
            Question question = new Question();
            question= _question.Find(q => q.Id == questionId).FirstOrDefault();
            if(question.Answers==null)
            {
                question.Answers = new List<Answern>();
            }
           
            question.Answers.Add(answer);
            _question.ReplaceOne(q => q.Id == questionId, question);
            return answer;
        }
        public async Task<AnswerUpdateModel> Update(string id, AnswerUpdateModel p)
        {
            var answer = await GetById(id);
            _mapper.Map(p, answer);
            await _answers.ReplaceOneAsync(p => p.Id == id, answer);
            return p;
        }
        public async Task<Answern> Delete(string id)
        {
            var answer = await GetById(id);
            await _answers.DeleteOneAsync(p => p.Id == id);
            return answer;
        }
        public List<Answern> GetListByIssueId(string id)
        {
            List<Answern> answerns = _answers.Find(x =>true).ToList();
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
