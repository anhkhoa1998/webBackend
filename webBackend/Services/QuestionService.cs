using AutoMapper;
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
    public interface IQuestionService
    {

        Task<Question> Create(QuestionModel answerModel);
        List<QuestionResult> GetById(string lessonId);
        List<Question> Get();
        ResultQuestion CountQuestion(string id);

    }
    public class QuestionService :IQuestionService
    {
        private readonly IMongoCollection<Question> _question;
        private readonly IMongoCollection<Groups> _group;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;
        public QuestionService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _question = database.GetCollection<Question>(settings.QuestionCollectionName);
            _group = database.GetCollection<Groups>(settings.GroupsCollectionName);
            _mapper = mapper;
        }
        public async Task<Question> Create(QuestionModel answerModel)
        {
            Question question = new Question();
            question.LessonId = answerModel.LessonId;
            question.Content = answerModel.Content;
            question.GroupId = answerModel.GroupId;
            question.CreateAt = DateTime.Now;
            _question.InsertOne(question);
            return question;
        }
     public   List<Question> Get()
        {
            return _question.Find(b => true).ToList();
        }
        public List<QuestionResult> GetById(string lessonId)
        {
            
           var question = _question.Find(q => q.LessonId == lessonId).ToList();
            
            List<QuestionResult> questionResult = new List<QuestionResult>();
            foreach(Question item in question)
            {
                var gourp = _group.Find(g => g.Id == item.GroupId).FirstOrDefault();
                QuestionResult temp = new QuestionResult();
                temp.Id = item.Id;
                temp.Content = item.Content;
                temp.Answers = item.Answers;
                temp.LessonId = item.LessonId;
                temp.CreateAt = item.CreateAt;
                temp.GroupName = gourp.Name;
                questionResult.Add(temp);
            }
            
           
            return questionResult;
        }
        public ResultQuestion CountQuestion(string id)
        {
            ResultQuestion result = new ResultQuestion();         
            var questions = _question.Find(q => q.GroupId == id).ToList();
            result.Count = questions.Count;
            var group = _group.Find(g => g.Id == id).FirstOrDefault();
            result.Name = group.Name;
            return result;
        }
    }
}
