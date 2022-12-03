using System.ComponentModel.DataAnnotations;

namespace StartasLamstvk.Shared.Models.Crew
{
    public class CrewWriteModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Location is required")]
        public string Location { get; set; }
        public int? Mileage { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Driver is required")]
        public int DriverId { get; set; }
        public int? Passenger1Id { get; set; }
        public int? Passenger2Id { get; set; }
        public int? Passenger3Id { get; set; }
        public int? Passenger4Id { get; set; }
    }
}