using Microsoft.AspNetCore.Mvc;

namespace SharedObjects.BindingModel
{
    public class GetTaskInMonthModel
    {
        public GetTaskInMonthModel()
        {
            Month = 1;
            Year = 1;
            Offset = 0;
        }

        [BindProperty(Name = "month")]
        public int Month { get; set; }

        [BindProperty(Name = "year")]
        public int Year { get; set; }

        [BindProperty(Name = "offset")]
        public int Offset { get; set; }
    }
}