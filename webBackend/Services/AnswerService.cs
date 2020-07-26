using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webBackend.Models;
using webBackend.Models.Answer;
using webBackend.Models.Issue;
using webBackend.Models.ResultModel;
using webBackend.Models.User;

namespace webBackend.Services
{
    public interface IAnswerService
    {
        Task<Answer> GetById(string id);
        Task<Answer> Create(AnswerModel answerModel);
        Task<AnswerUpdateModel> Update(string id, AnswerUpdateModel p);
        Task<Answer> Delete(string id);
        List<Answer> GetListByIssueId(string id);
        List<QandAModel> GetListQuestionAndAnswer(string chapterId);
    }
    public class AnswerService : IAnswerService
    {
        private readonly IMongoCollection<Answer> _answers;
        private readonly IMongoCollection<Issue> _issues;
        private readonly IMongoCollection<User> _users;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;

        public AnswerService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _answers = database.GetCollection<Answer>(settings.AnswersCollectionName);
            _issues = database.GetCollection<Issue>(settings.IssuesCollectionName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);
            _mapper = mapper;
        }
        public Task<Answer> GetById(string id)
        {
            return _answers.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Answer> Create(AnswerModel answerModel)
        {
            var user = _users.Find(u => u.Id == answerModel.UserId).FirstOrDefault();
            answerModel.UserName = user.FirstName + ' ' + user.LastName;
            var answer = _mapper.Map<Answer>(answerModel);
            await _answers.InsertOneAsync(answer);
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
            List<Answer> answerns = _answers.Find(x => x.IssueId == id).ToList();
            return answerns;
        }
        public List<QandAModel> GetListQuestionAndAnswer(string chapterId)
        {
            var listResult = new List<QandAModel>();
            var issues = _issues.Find(i => i.ChapterId == chapterId).ToList();
            foreach(var item in issues)
            {
                var listAnswers = _answers.Find(a => a.IssueId == item.Id).ToList();
                listResult.Add(new QandAModel { issue = item, Answers = listAnswers });
            }

            return listResult;
        }
    }
}
