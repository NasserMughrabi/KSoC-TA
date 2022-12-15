/**
  Author:    Nasser Mughrabi
  Partner:   None   
  Date:      14-Decemeber-2022
  Course:    CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Nasser Mughrabi - This work may not be copied for use in Academic Coursework.
  I, Nasser Mughrabi, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source. Any references used in the completion of the assignment are cited in my README file.
  
File Contents:
    This class is to build and structure the database
 */

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

        /// <summary>
        /// initialize/seed the users table
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// initialize/seed the applications table
        /// </summary>
        /// <returns></returns>
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
        /// initialize/seed the applications table
        /// </summary>
        /// <returns></returns>
        public async Task InitializeCourses(UserManager<TAUser> um)
        {
            // Look for any applications.
            if (this.Course.Any())
            {
                return;   // DB has been seeded
            }

            var courses = new Course[]
            {
            new Course{Semester="Spring", Year=2023, Title="Introduction to CS", Department="CS", Number=1400,
                Section=001, Description="Intro", ProfessorUnid="u1234566", ProfessorName="Jim St",
                TimeAndDaysOffered="M/W 2:30-3:50", Location="Web 104", Credit=4, Enrollment=95, Note="Need extra TAs"},
            new Course{Semester="Spring", Year=2023, Title="Software Practice 1", Department="CS", Number=3500,
                Section=001, Description="Software", ProfessorUnid="u1234567", ProfessorName="David johnson",
                TimeAndDaysOffered="M/W 4:35-5:55", Location="Web 105", Credit=4, Enrollment=150, Note=""},
            new Course{Semester="Spring", Year=2023, Title="Software Practice 2", Department="CS", Number=3505,
                Section=001, Description="Software", ProfessorUnid="u1234568", ProfessorName="Erin Parker",
                TimeAndDaysOffered="T/W 10:30-11:50", Location="Web 104", Credit=4, Enrollment=106, Note=""},
            new Course{Semester="Spring", Year=2023, Title="Computer Systems", Department="CS", Number=4400,
                Section=003, Description="Low level software practices", ProfessorUnid="u1234569", ProfessorName="Danny Kopta",
                TimeAndDaysOffered="T/F 11:30-12:50", Location="Web 101", Credit=4, Enrollment=105, Note="1 more TA needed"},
            };

            foreach (Course c in courses)
            {
                this.Course.Add(c);
            }
            this.SaveChanges();
        }


        /// <summary>
        /// initialize/seed the applications table
        /// </summary>
        /// <returns></returns>
        public async Task InitializeSlots(UserManager<TAUser> um)
        {
            // Look for any applications.
            if (this.Slot.Any())
            {
                return;   // DB has been seeded
            }

            var times = new string[]
            {
                "8:15am",
                "8:30am",
                "8:45am",
                "9:0am",

                "9:15am",
                "9:30am",
                "9:45am",
                "10:0am",

                "10:15am",
                "10:30am",
                "10:45am",
                "11:0am",

                "11:15am",
                "11:30am",
                "11:45am",
                "12:0pm",

                "12:15pm",
                "12:30pm",
                "12:45pm",
                "13:0pm",

                "13:15pm",
                "13:30pm",
                "13:45pm",
                "14:0pm",

                "14:15pm",
                "14:30pm",
                "14:45pm",
                "15:0pm",

                "15:15pm",
                "15:30pm",
                "15:45pm",
                "16:0pm",

                "16:15pm",
                "16:30pm",
                "16:45pm",
                "17:0pm",
            };

            TAUser initUser = null;
            foreach (TAUser user in um.Users)
            {
                if (user.Unid == "u0000000")
                    initUser = user;
            }

            for (int i = 0; i < 16; i++)
            {
                this.Slot.Add(new Slot { DayAndTime = "Monday " + times[i], IsOpen = true, UserID = initUser });
                this.Slot.Add(new Slot { DayAndTime = "Friday " + times[i], IsOpen = true, UserID = initUser });
            }
            for (int i = 16; i < times.Length; i++)
            {
                this.Slot.Add(new Slot { DayAndTime = "Tuesday " + times[i], IsOpen = true, UserID = initUser });
                this.Slot.Add(new Slot { DayAndTime = "Thursday " + times[i], IsOpen = true, UserID = initUser });
            }
            this.SaveChanges();
        }


        public async Task InitializeEnrollmentOverTime(UserManager<TAUser> um)
        {
            // Look for any applications.
            if (this.EnrollmentOverTime.Any())
            {
                return;   // DB has been seeded
            }

            // read CSV File
            ReadCSVFile();

            this.SaveChanges();
        }

        private void ReadCSVFile()
        {
            //string filePath = @"../temp.csv";
            string filePath = "Data\\temp.csv";
            string[] dates;
            int len = 0;
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // read the first line
                    string firstLine = reader.ReadLine();
                    if (firstLine != null)
                    {
                        string[] split = firstLine.Split('\u002C');
                        len = split.Length;
                        dates = new string[len];

                        // split Copy To dates
                        for (int i = 1; i < split.Length; i++)
                        {
                            dates[i] = split[i];
                        }

                    }
                    else
                    {
                        return;
                    }

                    // read the rest of the lines
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        string[] split = line.Split('\u002C');

                        // add each course and its enrollments to the enrollmentovertime table
                        var courseDeptNum = split[0].Split(' ');
                        var course = courseDeptNum[0] + courseDeptNum[1];
                        for (int i = 1; i < split.Length; i++)
                        {
                            var enrollment = Int32.Parse(split[i]);
                            var date = dates[i];
                            this.EnrollmentOverTime.Add(new EnrollmentOverTime { Enrollment = enrollment, EnrollmentDate = date, Course = course });
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

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
        public DbSet<Course> Course { get; set; }
        public DbSet<Slot> Slot { get; set; }
        public DbSet<EnrollmentOverTime> EnrollmentOverTime { get; set; }
    }
}