namespace DeepFrees.Scheduler.Model
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";

        public string DatabaseName { get; set; } = "HomeStreetInvest";

        public string[] ShoppinzUsersCollectionName { get; set; } = new string[] { "Log"};
    }
}
