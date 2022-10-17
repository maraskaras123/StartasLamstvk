using StartasLamstvk.Shared.Models;
using StartasLamstvk.Shared.Models.Enum;

namespace StartasLamstvk.API.Entities
{
    public class Role
    {
        public EnumRole Id { get; set; }

        public virtual List<RoleTranslation> RoleTranslations { get; set; }
    }
}
