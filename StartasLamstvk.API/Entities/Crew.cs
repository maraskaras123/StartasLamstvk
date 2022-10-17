namespace StartasLamstvk.API.Entities
{
    public class Crew
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int? Mileage { get; set; }
        public int DriverId { get; set; }
        public int? Passenger1Id { get; set; }
        public int? Passenger2Id { get; set; }
        public int? Passenger3Id { get; set; }
        public int? Passenger4Id { get; set; }

        public virtual Event Event { get; set; }
        public virtual RaceOfficial Driver { get; set; }
        public virtual RaceOfficial Passenger1 { get; set; }
        public virtual RaceOfficial Passenger2 { get; set; }
        public virtual RaceOfficial Passenger3 { get; set; }
        public virtual RaceOfficial Passenger4 { get; set; }
    }
}
