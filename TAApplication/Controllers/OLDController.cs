﻿using Microsoft.AspNetCore.Authorization;
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