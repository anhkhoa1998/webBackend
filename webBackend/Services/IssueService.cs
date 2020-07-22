using AutoMapper;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using webBackend.Models;
using webBackend.Models.Issue;

namespace webBackend.Services
{
    public interface IIssueService
    {
        Task<Issue> GetById(string id);
        Task<Issue> Create(IssueModel issueModel);
        Task<IssueUpdateModel> Update(string id, IssueUpdateModel p);
        Task<Issue> Delete(string id);
        List<Issue> GetByChapterID(string id);
        Issue GetId(string id);
    }
    public class IssueService
    {
        private readonly IMongoCollection<Issue> _issues;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;

        public IssueService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _issues = database.GetCollection<Issue>(settings.IssuesCollectionName);
            _mapper = mapper;
        }
        public Issue GetId(string id)
        {
            return _issues.Find(x => x.Id == id).FirstOrDefault();
        }
        public Task<Issue> GetById(string id)
        {
            return _issues.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Issue> Create(IssueModel issueModel)
        {
            var issue = _mapper.Map<Issue>(issueModel);
            await _issues.InsertOneAsync(issue);
            return issue;
        }
        public async Task<IssueUpdateModel> Update(string id, IssueUpdateModel p)
        {
            var issue = await GetById(id);
            _mapper.Map(p, issue);
            await _issues.ReplaceOneAsync(p => p.Id == id, issue);
            return p;
        }
        public async Task<Issue> Delete(string id)
        {
            var issue = await GetById(id);
            await _issues.DeleteOneAsync(p => p.Id == id);
            return issue;
        }
        public List<Issue> GetByChapterID(string id)
        {
            List<Issue> list = _issues.Find(x => x.ChapterId == id).ToList();
            return list;
        }
    }
}
