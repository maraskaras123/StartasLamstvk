using StartasLamstvk.Shared.Models.User;
using StartasLamstvk.Shared.Models.Wage;

namespace StartasLamstvk.Shared.Models.RaceOfficial
{
    public class RaceOfficialReadModel
    {
        public int Id { get; set; }
        public UserBaseModel User { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string ArrivalTime { get; set; }
        public List<WageReadModel> Wages { get; set; }
    }
}