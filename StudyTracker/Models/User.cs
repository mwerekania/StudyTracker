namespace StudyTracker.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsVerified { get; set; }

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }

        public ICollection<UserSession> UserSessions { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<Subject> Subjects { get; set; }

        public ICollection<Assignment> Assignments { get; set; }

        public User()
        {
            UserName = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            DateAdded = DateTime.Now;
            RegistrationDate = DateTime.Now;
            IsVerified = false;
            FullName();

            UserSessions = new List<UserSession>();
            Courses = new List<Course>();
            Subjects = new List<Subject>();
            Assignments = new List<Assignment>();
        }
    }

    public class UserSession : BaseEntity
    {
        public int UserSessionId { get; set; }
        public int UserId { get; set; }
        public string? SessionToken { get; set; }
        public DateTime ExpirationTime { get; set; }
    }

}
