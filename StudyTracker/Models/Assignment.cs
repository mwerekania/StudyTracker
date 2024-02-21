using System.ComponentModel.DataAnnotations;

namespace StudyTracker.Models
{
    public class Assignment : BaseEntity
    {
        [Display(Name = "ID")]
        public int AssignmentId { get; set; }

        [Display(Name = "Course ID")]
        public int CourseId { get; set; }

        [Display(Name = "Course")]
        public Course Course { get; set; }

        [Display(Name = "Subject ID")]
        public int SubjectId { get; set; }

        [Display(Name = "Subject")]
        public Subject Subject { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Priority")]
        public Priority Priority { get; set; }

        [Display(Name = "Status")]
        public Status Status { get; set; }

        [Display(Name = "User ID")]
        public int UserId { get; set; }

        [Display(Name = "User")]
        public User User { get; set; }

        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Completion Date")]
        public DateTime? CompletionDate { get; set; }

        public Assignment()
        {
            SubjectId = 0;
            Title = string.Empty;
            Description = string.Empty;
            UserId = 0;
            Status = Status.NotStarted;
            Priority = Priority.Low;
            DateAdded = DateTime.Now;
        }
    }
}