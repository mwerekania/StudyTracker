using System.ComponentModel.DataAnnotations;

namespace StudyTracker.Models
{
    public enum Priority
    {
        None,
        Low,
        Medium,
        High
    }

    public enum Status
    {
        [Display(Name = "Not Started")]
        NotStarted,
        [Display(Name = "In Progress")]
        InProgress,
        [Display(Name = "Completed")]
        Completed
    }
}
