namespace UniversityManagement
{
    public class Student
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime EnrollmentDate { get; private set; }
        public DateTime DateOfBirth { get; private set; }

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