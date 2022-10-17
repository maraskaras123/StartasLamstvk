using StartasLamstvk.Shared.Models.UserRole;

namespace StartasLamstvk.Shared.Models.User
{
    public class UserBaseModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public UserRoleReadModel Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}
