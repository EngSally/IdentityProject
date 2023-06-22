using IdentityProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProject.Data.Configuration
{
    public class IdentityUserConfiguration : IEntityTypeConfiguration<CustomIdentityUser>
    {
        public void Configure(EntityTypeBuilder<CustomIdentityUser> builder)
        {
            var adminuser=new CustomIdentityUser
            {

                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "eng.sally.atalla",
                Email = "eng.sally.atalla@gmail.com",
                NormalizedUserName="eng.sally.atalla".ToUpper(),
                LockoutEnabled = false,
                PhoneNumber = "01015596191",
                FirstName="Sally",
                LastName="Attalla",
               PasswordHash="AQAAAAEAACcQAAAAEHOG9FzgTqIb3fdK10Dvj2fmXkbDwqclJjt4sd0cw8OTthkQgl635XhQgPNm2UiNWQ=="

            };

          
          
            builder.HasData(adminuser);
        }
    }
}
