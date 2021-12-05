using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LifeAfterSchool.Data;
using LifeAfterSchool.Models;
using Microsoft.AspNetCore.Authorization;

namespace LifeAfterSchool.Controllers
{
    public class AttendeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attendees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Attendees.ToListAsync());
        }

        // GET: Attendees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendees = await _context.Attendees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendees == null)
            {
                return NotFound();
            }

            return View(attendees);
        }

        // GET: Attendees/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attendees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Department,PhoneNo,Email")] Attendees attendees)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attendees);
        }

        // GET: Attendees/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendees = await _context.Attendees.FindAsync(id);
            if (attendees == null)
            {
                return NotFound();
            }
            return View(attendees);
        }

        // POST: Attendees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Department,PhoneNo,Email")] Attendees attendees)
        {
            if (id != attendees.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendeesExists(attendees.Id))
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
            return View(attendees);
        }

        // GET: Attendees/Delete/5
        [Authorize(User=)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendees = await _context.Attendees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendees == null)
            {
                return NotFound();
            }

            return View(attendees);
        }

        // POST: Attendees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendees = await _context.Attendees.FindAsync(id);
            _context.Attendees.Remove(attendees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendeesExists(int id)
        {
            return _context.Attendees.Any(e => e.Id == id);
        }
    }
}
