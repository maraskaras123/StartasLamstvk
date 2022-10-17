using StartasLamstvk.Shared.Models;
using StartasLamstvk.Shared.Models.Enum;

namespace StartasLamstvk.API.Entities
{
    public class RaceTypeTranslation
    {
        public int Id { get; set; }
        public EnumRaceType RaceTypeId { get; set; }
        public string Text { get; set; }
        public string LanguageCode { get; set; }

        public virtual RaceType RaceType { get; set; }
    }
}
