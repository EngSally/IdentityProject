using IdentityProject.Data.Configuration;
using IdentityProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection.Emit;

namespace IdentityProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<CustomIdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CustomIdentityUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserClaim<string >>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserRole<string >>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserLogin<string >>().ToTable("UserLogin", "security");
            builder.Entity<IdentityRoleClaim<string >>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string >>().ToTable("UserTokens", "security");


            //seed Admin Data
            builder.ApplyConfiguration(new RoleConfiguration());
           builder.ApplyConfiguration(configuration: new IdentityUserConfiguration());
           builder.ApplyConfiguration(new UserRoleConfiguration());


           
        }
    }
}