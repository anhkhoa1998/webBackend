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
        Task<Answer> GetById(string id);
        Task<Answer> Create(AnswerModel answerModel);
        Task<AnswerUpdateModel> Update(string id, AnswerUpdateModel p);
        Task<Answer> Delete(string id);
    }
    public class AnswerService : IAnswerService
    {
        private readonly IMongoCollection<Answer> _answers;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;

        public AnswerService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _answers = database.GetCollection<Answer>(settings.AnswersCollectionName);
            _mapper = mapper;
        }
        public Task<Answer> GetById(string id)
        {
            return _answers.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Answer> Create(AnswerModel answerModel)
        {
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
    }
}
