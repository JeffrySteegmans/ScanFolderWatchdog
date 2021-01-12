namespace ScanFolderWatchdog.Common.Services
{
    public interface IConfigurationService
    {
        string GetSetting(string key);
    }

    public static class ConfigurationServiceExtensions 
    {
        public static bool GetSettingOrDefault(this IConfigurationService configurationService, string key,
            bool defaultValue)
            => bool.TryParse(configurationService.GetSetting(key), out var result) ? result : defaultValue;


        public static int GetSettingOrDefault(this IConfigurationService configurationService, string key,
            int defaultValue)
            => int.TryParse(configurationService.GetSetting(key), out var result) ? result : defaultValue;
    }
}
