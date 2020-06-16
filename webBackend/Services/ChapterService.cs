using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webBackend.Models;
using webBackend.Models.Chapter;

namespace webBackend.Services
{
    public interface IChapterService
    {
        Task<Chapter> GetById(string id);
        Task<Chapter> Create(ChapterModel chapterModel);
        Task<ChapterUpdateModel> Update(string id, ChapterUpdateModel p);
        Task<Chapter> Delete(string id);
    }
    public class ChapterService: IChapterService
    {
        private readonly IMongoCollection<Chapter> _chapters;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;

        public ChapterService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _chapters = database.GetCollection<Chapter>(settings.ChaptersCollectionName);
            _mapper = mapper;
        }
        public Task<Chapter> GetById(string id)
        {
            return _chapters.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Chapter> Create(ChapterModel chapterModel)
        {
            var chapter = _mapper.Map<Chapter>(chapterModel);
            await _chapters.InsertOneAsync(chapter);
            return chapter;
        }
        public async Task<ChapterUpdateModel> Update(string id, ChapterUpdateModel p)
        {
            var chapter = await GetById(id);
            _mapper.Map(p, chapter);
            await _chapters.ReplaceOneAsync(p => p.Id == id, chapter);
            return p;
        }
        public async Task<Chapter> Delete(string id)
        {
            var chapter = await GetById(id);
            await _chapters.DeleteOneAsync(p => p.Id == id);
            return chapter;
        }
    }
}
