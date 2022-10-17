using StartasLamstvk.Shared.Models;
using StartasLamstvk.Shared.Models.Enum;

namespace StartasLamstvk.API.Entities
{
    public class LasfCategory
    {
        public EnumLasfCategory Id { get; set; }

        public virtual List<LasfCategoryTranslation> LasfCategoryTranslations { get; set; }
    }
}