using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMgmt.Domain
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityRole>().HasData(
                   new IdentityRole
                   {
                       Id = "b0354acd-9219-4621-ba32-282db259c081",
                       Name = "Admin",
                       NormalizedName = "Admin"
                   },
                   new IdentityRole
                   {
                       Id = "ccce14b5-123c-4166-981c-21e23aaef4c2",
                       Name = "Employee",
                       NormalizedName = "Employee"
                   }
               );
            modelBuilder.Entity<IdentityUser>().HasData(
                 new IdentityUser
                 {
                     Id= "330ff8fa-a471-4bf7-8011-21bab7a7a23b",
                     Email = "admin@admincontrol.com",
                     UserName = "admin@admincontrol.com",
                     NormalizedUserName = "ADMIN@ADMINCONTROL.COM",
                     PasswordHash = hasher.HashPassword(null, "P@ssw0rd")
                 }
             );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "b0354acd-9219-4621-ba32-282db259c081",
                UserId = "330ff8fa-a471-4bf7-8011-21bab7a7a23b"
            }
        );


        }
    }
}
