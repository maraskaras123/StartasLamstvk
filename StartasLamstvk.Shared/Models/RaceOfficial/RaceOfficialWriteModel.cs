using System.ComponentModel.DataAnnotations;

namespace StartasLamstvk.Shared.Models.RaceOfficial
{
    public class RaceOfficialWriteModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "User is required")]
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }
        [RegularExpression("\\d\\d:\\d\\d", ErrorMessage = "Arrival time must have a \"00:00\" format")]
        public string ArrivalTime { get; set; }
    }
}