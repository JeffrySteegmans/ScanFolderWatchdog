using System.Collections.Generic;

namespace ScanFolderWatchdog.Common.Services
{
    public interface INotificationService
    {
        void Send(string toAddress, string toName, string subject, string message, string attachment);
    }
}
