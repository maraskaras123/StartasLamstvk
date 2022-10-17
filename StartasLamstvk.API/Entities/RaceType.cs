using StartasLamstvk.Shared.Models;
using StartasLamstvk.Shared.Models.Enum;

namespace StartasLamstvk.API.Entities
{
    public class RaceType
    {
        public EnumRaceType Id { get; set; }

        public virtual List<RaceTypeTranslation> RaceTypeTranslations { get; set; }
    }
}
