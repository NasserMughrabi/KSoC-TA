﻿using System.ComponentModel.DataAnnotations;
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
