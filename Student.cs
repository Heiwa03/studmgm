namespace UniversityManagement
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Student(string firstName, string lastName, string email, DateTime enrollmentDate, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            EnrollmentDate = enrollmentDate;
            DateOfBirth = dateOfBirth;
        }
    }
}