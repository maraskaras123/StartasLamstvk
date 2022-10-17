using StartasLamstvk.Shared.Models;
using StartasLamstvk.Shared.Models.Enum;

namespace StartasLamstvk.API.Entities
{
    public class MotoCategory
    {
        public EnumMotoCategory Id { get; set; }

        public virtual List<MotoCategoryTranslation> MotoCategoryTranslations { get; set; }
    }
}
