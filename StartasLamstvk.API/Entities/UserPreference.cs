using StartasLamstvk.Shared.Models;
using StartasLamstvk.Shared.Models.Enum;

namespace StartasLamstvk.API.Entities
{
    public class UserPreference
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public EnumRaceType RaceTypeId { get; set; }
        public short Year { get; set; }

        public virtual User User { get; set; }
        public virtual RaceType RaceType { get; set; }
    }
}
