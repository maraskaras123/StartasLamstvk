using StartasLamstvk.Shared.Models.Event;
using StartasLamstvk.Shared.Models.RaceOfficial;

namespace StartasLamstvk.Shared.Models.Crew
{
    public class CrewReadModel
    {
        public int Id { get; set; }
        public EventReadModel Event { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int? Mileage { get; set; }
        public RaceOfficialReadModel Driver { get; set; }
        public RaceOfficialReadModel Passenger1 { get; set; }
        public RaceOfficialReadModel Passenger2 { get; set; }
        public RaceOfficialReadModel Passenger3 { get; set; }
        public RaceOfficialReadModel Passenger4 { get; set; }
    }
}
