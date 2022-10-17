using StartasLamstvk.Shared.Models.Enum;

namespace StartasLamstvk.API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public EnumLasfCategory? LasfCategoryId { get; set; }
        public EnumMotoCategory? MotoCategoryId { get; set; }
        public EnumRole RoleId { get; set; }

        public virtual LasfCategory LasfCategory { get; set; }
        public virtual MotoCategory MotoCategory { get; set; }
        public virtual Role Role { get; set; }
        public virtual List<UserPreference> UserPreferences { get; set; }
        public virtual List<UserRacePreference> UserRacePreferences { get; set; }
        public virtual List<RaceOfficial> RaceOfficials { get; set; }
        public virtual List<Event> ManagedEvents { get; set; }
    }
}
