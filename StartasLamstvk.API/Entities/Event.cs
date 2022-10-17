using StartasLamstvk.Shared.Models;
using StartasLamstvk.Shared.Models.Enum;

namespace StartasLamstvk.API.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int AuthorId { get; set; }
        public int ManagerId { get; set; }
        public string Location { get; set; }
        public string Championship { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public EnumRaceType RaceTypeId { get; set; }

        public virtual User Author { get; set; }
        public virtual User Manager { get; set; }
        public virtual RaceType RaceType { get; set; }
        public virtual List<RaceOfficial> RaceOfficials { get; set; }
        public virtual List<UserRacePreference> UserRacePreferences { get; set; }
    }
}
