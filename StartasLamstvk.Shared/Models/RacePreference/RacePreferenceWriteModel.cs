using System.ComponentModel.DataAnnotations;

namespace StartasLamstvk.Shared.Models.RacePreference
{
    public class RacePreferenceWriteModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Event is required")]
        public int EventId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }
    }
}