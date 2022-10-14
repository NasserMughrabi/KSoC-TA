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
