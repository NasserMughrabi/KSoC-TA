/**
  Author:    Nasser Mughrabi
File Contents:
    This class is to build an applications table in the database with certain schema and fields
 */

using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;
using TAApplication.Areas.Data;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace TAApplication.Models
{
    public enum Pursuing
    {
        BS, MS, PhD
    }
    public class Application : ModificationTracking
    {
        public int ID { get; set; }

        [Display(
            Name = "Current Degree",
            ShortName = "Degree",
            Prompt = "BS",
            Description = "Please choose a degree"
            )]
        [Required]
        public Pursuing Pursuing { get; set; }

        [Display(
            Name = "Current Department",
            ShortName = "Department",
            Prompt = "CS",
            Description = "Please enter your department"
            )]
        [Required]
        public string Department { get; set; } = string.Empty;

        [Display(
            Name = "Grade Point Average",
            ShortName = "GPA",
            Prompt = "3.2",
            Description = "Please give a gpa between 0 and 4"
            )]
        [Required]
        [Range(0, 4, ErrorMessage = "Please enter number between 0 and 4")]
        public float GPA { get; set; }

        [Display(
            Name = "Desired Hours Per Week",
            ShortName = "Weekly Hours",
            Prompt = "15",
            Description = "Please enter desired number of hours"
            )]
        [Required]
        [Range(5, 20, ErrorMessage = "Please enter number between 5 and 20")]
        public int Hours { get; set; }

        [Display(
            Name = "availability week before semester start",
            ShortName = "Week Before",
            Prompt = "True",
            Description = "Are you available week before semester begins"
            )]
        [Required]
        public bool WeekBefore { get; set; }

        [Display(
            Name = "Completed Semesters at University of Utah",
            ShortName = "Completed Semesters",
            Prompt = "3",
            Description = "Please enter number of semesters completed at the University of Utah"
            )]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int CompletedSemesters { get; set; }

        [Display(
            Name = "Personal Statement",
            ShortName = "Personal Statement",
            Prompt = "I am John ...",
            Description = "Write something you want hiring manger to know about you"
            )]
        [StringLength(50000, ErrorMessage = "Your statement cannot exceed 50000 characters. ")]
        [DataType(DataType.MultilineText)]
        public string? Statement { get; set; }

        [Display(
            Name = "Transfer School",
            ShortName = "Transfer School",
            Prompt = "SLCC",
            Description = "What school did you transfer from if applicable"
            )]
        [StringLength(150, ErrorMessage = "Your statement cannot exceed 150 characters. ")]
        public string? TransferSchool { get; set; }

        [Display(
            Name = "Linkedin Account",
            ShortName = "Linkedin",
            Prompt = "https://www.linkedin.com/in/<username>",
            Description = "What is your linkedin"
            )]
        [Url]
        public string? Linkedin { get; set; }

        [Display(
            Name = "Your Resume",
            ShortName = "Resume",
            Prompt = "Resume File",
            Description = "upload your resume file"
            )]
        public string? ResumeFile { get; set; }

        [ScaffoldColumn(false)]
        public string UserID { get; set; } = string.Empty;

        // Navigation Property
        public TAUser User { get; set; } = null!;
    }
}
