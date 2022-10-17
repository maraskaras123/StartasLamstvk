using StartasLamstvk.Shared.Models.Category;

namespace StartasLamstvk.Shared.Models.User
{
    public class UserReadModel : UserBaseModel
    {
        public DateTime BirthDate { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public LasfCategoryReadModel LasfCategory { get; set; }
        public MotoCategoryReadModel MotoCategory { get; set; }
    }
}