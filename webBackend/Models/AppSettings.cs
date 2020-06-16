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
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string Secret { get; set; }
    }
    public interface IAppSettings
    {
        string UsersCollectionName { get; set; }
        string ClassesCollectionName { get; set; }
        string AnswersCollectionName { get; set; }
        string IssuesCollectionName { get; set; }
        string LessonsCollectionName { get; set; }
        string ChaptersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string Secret { get; set; }
    }
}
