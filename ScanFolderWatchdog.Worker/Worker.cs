using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ScanFolderWatchdog.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ScanFolderWatchdog.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfigurationService _configurationService;

        public Worker(ILogger<Worker> logger, IConfigurationService configurationService)
        {
            _logger = logger;
            _configurationService = configurationService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string folderToWatch = _configurationService.GetSetting("FOLDER_TO_WATCH");
            int delay = _configurationService.GetSettingOrDefault("DELAY_IN_SECONDS", 1);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}, folder to watch: {folder}, delay: {delay} seconds", DateTimeOffset.Now, folderToWatch, delay);
                await Task.Delay(delay * 1000, stoppingToken);
            }
        }
    }
}
