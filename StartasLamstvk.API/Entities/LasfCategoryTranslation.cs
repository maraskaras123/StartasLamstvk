using StartasLamstvk.Shared.Models;
using StartasLamstvk.Shared.Models.Enum;

namespace StartasLamstvk.API.Entities
{
    public class LasfCategoryTranslation
    {
        public int Id { get; set; }
        public EnumLasfCategory LasfCategoryId { get; set; }
        public string Text { get; set; }
        public string LanguageCode { get; set; }

        public virtual LasfCategory LasfCategory { get; set; }
    }
}