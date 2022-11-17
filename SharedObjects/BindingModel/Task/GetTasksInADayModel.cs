using Microsoft.AspNetCore.Mvc;

namespace SharedObjects.BindingModel
{
    public class GetTasksInADayModel
    {
        public GetTasksInADayModel()
        {
            DayOfMonth = 1;
            Year = 1;
            Month = 1;
            Offset = 1;
        }

        [BindProperty(Name = "date")]
        public int DayOfMonth { get; set; }

        [BindProperty(Name = "month")]
        public int Month { get; set; }

        [BindProperty(Name = "year")]
        public int Year { get; set; }

        [BindProperty(Name = "offset")]
        public int Offset { get; set; }
    }
}