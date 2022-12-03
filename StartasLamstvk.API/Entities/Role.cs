using Microsoft.AspNetCore.Identity;

namespace StartasLamstvk.API.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role(string roleName) : base(roleName)
        {
        }

        public Role()
        {
        }

        public virtual List<RoleTranslation> RoleTranslations { get; set; }
    }
}
