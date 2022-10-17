using StartasLamstvk.Shared.Models.User;

namespace StartasLamstvk.Shared.Models.RaceOfficial
{
    public class RaceOfficialReadModel
    {
        public int Id { get; set; }
        public UserBaseModel User { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
    }
}