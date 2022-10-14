/**
  Author:    Nasser Mughrabi
  Partner:   None   
  Date:      13-October-2022
  Course:    CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Nasser Mughrabi - This work may not be copied for use in Academic Coursework.
  I, Nasser Mughrabi, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source. Any references used in the completion of the assignment are cited in my README file.
  
File Contents:
    This class is to create a new identity with certain instance variables (TAUser)
 */

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TAApplication.Areas.Data
{
    [Index(nameof(Unid), IsUnique =true)]
    public class TAUser : IdentityUser
    {
        public string? Unid { get; set; }
        public string? Name { get; set; }
        public string? RefferedTo { get; set; }
    }
}
