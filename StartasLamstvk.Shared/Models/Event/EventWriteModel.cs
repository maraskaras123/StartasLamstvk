using System.ComponentModel.DataAnnotations;
using StartasLamstvk.Shared.Models.Enum;

namespace StartasLamstvk.Shared.Models.Event
{
    public class EventWriteModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "DateFrom/Date is required")]
        [FutureDate]
        public DateTime DateFrom { get; set; }
        [FutureDate]
        public DateTime? DateTo { get; set; }
        public int ManagerId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Location is required")]
        public string Location { get; set; }
        public string Championship { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Race type is required")]
        public EnumRaceType RaceTypeId { get; set; }
    }
}
