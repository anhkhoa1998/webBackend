using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webBackend.Models;
using webBackend.Models.Answer;

namespace webBackend.Services
{
    public interface IAnswerService
    {
        Task<Answern> GetById(string id);
        Task<Answern> Create(AnswerModel answerModel);
        Task<AnswerUpdateModel> Update(string id, AnswerUpdateModel p);
        Task<Answern> Delete(string id);
        List<Answern> GetListByIssueId(string id);
    }
    public class AnswerService : IAnswerService
    {
        private readonly IMongoCollection<Answern> _answers;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;

        public AnswerService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _answers = database.GetCollection<Answern>(settings.AnswersCollectionName);
            _mapper = mapper;
        }
        public Task<Answern> GetById(string id)
        {
            return _answers.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Answern> Create(AnswerModel answerModel)
        {
            var answer = _mapper.Map<Answern>(answerModel);
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
        public async Task<Answern> Delete(string id)
        {
            var answer = await GetById(id);
            await _answers.DeleteOneAsync(p => p.Id == id);
            return answer;
        }
        public List<Answern> GetListByIssueId(string id)
        {
            List<Answern> answerns = _answers.Find(x => x.IssueId == id).ToList();
            return answerns;
        }
    }
}
