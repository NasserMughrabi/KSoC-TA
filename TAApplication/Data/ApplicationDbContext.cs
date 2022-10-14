using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TAApplication.Areas.Data;
using TAApplication.Models;

namespace TAApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<TAUser>
    {
        public IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor http)
            : base(options)
        {
            _httpContextAccessor = http;
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

        public async Task InitializeApplications(UserManager<TAUser> um)
        {
            // Look for any applications.
            if (this.Application.Any())
            {
                return;   // DB has been seeded
            }

            var applications = new Application[]
            {
            new Application{Pursuing=Pursuing.BS,Department="CS",GPA=3, Hours=15, WeekBefore=true, CompletedSemesters=2,
            Statement="Hello I am John", TransferSchool="SLCC", Linkedin="http", ResumeFile="resume", UserID="u0000000"},
            new Application{Pursuing=Pursuing.MS,Department="MATH",GPA=4, Hours=10, WeekBefore=false, CompletedSemesters=3,
            Statement="", TransferSchool="", Linkedin="", ResumeFile="", UserID="u0000001"},
            };
            foreach (Application a in applications)
            {
                this.Application.Add(a);
            }

            // Assign users to roles
            foreach (TAUser user in um.Users)
            {
                if (user.Unid == "u0000000")
                    applications[0].User = user;
                else if (user.Unid == "u0000001")
                    applications[1].User = user;
            }

            this.SaveChanges();
        }

        /// <summary>
        /// Every time Save Changes is called, add timestamps
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()  // JIM: Override save changes to add timestamps
        {
            AddTimestamps();
            return base.SaveChanges();
        }
        /// <summary>
        /// Every time Save Changes (Async) is called, add timestamps
        /// </summary>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            AddTimestamps();   // JIM: Override save changes async to add timestamps
            return await base.SaveChangesAsync(cancellationToken);
        }
        /// <summary>
        /// JIM: this code adds time/user to DB entry
        /// 
        /// Check the DB tracker to see what has been modified, and add timestamps/names as appropriate.
        /// 
        /// </summary>
        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is ModificationTracking
                        && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = "";

            if (_httpContextAccessor.HttpContext == null) // happens during startup/initialization code
            {
                currentUsername = "DBSeeder";
            }
            else
            {
                currentUsername = _httpContextAccessor.HttpContext.User.Identity?.Name;
            }

            currentUsername ??= "Sadness"; // JIM: compound assignment magic... test for null, and if so, assign value

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((ModificationTracking)entity.Entity).CreationDate = DateTime.UtcNow;
                    ((ModificationTracking)entity.Entity).CreatedBy = currentUsername;
                }
                ((ModificationTracking)entity.Entity).ModificationDate = DateTime.UtcNow;
                ((ModificationTracking)entity.Entity).ModifiedBy = currentUsername;
            }
        }
        /// <summary>
        /// JIM: this code adds time/user to DB entry
        /// 
        /// Check the DB tracker to see what has been modified, and add timestamps/names as appropriate.
        /// 
        /// </summary>
        public DbSet<Application> Application { get; set; }
    }
}