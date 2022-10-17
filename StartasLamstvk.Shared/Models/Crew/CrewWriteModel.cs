namespace StartasLamstvk.Shared.Models.Crew
{
    public class CrewWriteModel
    {
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int? Mileage { get; set; }
        public int DriverId { get; set; }
        public int? Passenger1Id { get; set; }
        public int? Passenger2Id { get; set; }
        public int? Passenger3Id { get; set; }
        public int? Passenger4Id { get; set; }
    }
}
