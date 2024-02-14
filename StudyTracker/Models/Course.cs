namespace StudyTracker.Models
{
    public class Course : BaseEntity
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int UserId { get; set; }

        public ICollection<Subject> Subjects { get; set; }
    }
}
