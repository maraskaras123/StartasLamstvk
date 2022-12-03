using StartasLamstvk.Shared.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace StartasLamstvk.Shared.Models.User
{
    public class UserWriteModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        public DateTime BirthDate { get; set; }
        public string Location { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Role is required")]
        public EnumRole RoleId { get; set; }

        public EnumLasfCategory? LasfCategoryId { get; set; }
        public EnumMotoCategory? MotoCategoryId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}