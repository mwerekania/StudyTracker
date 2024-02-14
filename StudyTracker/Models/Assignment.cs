namespace StudyTracker.Models
{
    public class Assignment : BaseEntity
    {
        public int AssignmentId { get; set; }

        public int SubjectId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        public int UserId { get; set; }

        public DateTime? DueDate { get; set; }

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