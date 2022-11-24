using Microsoft.AspNetCore.Identity;
using System.Globalization;
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
