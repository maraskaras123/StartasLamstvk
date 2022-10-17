namespace StartasLamstvk.API.Entities
{
    public class UserRacePreference
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
        public virtual Event Event { get; set; }
    }
}
