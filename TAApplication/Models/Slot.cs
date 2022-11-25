/**
  Author:    Nasser Mughrabi
  Partner:   None   
  Date:      25-November-2022
  Course:    CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Nasser Mughrabi - This work may not be copied for use in Academic Coursework.
  I, Nasser Mughrabi, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source. Any references used in the completion of the assignment are cited in my README file.
  
File Contents:
    This class is to build availabiltiy slots table in the database with certain schema and fields
 */


using TAApplication.Areas.Data;

namespace TAApplication.Models
{
    public class Slot
    {

        public int ID { get; set; }

        public string DayAndTime { get; set; } = string.Empty;

        public bool IsOpen { get; set; }

        // Navigation property
        public TAUser UserID { get; set; } = null!;


    }
}
