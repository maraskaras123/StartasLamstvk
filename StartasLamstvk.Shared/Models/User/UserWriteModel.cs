using StartasLamstvk.Shared.Models.Enum;

namespace StartasLamstvk.Shared.Models.User
{
    public class UserWriteModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public EnumRole RoleId { get; set; }
        public EnumLasfCategory? LasfCategoryId { get; set; }
        public EnumMotoCategory? MotoCategoryId { get; set; }
    }
}