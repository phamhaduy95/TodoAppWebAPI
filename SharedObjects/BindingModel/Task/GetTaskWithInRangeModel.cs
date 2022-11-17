using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SharedObjects.BindingModel
{
    public class GetTaskWithInRangeModel
    {
        [Required]
        [BindProperty(Name = "startTime")] // binding property will only work query string
        public DateTimeOffset StartTime { get; set; }

        [Required]
        [BindProperty(Name = "endTime")]
        public DateTimeOffset EndTime { get; set; }
    }
}