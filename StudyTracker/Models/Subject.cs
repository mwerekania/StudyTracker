using System.ComponentModel.DataAnnotations;

namespace StudyTracker.Models
{
    public class Subject : BaseEntity
    {
        public int SubjectId { get; set; }
        
        [Required]
        
        public int CourseId { get; set; }
        [Required]
        
        public string SubjectName { get; set; }

    }
}
