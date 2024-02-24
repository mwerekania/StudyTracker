using System.ComponentModel.DataAnnotations;

namespace StudyTracker.Models
{
    public class Subject : BaseEntity
    {
        [Required]
        [Display(Name = "Subject ID")]
        public int SubjectId { get; set; }
        
        [Required]
        [Display(Name = "Course ID")]
        public int CourseId { get; set; }
        [Required]

        [Display(Name = "Course")]
        public Course Course { get; set; }

        [Required]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        [Display(Name = "App User ID")]
        [Required]
        public string AppUserID { get; set; }
    }
}
