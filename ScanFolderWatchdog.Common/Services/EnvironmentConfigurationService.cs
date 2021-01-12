using System;

namespace ScanFolderWatchdog.Common.Services
{
    public class EnvironmentConfigurationService : IConfigurationService
    {
        public string GetSetting(string key)
        {
            return Environment.GetEnvironmentVariable(key)?.ToString();
        }
    }
}
