namespace StartasLamstvk.API.Entities
{
    public class RaceOfficial
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? ArrivalTime { get; set; }

        public virtual User User { get; set; }
        public virtual Event Event { get; set; }
        public virtual List<Wage> Wages { get; set; }
    }
}
