using System.ComponentModel.DataAnnotations;

namespace StudyTracker.Models
{
    public class Course : BaseEntity
    {
        [Display(Name = "Course ID")]
        public int CourseId { get; set; }

        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        /*
         * [Display(Name = "User ID")]
       // public int UserId { get; set; }

        [Display(Name = "User")]
        public User User { get; set; }
        */

        [Display(Name = "App User ID")]
        [Required]
        public string AppUserID { get; set; }
    }
}
