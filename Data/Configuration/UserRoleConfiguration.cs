using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityProject.Data.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            var userRole=new IdentityUserRole<string>()
            {
                RoleId="fab4fac1-c546-41de-aebc-a14da6895711",
                UserId="b74ddd14-6340-4840-95c2-db12554843e5"
            };

            builder.HasData(userRole);
        }
    }
}
