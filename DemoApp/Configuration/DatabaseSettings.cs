namespace DemoApp.Configuration
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string InterestsConnectionString { get; }
        public DatabaseSettings(string interestsConnectionString)
        {
            InterestsConnectionString = interestsConnectionString;
        }
    }
}
