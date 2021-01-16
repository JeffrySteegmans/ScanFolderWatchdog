namespace ScanFolderWatchdog.Common.Services
{
    internal class MailNotificationSettings : INotificationSettings
    {
        private readonly IConfigurationService _configurationService;

        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public MailNotificationSettings(IConfigurationService configurationService)
        {
            _configurationService = configurationService;

            Host = configurationService.GetSetting("EMAIL_HOST");
            Port = int.Parse(configurationService.GetSetting("EMAIL_PORT"));
            Username = configurationService.GetSetting("EMAIL_USERNAME");
            Password = configurationService.GetSetting("EMAIL_PASSWORD");
        }
    }
}
