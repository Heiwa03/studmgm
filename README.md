# Student Management System for the Technical University of Moldova

## Overview

This project is a Student Management System designed for the Technical University of Moldova. It allows for the management of faculties and students, including enrollment, graduation, and various queries related to students and faculties.

## Features

- **Create a new faculty**: Add a new faculty to the system.
- **Search for a faculty by student email**: Find which faculty a student belongs to using their email.
- **Display all faculties**: List all faculties in the system.
- **Display faculties by study field**: List faculties filtered by their study field.
- **Create and assign a student to a faculty**: Add a new student and assign them to a faculty.
- **Graduate a student from a faculty**: Graduate a student from their faculty.
- **Display all enrolled students**: List all students currently enrolled in faculties.
- **Display all graduates**: List all students who have graduated.
- **Check if a student belongs to a faculty**: Verify if a student is part of a specific faculty.
- **Batch enroll students from CSV**: Enroll multiple students from a CSV file.
- **Graduate students from email file**: Graduate multiple students using a file containing their emails.

## Getting Started

### Prerequisites

- .NET SDK 8.0
- A text editor or IDE (e.g., Visual Studio Code)

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/student-management-system.git
    ```
2. Navigate to the project directory:
    ```sh
    cd student-management-system
    ```
3. Build the project:
    ```sh
    dotnet build
    ```

### Running the Application

To run the application, use the following command:
```sh
dotnet run