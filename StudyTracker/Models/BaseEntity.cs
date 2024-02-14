using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StudyTracker.Models
{
    public class BaseEntity
    {
        [ReadOnly(true)]
        [Display(Name = "Date Added")]
        [ScaffoldColumn(false)]
        [HiddenInput(DisplayValue = false)]
        public DateTime DateAdded { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Date Modified")]
        [Editable(false)]
        [ScaffoldColumn(false)]
        [HiddenInput(DisplayValue = false)]
        public DateTime? DateModified { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Date Deleted")]
        [ScaffoldColumn(false)]
        [HiddenInput(DisplayValue = false)]
        public DateTime? DateDeleted { get; set; }
    }
}
