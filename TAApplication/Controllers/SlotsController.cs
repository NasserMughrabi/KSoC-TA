/**
  Author:    Nasser Mughrabi
File Contents:
    This class is a controller to handle url page navigations and model interactions for the availability graphs page
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAApplication.Data;
using TAApplication.Models;

namespace TAApplication.Controllers
{
    [Authorize]
    public class SlotsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SlotsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Slots
        public async Task<IActionResult> Index()
        {
            //var list = await _context.Slot.ToListAsync();
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item.IsOpen);
            //}

            var slots = _context.Slot.Include(u => u.UserID).Where(u => u.UserID.Unid == User.Identity.Name).Select(u => u).ToListAsync();
            return View(await slots);
        }

        // Given an array of slots, update the DB to reflect the current users availability
        [HttpPost]
        public async Task<IActionResult> SetSchedule(string id, List<string> times)
        {
            // if the time exists in the model then we update it to be the opposite value 
            // otherwise, add it to the model with isOpen value as false (meaning the slot is closed)
            foreach (var item in times)
            {
                if (_context.Slot.Any())
                {
                    var value = _context.Slot.Include(u => u.UserID).Select(u => u).FirstOrDefault();
                    var slot = _context.Slot.Include(u => u.UserID).Where(u => u.UserID.Id == id && u.DayAndTime == item).Select(u => u).ToArray();

                    // if the slot exists, update its isOpen boolean
                    if (slot.Any())
                    {
                        slot.First().IsOpen = !slot.First().IsOpen;
                    }
                    else
                    {
                        // add item to the model
                        var userId = from u in _context.Users where u.Id == id select u;
                        var user = userId.First();
                        await _context.Slot.AddAsync(new Slot { DayAndTime = item, IsOpen = true, UserID = user });
                    }
                }
                else
                {
                    // add item to the model
                    var userId = from u in _context.Users where u.Id == id select u;
                    var user = userId.First();
                    await _context.Slot.AddAsync(new Slot { DayAndTime = item, IsOpen = true, UserID = user });
                }
            }
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = id + " added slot(s)" });
        }

        // Return an array of slots representing the current users availability
        public async Task<IActionResult> GetSchedule()
        {
            var slots = _context.Slot.Include(u => u.UserID).Where(u => u.UserID.Unid == User.Identity.Name).Select(u => u).ToListAsync();
            return Ok(new { success = true, message = await slots });
        }

    }
}
