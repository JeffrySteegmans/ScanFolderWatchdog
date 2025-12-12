# ScanFolderWatchdog

A .NET background service that monitors a folder for new files and automatically emails them as attachments. Perfect for automated document processing, scan workflows, or any scenario where files need to be automatically sent via email.

## Features

- 🔍 **Folder Monitoring**: Continuously watches a designated folder for new files
- 📧 **Email Notifications**: Automatically sends files as email attachments via SMTP
- 📁 **File Management**: Moves processed files to a dedicated `processed` subfolder
- ⚙️ **Configurable**: All settings configured via environment variables
- 🐳 **Docker Ready**: Runs seamlessly in Docker containers
- ⏱️ **Customizable Polling**: Adjustable scan interval (default: 1 second)

## Requirements

- .NET 5.0 Runtime (for local execution)
- Docker (for containerized deployment)
- SMTP server credentials for email functionality

## Configuration

The application is configured entirely through environment variables:

### Email Configuration

| Variable | Description | Required |
|----------|-------------|----------|
| `EMAIL_HOST` | SMTP server hostname (e.g., `smtp.gmail.com`) | Yes |
| `EMAIL_PORT` | SMTP server port (e.g., `587` or `465`) | Yes |
| `EMAIL_USERNAME` | SMTP authentication username | Yes |
| `EMAIL_PASSWORD` | SMTP authentication password | Yes |
| `EMAIL_TO_ADDRESS` | Recipient email address | Yes |
| `EMAIL_TO_NAME` | Recipient display name | Yes |
| `EMAIL_SUBJECT` | Email subject line | Yes |
| `EMAIL_MESSAGE` | Email body text | Yes |

### Application Configuration

| Variable | Description | Default |
|----------|-------------|---------|
| `DELAY_IN_SECONDS` | Interval between folder scans (in seconds) | `1` |

## Usage

### Running with Docker (Recommended)

1. **Create an environment file** (`settings.env`):
```env
EMAIL_HOST=smtp.example.com
EMAIL_PORT=587
EMAIL_USERNAME=your-username
EMAIL_PASSWORD=your-password
EMAIL_TO_ADDRESS=recipient@example.com
EMAIL_TO_NAME=Recipient Name
EMAIL_SUBJECT=New Scanned Document
EMAIL_MESSAGE=Please find the attached scanned document.
DELAY_IN_SECONDS=5
```

2. **Build the Docker image**:
```bash
docker build -t scanfolderwatchdog .
```

3. **Run the container**:
```bash
docker run -d \
  --name scanfolderwatchdog \
  --env-file settings.env \
  -v /path/to/local/folder:/scans \
  scanfolderwatchdog
```

Replace `/path/to/local/folder` with the actual path to the folder you want to monitor.

### Running Locally

1. **Set environment variables** (Windows PowerShell example):
```powershell
$env:EMAIL_HOST="smtp.example.com"
$env:EMAIL_PORT="587"
# ... set other variables
```

2. **Build and run**:
```bash
cd ScanFolderWatchdog.Worker
dotnet run
```

## How It Works

1. The service monitors the `/scans` folder at regular intervals (configurable via `DELAY_IN_SECONDS`)
2. When new files are detected:
   - Each file is sent as an email attachment to the configured recipient
   - The file is moved to `/scans/processed` subfolder
3. The process repeats continuously until the service is stopped

## Project Structure

```
ScanFolderWatchdog/
├── ScanFolderWatchdog.Worker/       # Main background service application
├── ScanFolderWatchdog.Common/       # Shared services and interfaces
│   └── Services/                    # Configuration and notification services
├── ScanFolderWatchdog.Notification.Email/  # Email notification implementation
├── Dockerfile                       # Docker container configuration
└── README.md                        # This file
```

## Technology Stack

- **.NET 5.0**: Cross-platform framework
- **Worker Service**: Background service template
- **MailKit**: SMTP email client library
- **Docker**: Containerization platform

## Development

### Prerequisites
- Visual Studio 2019/2022 or Visual Studio Code
- .NET 5.0 SDK

### Building the Solution
```bash
dotnet build ScanFolderWatchdog.sln
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Author

Copyright (c) 2021 Jeffry Steegmans

## Support

For issues, questions, or contributions, please open an issue on the GitHub repository. 
