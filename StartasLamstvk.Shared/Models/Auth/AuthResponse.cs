using StartasLamstvk.Shared.Models.User;

namespace StartasLamstvk.Shared.Models.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public DateTime ExpiredAt { get; set; }
        public UserBaseModel User { get; set; }
    }
}