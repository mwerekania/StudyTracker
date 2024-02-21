using System.ComponentModel.DataAnnotations;

namespace StudyTracker.Models
{
    public class User : BaseEntity
    {
        [Display(Name = "User ID")]
        public int UserId { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string PasswordHash { get; set; }

        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Is Verified")]
        public bool IsVerified { get; set; }

        [Display(Name = "Full Name")]
        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }

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
