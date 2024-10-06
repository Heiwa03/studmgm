using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace UniversityManagement
{
    public class BatchEnrollment
    {
        private readonly Logger logger;

        public BatchEnrollment(Logger logger)
        {
            this.logger = logger;
        }

        public List<Faculty> ImportFacultiesFromCsv(string filePath)
        {
            var faculties = new List<Faculty>();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    string? line;
                    bool isFirstLine = true;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (isFirstLine)
                        {
                            isFirstLine = false;
                            continue; // Skip the header line
                        }

                        var fields = line.Split(',');

                        if (fields.Length != 3)
                        {
                            logger.LogWarning($"Invalid record: {line}");
                            continue;
                        }

                        try
                        {
                            var faculty = new Faculty(
                                fields[0].Trim(), // Name
                                fields[1].Trim(), // Abbreviation
                                Enum.Parse<StudyField>(fields[2].Trim()) // StudyField
                            );

                            faculties.Add(faculty);
                            logger.LogInfo($"Faculty {faculty.Name} imported.");
                        }
                        catch (Exception ex)
                        {
                            logger.LogError($"Error parsing record: {line}. Exception: {ex.Message}. Faculty could not be imported.");
                        }
                    }
                }

                logger.LogInfo($"Successfully imported faculties from CSV file.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Error reading CSV file: {ex.Message}");
            }

            return faculties;
        }

        public List<Student> ImportStudentsFromCsv(string filePath, List<Faculty> faculties)
        {
            var students = new List<Student>();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    string? line;
                    bool isFirstLine = true;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (isFirstLine)
                        {
                            isFirstLine = false;
                            continue; // Skip the header line
                        }

                        var fields = line.Split(',');

                        if (fields.Length != 6)
                        {
                            logger.LogWarning($"Invalid record: {line}");
                            continue;
                        }

                        try
                        {
                            var student = new Student(
                                fields[0].Trim(), // FirstName
                                fields[1].Trim(), // LastName
                                fields[2].Trim(), // Email
                                DateTime.ParseExact(fields[3].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture), // EnrollmentDate
                                DateTime.ParseExact(fields[4].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture) // DateOfBirth
                            );

                            var facultyAbbreviation = fields[5].Trim();
                            var faculty = faculties.Find(f => f.Abbreviation == facultyAbbreviation);

                            if (faculty != null)
                            {
                                faculty.AddStudent(student);
                                students.Add(student);
                                logger.LogInfo($"Student {student.FirstName} {student.LastName} assigned to faculty {faculty.Name}");
                            }
                            else
                            {
                                logger.LogWarning($"No faculty found for abbreviation: {facultyAbbreviation}. Student {student.FirstName} {student.LastName} could not be imported.");
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.LogError($"Error parsing record: {line}. Exception: {ex.Message}. Student could not be imported.");
                        }
                    }
                }

                logger.LogInfo($"Successfully imported students from CSV file.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Error reading CSV file: {ex.Message}");
            }

            return students;
        }

        public void MergeFaculties(List<Faculty> mainFaculties, List<Faculty> newFaculties)
        {
            foreach (var newFaculty in newFaculties)
            {
                var mainFaculty = mainFaculties.Find(f => f.Abbreviation == newFaculty.Abbreviation);
                if (mainFaculty != null)
                {
                    mainFaculty.Students.AddRange(newFaculty.Students);
                    logger.LogInfo($"Merged {newFaculty.Students.Count} students into faculty {mainFaculty.Name}");
                }
                else
                {
                    mainFaculties.Add(newFaculty);
                    logger.LogInfo($"Added new faculty {newFaculty.Name} with {newFaculty.Students.Count} students");
                }
            }
        }

        public void GraduateStudentByEmail(string email, List<Faculty> faculties)
        {
            foreach (var faculty in faculties)
            {
                var student = faculty.Students.Find(s => s.Email == email);
                if (student != null)
                {
                    faculty.GraduateStudent(student);
                    logger.LogInfo($"Student {student.FirstName} {student.LastName} graduated from faculty {faculty.Name}");
                    return;
                }
            }

            logger.LogWarning($"No student found with email: {email}");
        }

        public void GraduateStudentsFromFile(string filePath, List<Faculty> faculties)
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    string? email;
                    while ((email = reader.ReadLine()) != null)
                    {
                        email = email.Trim();
                        if (!string.IsNullOrEmpty(email))
                        {
                            logger.LogInfo($"Processing graduation for student with email: {email}");
                            GraduateStudentByEmail(email, faculties);
                        }
                        else
                        {
                            logger.LogWarning("Encountered empty or invalid email in file.");
                        }
                    }
                }

                logger.LogInfo($"Successfully processed graduation for students from file: {filePath}");
            }
            catch (Exception ex)
            {
                logger.LogError($"Error reading email file: {ex.Message}");
            }
        }
    }
}