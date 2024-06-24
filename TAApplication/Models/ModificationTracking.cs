/**
  Author:    Nasser Mughrabi
File Contents:
    This class is to build an applications table in the database with 
    certain schema and fields to track changes timestamps
 */

using System.ComponentModel.DataAnnotations;

namespace TAApplication.Models
{
    public class ModificationTracking
    {
        [ScaffoldColumn(false)]
        public DateTime CreationDate { get; set; }

        [ScaffoldColumn(false)]
        public DateTime ModificationDate { get; set; }

        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; } = string.Empty;

        [ScaffoldColumn(false)]
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
