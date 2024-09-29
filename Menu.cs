using System;
using System.Collections.Generic;

namespace UniversityManagement
{
    public class Menu
    {
        private List<Faculty> faculties = new List<Faculty>();

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
                string choice = Console.ReadLine();

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
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void CreateFaculty()
        {
            Console.Write("Enter faculty name: ");
            string name = Console.ReadLine();
            Console.Write("Enter faculty abbreviation: ");
            string abbreviation = Console.ReadLine();
            Console.WriteLine("Select study field:");
            foreach (var field in Enum.GetValues(typeof(StudyField)))
            {
                Console.WriteLine($"{(int)field}. {field}");
            }
            Console.Write("Enter the number corresponding to the study field: ");
            int studyFieldChoice = int.Parse(Console.ReadLine());
            StudyField studyField = (StudyField)studyFieldChoice;

            Faculty newFaculty = new Faculty(name, abbreviation, studyField);
            faculties.Add(newFaculty);
            Console.WriteLine("Faculty created successfully.");
        }

        private void SearchFacultyByEmail()
        {
            Console.Write("Enter student email: ");
            string email = Console.ReadLine();

            foreach (var faculty in faculties)
            {
                foreach (var student in faculty.Students)
                {
                    if (student.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Student belongs to faculty: {faculty.Name}");
                        return;
                    }
                }
            }

            Console.WriteLine("Student not found in any faculty.");
        }

        private void DisplayAllFaculties()
        {
            Console.WriteLine("Faculties:");
            foreach (var faculty in faculties)
            {
                Console.WriteLine($"Name: {faculty.Name}, Abbreviation: {faculty.Abbreviation}, Study Field: {faculty.StudyField}");
            }
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
        }

        private void CreateAndAssignStudentToFaculty()
        {
            // Prompt for student details
            Console.Write("Enter student's first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter student's last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter student's email: ");
            string email = Console.ReadLine();
            Console.Write("Enter student's date of birth (yyyy-mm-dd): ");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());
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
            }
            else
            {
                Console.WriteLine("Invalid faculty selection.");
            }
        }

        private void GraduateStudentFromFaculty()
        {
            // Prompt for student email
            Console.Write("Enter student's email: ");
            string email = Console.ReadLine();

            // Search for the student in all faculties
            foreach (var faculty in faculties)
            {
                foreach (var student in faculty.Students)
                {
                    if (student.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                    {
                        // Graduate the student
                        faculty.GraduateStudent(student);
                    }
                }
            }

            Console.WriteLine("Student not found in any faculty.");
        }

        private void DisplayAllEnrolledStudents()
        {
            foreach (var faculty in faculties)
            {
                Console.WriteLine($"Faculty: {faculty.Name}");
                faculty.DisplayEnrolledStudents();
            }
        }

        private void DisplayAllGraduates()
        {
            foreach (var faculty in faculties)
            {
                Console.WriteLine($"Faculty: {faculty.Name}");
                faculty.DisplayGraduatedStudents();
            }
        }

        private void CheckIfStudentBelongsToFaculty() // TODO - Move this to Faculty class
        {
            // Prompt for student email
            Console.Write("Enter student's email: ");
            string email = Console.ReadLine();

            // Search for the student in all faculties
            foreach (var faculty in faculties)
            {
                foreach (var student in faculty.Students)
                {
                    if (student.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Student belongs to faculty: {faculty.Name}");
                        return;
                    }
                }
            }

            Console.WriteLine("Student not found in any faculty.");
        }
    }
}