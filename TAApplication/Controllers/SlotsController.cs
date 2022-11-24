using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TAApplication.Areas.Data;
using TAApplication.Data;
using TAApplication.Models;
using static System.Reflection.Metadata.BlobBuilder;

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
            return View(await _context.Slot.ToListAsync());
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
                        Console.WriteLine(slot);
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
            return Ok(new { success = true, message = id + " added role" });
        }

        // Return an array of slots representing the current users availability
        public async Task<IActionResult> GetSchedule()
        {
            return Ok(new { success = true, message = await _context.Slot.ToListAsync() });
        }

        // GET: Slots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Slot == null)
            {
                return NotFound();
            }

            var slot = await _context.Slot
                .FirstOrDefaultAsync(m => m.ID == id);
            if (slot == null)
            {
                return NotFound();
            }

            return View(slot);
        }

        // GET: Slots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Slots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DayAndTime,IsOpen")] Slot slot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(slot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slot);
        }

        // GET: Slots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Slot == null)
            {
                return NotFound();
            }

            var slot = await _context.Slot.FindAsync(id);
            if (slot == null)
            {
                return NotFound();
            }
            return View(slot);
        }

        // POST: Slots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DayAndTime,IsOpen")] Slot slot)
        {
            if (id != slot.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SlotExists(slot.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(slot);
        }

        // GET: Slots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Slot == null)
            {
                return NotFound();
            }

            var slot = await _context.Slot
                .FirstOrDefaultAsync(m => m.ID == id);
            if (slot == null)
            {
                return NotFound();
            }

            return View(slot);
        }

        // POST: Slots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Slot == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Slot'  is null.");
            }
            var slot = await _context.Slot.FindAsync(id);
            if (slot != null)
            {
                _context.Slot.Remove(slot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SlotExists(int id)
        {
            return _context.Slot.Any(e => e.ID == id);
        }
    }
}
