/**
  Author:    Nasser Mughrabi
  Partner:   None   
  Date:      14-December-2022
  Course:    CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Nasser Mughrabi - This work may not be copied for use in Academic Coursework.
  I, Nasser Mughrabi, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source. Any references used in the completion of the assignment are cited in my README file.
  
File Contents:
    This class is to build courses table in the database with certain schema and fields
 */

using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace TAApplication.Models
{
    public class EnrollmentOverTime
    {
        public int ID { get; set; }

        [Required]
        public int Enrollment { get; set; }

        [Required]
        public string EnrollmentDate { get; set; } = string.Empty;

        [Required]
        public string Course { get; set; } = string.Empty;
    }
}
