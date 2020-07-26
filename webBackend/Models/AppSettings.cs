namespace webBackend.Models
{
    public class AppSettings : IAppSettings
    {
        public string UsersCollectionName { get; set; }
        public string ClassesCollectionName { get; set; }
        public string AnswersCollectionName { get; set; }
        public string IssuesCollectionName { get; set; }
        public string LessonsCollectionName { get; set; }
        public string ChaptersCollectionName { get; set; }
        public string ProjectsCollectionName { get; set; }
        public string GroupsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string Secret { get; set; }
        public string ScheduleCollectionName { get; set; }
        public string QuestionCollectionName { get; set; }
    }
    public interface IAppSettings
    {
        string QuestionCollectionName { get; set; }
        string ScheduleCollectionName { get; set; }
        string GroupsCollectionName { get; set; }
        string UsersCollectionName { get; set; }
        string ClassesCollectionName { get; set; }
        string AnswersCollectionName { get; set; }
        string IssuesCollectionName { get; set; }
        string LessonsCollectionName { get; set; }
        string ChaptersCollectionName { get; set; }
        string ProjectsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string Secret { get; set; }
    }
}
