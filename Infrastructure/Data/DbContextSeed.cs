using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminRole = new IdentityRole("Admin");

            var mentor = new IdentityRole("Mentor");

            var student = new IdentityRole("Student");


            if (roleManager.Roles.All(r => r.Name != adminRole.Name))
            {
                await roleManager.CreateAsync(adminRole);
                await roleManager.AddClaimAsync(adminRole, new Claim(ClaimTypes.Role, adminRole.Name));
            }
            if (roleManager.Roles.All(r => r.Name != mentor.Name))
            {
                await roleManager.CreateAsync(mentor);
                await roleManager.AddClaimAsync(mentor, new Claim(ClaimTypes.Role, mentor.Name));
            }
            if (roleManager.Roles.All(r => r.Name != student.Name))
            {
                await roleManager.CreateAsync(student);
                await roleManager.AddClaimAsync(mentor, new Claim(ClaimTypes.Role, student.Name));
            }

            var administrator = new User() { UserName = "admin", Email = "administrator@academicblog.com" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                var result = await userManager.CreateAsync(administrator, "~d[3f6mz)yxx'D=y");
                await userManager.AddToRolesAsync(administrator, new[] { adminRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(AcademicBlogContext context)
        {
            // Seed, if necessary

            //await context.SaveChangesAsync();

        }
    }
}
