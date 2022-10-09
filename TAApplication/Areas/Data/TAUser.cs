using Microsoft.AspNetCore.Identity;

namespace TAApplication.Areas.Data
{
    public class TAUser : IdentityUser
    {
        public string Unid { get; set; }
    }
}
