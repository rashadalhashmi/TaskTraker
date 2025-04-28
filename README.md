# TaskTracker

A modern task management application built with .NET 9.0 and Blazor, featuring real-time updates and a clean, responsive UI.

## 🚀 Features

- Real-time task updates using SignalR
- Clean and modern UI with MudBlazor components
- Task management with CRUD operations
- Responsive design for all devices
- SQLite database for data persistence
- CQRS pattern implementation with MediatR

## 🛠️ Tech Stack

- .NET 9.0
- Blazor Server
- MudBlazor 6.17.0
- Entity Framework Core
- SQLite
- SignalR
- MediatR

## 📋 Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or Visual Studio Code
- Git

## 🚀 Getting Started

1. Clone the repository:
```bash
git clone [repository-url]
```

2. Navigate to the project directory:
```bash
cd TaskTracker
```

3. Restore dependencies:
```bash
dotnet restore
```

4. Run the application:
```bash
dotnet run
```

5. Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

## 📁 Project Structure

```
TaskTracker/
├── Application/     # Application layer (commands, queries, handlers)
├── Domain/         # Domain models and interfaces
├── Infrastructure/ # Infrastructure implementations
├── Pages/          # Blazor pages
├── Components/     # Reusable Blazor components
├── Shared/         # Shared components and models
├── Services/       # Application services
├── Hubs/           # SignalR hubs
├── Data/           # Data access layer
├── Models/         # Data models
├── Repositories/   # Repository implementations
└── wwwroot/        # Static files
```

## 🔧 Configuration

The application uses SQLite as its database. The connection string is configured in `appsettings.json`. No additional configuration is required for development.

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- MudBlazor for the beautiful UI components
- .NET team for the amazing framework
- All contributors who have helped shape this project 