using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TAApplication.Areas.Data;

namespace TAApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<TAUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public async Task InitializeUsers(UserManager<TAUser> um, RoleManager<IdentityRole> rm, IUserStore<TAUser> us)
        {
            // Add roles if they done exist
            if (!(await rm.RoleExistsAsync("Administrator")))
                await rm.CreateAsync(new IdentityRole("Administrator"));
            if (!(await rm.RoleExistsAsync("Applicant")))
                await rm.CreateAsync(new IdentityRole("Applicant"));
            if (!(await rm.RoleExistsAsync("Professor")))
                await rm.CreateAsync(new IdentityRole("Professor"));

            // Look for any users
            if (um.Users.Any<TAUser>())
            {
                return;   // DB has been seeded
            }
            string password = "123ABC!@#def";
            var users = new TAUser[]
            {
                new TAUser{Unid = "u1234567", Name = "Sam Admin", RefferedTo = "Admin", Email = "admin@utah.edu"},
                new TAUser{Unid = "u7654321", Name = "Jim Prof", RefferedTo = "Professor", Email = "professor@utah.edu"},
                new TAUser{Unid = "u0000000", Name = "Nasser Applicant", RefferedTo = "Applicant", Email = "u0000000@utah.edu"},
                new TAUser{Unid = "u0000001", Name = "Jake Applicant", RefferedTo = "Applicant", Email = "u0000001@utah.edu"},
                new TAUser{Unid = "u0000002", Name = "Sam Applicant", RefferedTo = "Applicant", Email = "u0000002@utah.edu"}
            };

            // Add users
            foreach (TAUser user in users)
            {
                await us.SetUserNameAsync(user, user.Unid, CancellationToken.None);
                user.EmailConfirmed = true;
                await um.CreateAsync(user, password);
            }

            // Assign users to roles
            foreach (TAUser user in users)
            {
                if (user.Unid == "u1234567")
                    await um.AddToRoleAsync(user, "Administrator");
                else if (user.Unid == "u7654321")
                    await um.AddToRoleAsync(user, "Professor");
                else
                    await um.AddToRoleAsync(user, "Applicant");
            }
        }
    }
}