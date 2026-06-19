# SWQA Dashboard - Softito Backend Project #4

A lightweight, high-performance Quality Assurance admin dashboard developed as the 4th project for the Softito Backend face-to-face training program. This application provides a streamlined interface for tracking software bugs and managing test environments.

## 📌 Project Overview

This project demonstrates backend fundamentals using C# and .NET. The architecture is built around two independent database entities managed via a clean, Bootstrap-powered admin panel. It strictly utilizes ADO.NET for raw database interactions and CRUD operations, while leveraging LINQ for complex data aggregation in the reporting modules.

## 🚀 Key Features

* **Admin Panel:** Fully functional dashboard for managing QA data.
* **CRUD Operations:** Complete Create, Read, Update, and Delete capabilities for all records.
* **Independent Entities:** Manages two unrelated database tables (`BugReport` and `TestEnvironment`).
* **Advanced Reporting:** One-click XML report generation (e.g., Bug Frequencies, OS Distribution, API Bugs) using LINQ queries.
* **Global Search:** Dynamic, multi-table search functionality utilizing SQL `LIKE` queries. Results are cleanly categorized by their source tables and feature real-time keyword highlighting for better UX.

## 🛠️ Technologies & Architecture

* **Language:** C#
* **Framework:** ASP.NET Core (Razor Pages)
* **Data Access:** ADO.NET (Core CRUD & Search) & LINQ (Reporting)
* **Database:** SQL Server (Code-First Approach)
* **Frontend:** HTML5, Bootstrap 5, SB Admin Template

## 📸 Screenshots

* **Dashboard Overview:**
<img width="3033" height="1453" alt="1" src="https://github.com/user-attachments/assets/49eebed7-0570-462a-acb1-d10541470324" />

  
* **Test Environments Grid:**
<img width="3035" height="1460" alt="2" src="https://github.com/user-attachments/assets/c71690ee-de74-4db6-8f50-a9ec1d03b8ba" />

  
* **Data Entry / Form Handling:**
<img width="3028" height="1458" alt="3" src="https://github.com/user-attachments/assets/a06a2f2b-600d-46d1-845d-b082b1f92095" />

  
* **Admin Reports (XML Export):**
<img width="3037" height="1460" alt="4" src="https://github.com/user-attachments/assets/fa8ebbcb-84ab-4b54-b024-8279dde19f37" />

  
* **Global Search & Highlighting:**
<img width="3033" height="1451" alt="5" src="https://github.com/user-attachments/assets/7ca21751-5c33-490b-bd84-a5860fc5db76" />


## ⚙️ Setup & Installation

1. Clone the repository.
2. Update the `conString` (Connection String) in the PageModels to point to your local SQL Server instance.
3. Run the application via Visual Studio or the .NET CLI (`dotnet run`).
