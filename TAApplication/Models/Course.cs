using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace TAApplication.Models
{
    public class Course
    {
        public int ID { get; set; }

        [Display(
            Name = "Semester",
            ShortName = "Semester",
            Prompt = "Fall",
            Description = "Semester course offered"
            )]
        [Required]
        public string Semester { get; set; } = string.Empty;

        [Display(
            Name = "Year",
            ShortName = "Year",
            Prompt = "2023",
            Description = "Year course offered"
            )]
        [Required]
        public int Year { get; set; } 

        [Display(
            Name = "Title",
            ShortName = "Title",
            Prompt = "Web Development",
            Description = "Title of the course"
            )]
        [Required]
        public string Title { get; set; } = string.Empty;

        [Display(
            Name = "Department",
            ShortName = "Department",
            Prompt = "CS",
            Description = "Department that the course is in"
            )]
        [Required]
        public string Department { get; set; } = string.Empty;

        [Display(
            Name = "Number",
            ShortName = "Number",
            Prompt = "2420",
            Description = "Course Number"
            )]
        [Required]
        public int Number { get; set; }

        [Display(
            Name = "Section",
            ShortName = "Section",
            Prompt = "001",
            Description = "Course Section"
            )]
        [Required]
        public int Section { get; set; }

        [Display(
            Name = "Description",
            ShortName = "Description",
            Prompt = "",
            Description = "Course Description"
            )]
        public string Description { get; set; } = string.Empty;

        [Display(
            Name = "Professor UID",
            ShortName = "Professor UNID",
            Prompt = "u1234567",
            Description = "Professor University of Utah ID"
            )]
        [Required]
        public string ProfessorUnid { get; set; } = string.Empty;

        [Display(
            Name = "Professor Name",
            ShortName = "Professor Name",
            Prompt = "Danny Kopta",
            Description = "Professor Name"
            )]
        [Required]
        public string ProfessorName { get; set; } = string.Empty;

        [Display(
            Name = "Offered",
            ShortName = "Scheduel",
            Prompt = "M/W 3:30-5:00",
            Description = "Time and Days Offered"
            )]
        [Required]
        public string TimeAndDaysOffered { get; set; } = string.Empty;

        [Display(
            Name = "Location",
            ShortName = "Location",
            Prompt = "WEB L 104",
            Description = "Building name and classroom number"
            )]
        [Required]
        public string Location { get; set; } = string.Empty;

        [Display(
            Name = "Credit",
            ShortName = "Credit",
            Prompt = "3",
            Description = "Credit Hours"
            )]
        [Required]
        public int Credit { get; set; }

        [Display(
            Name = "Enrollment",
            ShortName = "Enrollment",
            Prompt = "150",
            Description = "How many students enrolled in the course"
            )]
        [Required]
        public int Enrollment { get; set; }

        [Display(
            Name = "Note",
            ShortName = "Note",
            Prompt = "Needs extra TAs",
            Description = "Admin note on the course"
            )]
        public string Note { get; set; } = string.Empty;

    }
}
