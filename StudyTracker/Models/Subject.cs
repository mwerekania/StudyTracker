namespace StudyTracker.Models
{
    public class Subject : BaseEntity
    {
        public int SubjectId { get; set; }

        public int CourseId { get; set; }
        public string SubjectName { get; set; }
     
    }
}
