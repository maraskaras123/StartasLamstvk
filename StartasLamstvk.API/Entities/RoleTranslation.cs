namespace StartasLamstvk.API.Entities
{
    public class RoleTranslation
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Text { get; set; }
        public string LanguageCode { get; set; }

        public virtual Role Role { get; set; }
    }
}
