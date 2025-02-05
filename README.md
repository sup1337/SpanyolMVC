# **QUIZ APPLICATION**

This application is an engaging vocabulary quiz designed to help users practice Spanish translations using Hungarian and English words. It dynamically selects a random set of words from the database, presents them to the user in a quiz format, and evaluates their performance, providing an interactive and educational experience.
## Technologies Used

### ASP\.NET Core
- **Framework**: The application is built using ASP\.NET Core, a cross-platform framework for building modern, cloud-based, internet-connected applications.
- **MVC Pattern**: Utilizes the Model-View-Controller \(MVC\) pattern to separate concerns and manage the application's structure.

### Entity Framework Core
- **ORM**: Entity Framework Core \(EF Core\) is used as the Object-Relational Mapper \(ORM\) to interact with the database.
- **Database Contexts**: Two database contexts are used:
  - `AuthDbContext` for managing user authentication and authorization.
  - `SpanishDbContext` for managing the vocabulary words and quiz data.

### Identity
- **User Management**: ASP\.NET Core Identity is used for managing user accounts, roles, and authentication.
- **Email Confirmation**: Users are required to confirm their email addresses before they can participate in quizzes.

### Dependency Injection
- **Service Registration**: Services such as repositories and email senders are registered in the `Program.cs` file for dependency injection.

### Razor Pages
- **Views**: Razor Pages are used to create dynamic web pages with C# and HTML.

## Features

### User Management
- **Registration and Login**: Users can register and log in to the application.
- **Roles**: Users are assigned roles such as `Admin`, `Manager`, and `User`.
- **Role Management**: Admins can manage user roles through the `AdminController`.

### Email Handling
- **Email Confirmation**: Users receive an email to confirm their account upon registration.
- **Email Sending**: The `EmailSender` service is used to send emails.

### Quizzes
- **Quiz Creation**: Users can start a quiz by specifying the number of questions.
- **Random Questions**: The application selects random words from the database for the quiz.
- **Evaluation**: User answers are evaluated, and results are displayed.

### File Upload
- **Word Import**: Admins can upload files to bulk insert vocabulary words into the database.
- **File Handling**: The uploaded file is processed and the data is inserted into the database.


### Views
- **Admin**: Views for managing user roles.
- **Quiz**: Views for creating and evaluating quizzes.
- **Words**: Views for managing vocabulary words.

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- SQL Server

### Setup
1. Clone the repository.
2. Configure the connection strings in `appsettings.json`.
3. Apply migrations and update to both database and start the application
