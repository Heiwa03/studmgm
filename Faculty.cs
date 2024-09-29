using System;
using System.Collections.Generic;

namespace UniversityManagement
{
    public enum StudyField
    {
        MECHANICAL_ENGINEERING,
        SOFTWARE_ENGINEERING,
        FOOD_TECHNOLOGY,
        URBANISM_ARCHITECTURE,
        VETERINARY_MEDICINE
    }

    public class Faculty
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public List<Student> Students { get; set; }
        public StudyField StudyField { get; set; }

        public Faculty(string name, string abbreviation, StudyField studyField)
        {
            Name = name;
            Abbreviation = abbreviation;
            StudyField = studyField;
            Students = new List<Student>();
            GraduatedStudents = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

            public void GraduateStudent(Student student)
        {
            if (Students.Remove(student))
            {
                GraduatedStudents.Add(student);
            }
        }

        public void DisplayEnrolledStudents()
        {
            Console.WriteLine("Enrolled Students:");
            foreach (var student in Students)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}");
            }
        }

        public void DisplayGraduatedStudents()
        {
            Console.WriteLine("Graduated Students:");
            foreach (var student in GraduatedStudents)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}");
            }
        }
    }
}