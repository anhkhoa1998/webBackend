using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webBackend.Models;
using webBackend.Models.Project;

namespace webBackend.Services
{
    public interface IProjectService
    {
        Task<Project> GetById(string id);
        Task<Project> GetByUserid(string userId);
        Task<Project> Create(string userId, Project _project);
    }
    public class ProjectService : IProjectService
    {
        private readonly IMongoCollection<Project> _projects;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;

        public ProjectService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _projects = database.GetCollection<Project>(settings.ProjectsCollectionName);
            _mapper = mapper;
        }

        public Task<Project> GetById(string id)
        {
            var project = _projects.Find(p => p.Id == id).FirstOrDefaultAsync();
            return project;
        }

        public Task<Project> GetByUserid(string userId)
        {
            var project = _projects.Find(p => p.userId == userId).FirstOrDefaultAsync();
            return project;
        }
        public async Task<Project> Create(string userId, Project _project)
        {
            var project = await GetByUserid(userId);
            _project.userId = userId;
            if(project != null)
            {
                await _projects.DeleteOneAsync(p => p.Id == project.Id);
            }
            await _projects.InsertOneAsync(_project);

            return project;
        }
    }
}
