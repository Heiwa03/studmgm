using System;
using System.Collections.Generic;

namespace UniversityManagement
{
    public class Menu
    {
        private List<Faculty> faculties = new List<Faculty>();
        private SaveManager saveManager = new SaveManager();
        private Logger logger = new Logger("application.log");

        public Menu()
        {
            faculties = saveManager.LoadState();
            logger.LogInfo("Application started and state loaded.");
        }

        public void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Create a new faculty");
                Console.WriteLine("2. Search for a faculty by student email");
                Console.WriteLine("3. Display all faculties");
                Console.WriteLine("4. Display faculties by study field");
                Console.WriteLine("5. Create and assign a student to a faculty");
                Console.WriteLine("6. Graduate a student from a faculty");
                Console.WriteLine("7. Display all enrolled students");
                Console.WriteLine("8. Display all graduates");
                Console.WriteLine("9. Check if a student belongs to a faculty");
                Console.WriteLine("10. Exit");
                Console.Write("Enter your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateFaculty();
                        break;
                    case "2":
                        SearchFacultyByEmail();
                        break;
                    case "3":
                        DisplayAllFaculties();
                        break;
                    case "4":
                        DisplayFacultiesByField();
                        break;
                    case "5":
                        CreateAndAssignStudentToFaculty();
                        break;
                    case "6":
                        GraduateStudentFromFaculty();
                        break;
                    case "7":
                        DisplayAllEnrolledStudents();
                        break;
                    case "8":
                        DisplayAllGraduates();
                        break;
                    case "9":
                        CheckIfStudentBelongsToFaculty();
                        break;
                    case "10":
                        saveManager.SaveState(faculties);
                        logger.LogInfo("Application state saved and exited.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        logger.LogWarning("Invalid menu choice entered.");
                        break;
                }
            }
        }

        private void CreateFaculty()
        {
            string name = GetValidatedInput("Enter faculty name: ", "Faculty name cannot be empty. Please enter a valid name.");
            string abbreviation = GetValidatedInput("Enter faculty abbreviation: ", "Faculty abbreviation cannot be empty. Please enter a valid abbreviation.");

            Console.WriteLine("Select study field:");
            foreach (var field in Enum.GetValues(typeof(StudyField)))
            {
                Console.WriteLine($"{(int)field}. {field}");
            }
            Console.Write("Enter the number corresponding to the study field: ");
            string? input = Console.ReadLine();
            int studyFieldChoice;

            while (!int.TryParse(input, out studyFieldChoice))
            {
                Console.Write("Invalid input. Please enter a valid number corresponding to the study field: ");
                input = Console.ReadLine();
            }

            StudyField studyField = (StudyField)studyFieldChoice;

            Faculty newFaculty = new Faculty(name, abbreviation, studyField);
            faculties.Add(newFaculty);
            Console.WriteLine("Faculty created successfully.");
            logger.LogInfo($"Faculty created: {name} ({abbreviation}), Study Field: {studyField}");
        }

        private void SearchFacultyByEmail()
        {
            string email = GetValidatedInput("Enter student email: ", "Email cannot be empty. Please enter a valid email.");

            foreach (var faculty in faculties)
            {
                foreach (var student in faculty.Students)
                {
                    if (student.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Student belongs to faculty: {faculty.Name}");
                        logger.LogInfo($"Student found: {student.FirstName} {student.LastName} in faculty {faculty.Name}");
                        return;
                    }
                }
            }

            Console.WriteLine("Student not found in any faculty.");
            logger.LogWarning($"Student not found with email: {email}");
        }

        private void DisplayAllFaculties()
        {
            Console.WriteLine("Faculties:");
            foreach (var faculty in faculties)
            {
                Console.WriteLine($"Name: {faculty.Name}, Abbreviation: {faculty.Abbreviation}, Study Field: {faculty.StudyField}");
            }
            logger.LogInfo("Displayed all faculties.");
        }

        private void DisplayFacultiesByField()
        {
            Console.WriteLine("Select study field:");
            foreach (var field in Enum.GetValues(typeof(StudyField)))
            {
                Console.WriteLine($"{(int)field}. {field}");
            }
            Console.Write("Enter the number corresponding to the study field: ");
            int studyFieldChoice = int.Parse(Console.ReadLine());
            StudyField studyField = (StudyField)studyFieldChoice;

            Console.WriteLine($"Faculties in {studyField}:");
            foreach (var faculty in faculties)
            {
                if (faculty.StudyField == studyField)
                {
                    Console.WriteLine($"Name: {faculty.Name}, Abbreviation: {faculty.Abbreviation}");
                }
            }
            logger.LogInfo($"Displayed faculties by study field: {studyField}");
        }

        private void CreateAndAssignStudentToFaculty()
        {
            // Prompt for student details
            string firstName = GetValidatedInput("Enter student's first name: ", "First name cannot be empty. Please enter a valid first name.");
            string lastName = GetValidatedInput("Enter student's last name: ", "Last name cannot be empty. Please enter a valid last name.");
            string email = GetValidatedInput("Enter student's email: ", "Email cannot be empty. Please enter a valid email.");

            Console.Write("Enter student's date of birth (yyyy-mm-dd): ");
            string dobInput = Console.ReadLine();
            DateTime dateOfBirth;

            while (!DateTime.TryParse(dobInput, out dateOfBirth))
            {
                Console.Write("Invalid date format. Please enter student's date of birth (yyyy-mm-dd): ");
                logger.LogWarning($"Invalid date format entered: {dobInput}");
                dobInput = Console.ReadLine();
            }

            DateTime enrollmentDate = DateTime.Now;

            // Create a new student
            Student newStudent = new Student(firstName, lastName, email, enrollmentDate, dateOfBirth);

            // Display faculties and prompt for selection
            Console.WriteLine("Select a faculty to assign the student to:");
            for (int i = 0; i < faculties.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {faculties[i].Name}");
            }
            Console.Write("Enter the number corresponding to the faculty: ");
            int facultyChoice = int.Parse(Console.ReadLine()) - 1;

            if (facultyChoice >= 0 && facultyChoice < faculties.Count)
            {
                // Add the student to the selected faculty
                faculties[facultyChoice].AddStudent(newStudent);
                Console.WriteLine("Student assigned to faculty successfully.");
                logger.LogInfo($"Student {firstName} {lastName} assigned to faculty {faculties[facultyChoice].Name}");
            }
            else
            {
                Console.WriteLine("Invalid faculty selection.");
                logger.LogWarning("Invalid faculty selection for student assignment.");
            }
        }

        private void GraduateStudentFromFaculty()
        {
            // Prompt for student email
            string email = GetValidatedInput("Enter student's email: ", "Email cannot be empty. Please enter a valid email.");

            // Search for the student in all faculties
            foreach (var faculty in faculties)
            {
                Student studentToGraduate = null;
                foreach (var student in faculty.Students)
                {
                    if (student.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                    {
                        studentToGraduate = student;
                        break;
                    }
                }

                if (studentToGraduate != null)
                {
                    // Graduate the student
                    faculty.GraduateStudent(studentToGraduate);
                    Console.WriteLine($"Student {studentToGraduate.FirstName} {studentToGraduate.LastName} has been graduated from {faculty.Name}.");
                    logger.LogInfo($"Student {studentToGraduate.FirstName} {studentToGraduate.LastName} graduated from faculty {faculty.Name}");
                    return;
                }
            }

            Console.WriteLine("Student not found in any faculty.");
            logger.LogWarning($"Student not found with email: {email}");
        }

        private void DisplayAllEnrolledStudents()
        {
            foreach (var faculty in faculties)
            {
                Console.WriteLine($"Faculty: {faculty.Name}");
                faculty.DisplayEnrolledStudents();
            }
            logger.LogInfo("Displayed all enrolled students.");
        }

        private void DisplayAllGraduates()
        {
            foreach (var faculty in faculties)
            {
                Console.WriteLine($"Faculty: {faculty.Name}");
                faculty.DisplayGraduatedStudents();
            }
            logger.LogInfo("Displayed all graduates.");
        }

        private void CheckIfStudentBelongsToFaculty()
        {
            // Prompt for student email
            string email = GetValidatedInput("Enter student's email: ", "Email cannot be empty. Please enter a valid email.");

            // Search for the student in all faculties
            foreach (var faculty in faculties)
            {
                foreach (var student in faculty.Students)
                {
                    if (student.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Student belongs to faculty: {faculty.Name}");
                        logger.LogInfo($"Student {student.FirstName} {student.LastName} belongs to faculty {faculty.Name}");
                        return;
                    }
                }
            }

            Console.WriteLine("Student not found in any faculty.");
            logger.LogWarning($"Student not found with email: {email}");
        }

        private string GetValidatedInput(string prompt, string warningMessage)
        {
            string? input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine(warningMessage);
                    logger.LogWarning(warningMessage);
                }
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }
    }
}