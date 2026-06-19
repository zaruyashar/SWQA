SWQA Dashboard - Softito Backend Project #4
A lightweight, high-performance Quality Assurance admin dashboard developed as the 4th project for the Softito Backend face-to-face training program. This application provides a streamlined interface for tracking software bugs and managing test environments.

📌 Project Overview
This project demonstrates backend fundamentals using C# and .NET. The architecture is built around two independent database entities managed via a clean, Bootstrap-powered admin panel. It strictly utilizes ADO.NET for raw database interactions and CRUD operations, while leveraging LINQ for complex data aggregation in the reporting modules.

🚀 Key Features
Admin Panel: Fully functional dashboard for managing QA data.

CRUD Operations: Complete Create, Read, Update, and Delete capabilities for all records.

Independent Entities: Manages two unrelated database tables (BugReport and TestEnvironment).

Advanced Reporting: One-click XML report generation (e.g., Bug Frequencies, OS Distribution, API Bugs) using LINQ queries.

Global Search: Dynamic search functionality spanning across multiple tables with highlighted keyword results.

🛠️ Technologies & Architecture
Language: C#

Framework: ASP.NET Core (Razor Pages)

Data Access: ADO.NET (Core CRUD) & LINQ (Reporting)

Database: SQL Server (Code-First Approach)

Frontend: HTML5, Bootstrap 5, SB Admin Template

📸 Screenshots

Dashboard Overview: 
<img width="3033" height="1453" alt="1" src="https://github.com/user-attachments/assets/1b64a95c-a617-47f7-9d3e-989e5466472d" />


Test Environments Grid:
<img width="3035" height="1460" alt="2" src="https://github.com/user-attachments/assets/f1223957-4acf-42f0-b35b-f2d434777007" />


Data Entry / Form Handling:
<img width="3028" height="1458" alt="3" src="https://github.com/user-attachments/assets/5a4de372-0d80-4125-a9e0-849e5a6d41aa" />


Admin Reports (XML Export):
<img width="3037" height="1460" alt="4" src="https://github.com/user-attachments/assets/b3f758a0-5860-4fcf-ae75-3df2c44aa3f7" />


⚙️ Setup & Installation
Clone the repository.

Update the conString (Connection String) in the PageModels to point to your local SQL Server instance.

Run the application via Visual Studio or the .NET CLI (dotnet run).
