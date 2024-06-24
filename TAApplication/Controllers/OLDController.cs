/**
  Author:    Nasser Mughrabi
File Contents:
    This class is a controller to direct user to old views' urls
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TAApplication.Controllers
{
    [Authorize]
    public class OLDController : Controller
    {
        public IActionResult ApplicantCreate()
        {
            return View();
        }

        public IActionResult ApplicantDetails()
        {
            return View();
        }

        public IActionResult ApplicantList()
        {
            return View();
        }
    }
}