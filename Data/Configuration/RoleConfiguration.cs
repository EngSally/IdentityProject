using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProject.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            IdentityRole adminRoleIdentity= new IdentityRole()
            {
                Id="fab4fac1-c546-41de-aebc-a14da6895711",
                Name=Constant.AdminRole,
                ConcurrencyStamp="1",
                NormalizedName=Constant.AdminRole.ToUpper()
            };
            IdentityRole UserRoleIdentity= new IdentityRole()
            {
                Id="c7b013f0-5201-4317-abd8-c211f91b7330",
                Name=Constant.UserRole,
                ConcurrencyStamp="2",
                NormalizedName=Constant.UserRole.ToUpper()
            };

            builder.HasData(new[] { adminRoleIdentity, UserRoleIdentity });
        }
    }
}
