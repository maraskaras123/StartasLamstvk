using StartasLamstvk.Shared.Models.RaceOfficials;
using StartasLamstvk.Shared.Models.RaceType;
using StartasLamstvk.Shared.Models.User;

namespace StartasLamstvk.Shared.Models.Event
{
    public class EventReadModel
    {
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public UserBaseModel Author { get; set; }
        public UserBaseModel Manager { get; set; }
        public string Location { get; set; }
        public string Championship { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public RaceTypeReadModel RaceType { get; set; }
        public List<RaceOfficialReadModel> RaceOfficials { get; set; }
    }
}
