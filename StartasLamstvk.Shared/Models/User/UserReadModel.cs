using StartasLamstvk.Shared.Models.Category;
using StartasLamstvk.Shared.Models.RacePreference;

namespace StartasLamstvk.Shared.Models.User
{
    public class UserReadModel : UserBaseModel
    {
        public DateTime BirthDate { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public LasfCategoryReadModel LasfCategory { get; set; }
        public MotoCategoryReadModel MotoCategory { get; set; }
        public List<PreferenceReadModel> Preferences { get; set; }
        public List<RacePreferenceReadModel> RacePreferences { get; set; }
    }
}