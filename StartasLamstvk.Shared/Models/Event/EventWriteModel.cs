using StartasLamstvk.Shared.Models.Enum;

namespace StartasLamstvk.Shared.Models.Event
{
    public class EventWriteModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int ManagerId { get; set; }
        public string Location { get; set; }
        public string Championship { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public EnumRaceType RaceTypeId { get; set; }
    }
}
