using StartasLamstvk.Shared.Models.Enum;

namespace StartasLamstvk.API.Entities
{
    public class MotoCategoryTranslation
    {
        public int Id { get; set; }
        public EnumMotoCategory MotoCategoryId { get; set; }
        public string Text { get; set; }
        public string LanguageCode { get; set; }

        public virtual MotoCategory MotoCategory { get; set; }
    }
}
