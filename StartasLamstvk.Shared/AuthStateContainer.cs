using System.Globalization;
using StartasLamstvk.Shared.Helpers;
using StartasLamstvk.Shared.Models.Auth;
using StartasLamstvk.Shared.Models.User;

namespace StartasLamstvk.Shared
{
    public class AuthStateContainer
    {
        private readonly ILocalStorage _localStorage;

        public string Token { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public int? UserId { get; set; }
        public UserBaseModel User { get; set; }
        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public AuthStateContainer(ILocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task LoadToken()
        {
            var expiredDate = await _localStorage.GetStringAsync("tokenExpiredAt");
            if (!string.IsNullOrEmpty(expiredDate)
                && DateTime.TryParse(expiredDate, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var expiredAt)
                && expiredAt > DateTime.UtcNow)
            {
                var token = await _localStorage.GetStringAsync("token");
                var userId = await _localStorage.GetStringAsync("userId");
                ExpiredAt = expiredAt;
                Token = token;
                UserId = int.Parse(userId);
            }
            NotifyStateChanged();
        }

        public async Task SaveToken(AuthResponse token)
        {
            User = token.User;
            ExpiredAt = token.ExpiredAt;
            Token = token.Token;
            await _localStorage.SaveStringAsync("token", token.Token);
            await _localStorage.SaveStringAsync("tokenExpiredAt",
                token.ExpiredAt.ToString(CultureInfo.InvariantCulture));
            await _localStorage.SaveStringAsync("userId", token.User.Id.ToString());
            NotifyStateChanged();
        }

        public async Task Clear()
        {
            await _localStorage.RemoveAsync("token");
            await _localStorage.RemoveAsync("tokenExpiredAt");
            await _localStorage.RemoveAsync("userId");
            NotifyStateChanged();
        }
    }
}
