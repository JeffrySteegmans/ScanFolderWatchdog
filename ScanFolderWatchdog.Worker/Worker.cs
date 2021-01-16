using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ScanFolderWatchdog.Common.Services;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ScanFolderWatchdog.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfigurationService _configurationService;
        private readonly INotificationService _notificationService;

        public Worker(ILogger<Worker> logger, IConfigurationService configurationService, INotificationService notificationService)
        {
            _logger = logger;
            _configurationService = configurationService;
            _notificationService = notificationService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string folderToWatch = "/scans";
            string destinationFolder = Path.Combine(folderToWatch, "processed");
            int delay = _configurationService.GetSettingOrDefault("DELAY_IN_SECONDS", 1);
            EnsureFolderExist(destinationFolder);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}, folder to watch: {folder}, delay: {delay} seconds", DateTimeOffset.Now, folderToWatch, delay);

                if (Directory.Exists(folderToWatch))
                    ProcessFolder(folderToWatch, destinationFolder);

                await Task.Delay(delay * 1000, stoppingToken);
            }
        }

        private void EnsureFolderExist(string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }

        private void ProcessFolder(string folderToWatch, string destinationFolder)
        {
            var files = Directory.GetFiles(folderToWatch);
            Console.WriteLine($"\t{files.Length} files found");
            foreach (string file in files)
            {
                Console.WriteLine($"\tMail File: {file}");

                _notificationService.Send(
                    _configurationService.GetSetting("EMAIL_TO_ADDRESS"),
                    _configurationService.GetSetting("EMAIL_TO_NAME"),
                    _configurationService.GetSetting("EMAIL_SUBJECT"),
                    _configurationService.GetSetting("EMAIL_MESSAGE"),
                    file
                );
                _logger.LogInformation("\tMail Sent ({email})", _configurationService.GetSetting("EMAIL_TO_ADDRESS"));

                string fileName = Path.GetFileName(file);
                Console.WriteLine($"\tMove File ({file}) to processed folder: {Path.Combine(destinationFolder, fileName)}");

                File.Move(file, Path.Combine(destinationFolder, fileName));
            }
        }
    }
}
