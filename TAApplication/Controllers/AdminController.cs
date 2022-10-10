using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using System.Web.Helpers;
using TAApplication.Areas.Data;
using TAApplication.Data;
using TAApplication.Models;

namespace TAApplication.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private ApplicationDbContext _context;
        private UserManager<TAUser> _userManager;

        public AdminController(ILogger<AdminController> logger, ApplicationDbContext context, UserManager<TAUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public ActionResult GetUsers()
        {
            var users = from u in _context.Users select u.Email;
            var data = users.ToList();
            return Json(new {data=data});
        }

        [HttpPost]
        public async Task<IActionResult> changeRole(string id, string role)
        {
            var userQuery = from u in _context.Users where u.Id == id select u;
            string roleId = (from r in _context.Roles where r.Name == role select r).First<IdentityRole<string>>().Id;
            // check if user exists
            if (!userQuery.Any<TAUser>())
                return BadRequest(new { success = false, message = id + " not found" });

            else
            {
                var query = from u in _context.UserRoles where u.UserId == id select u;
                IdentityUserRole<string> user = null;
                // check through query and see if the user already has the role
                foreach (IdentityUserRole<string> userLoop in query)
                {
                    if (userLoop.RoleId == roleId)
                        user = userLoop;
                }

                if (user == null)
                {
                    Console.WriteLine("notfound roleid: " + role + " userid: " + id);
                    // Occurs if role is not found on user so add it
                    await _context.UserRoles.AddAsync(new IdentityUserRole<string> { UserId = id, RoleId = roleId });
                    await _context.SaveChangesAsync();
                    return Ok(new { success = true, message = id + " added role" });
                }
                else
                {
                    Console.WriteLine("removing " + role);
                    // if user has role, remove it
                    _context.UserRoles.Remove(user);
                    await _context.SaveChangesAsync();
                    return Ok(new { success = true, message = id + " removed role" });
                }

            }

        }

        public IActionResult Roles()
        {
            return View();
        }

        public IActionResult Tables()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}