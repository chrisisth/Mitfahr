using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mitfahr.Data;
using mitfahr.Models;

namespace mitfahr.Controllers
{
    public class JourneysController : Controller
    {
        private readonly mitfahrgelegenheitContext _context;

        public JourneysController(mitfahrgelegenheitContext context)
        {
            _context = context;
        }

        // GET: Journeys
        public async Task<IActionResult> Index()
        {
            var mitfahrgelegenheitContext = _context.Journey.Include(j => j.DeparturePointPostcodeNavigation).Include(j => j.UserIdUserNavigation);
            return View(await mitfahrgelegenheitContext.ToListAsync());
        }

        // GET: Journeys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journey = await _context.Journey
                .Include(j => j.DeparturePointPostcodeNavigation)
                .Include(j => j.UserIdUserNavigation)
                .FirstOrDefaultAsync(m => m.Idjourney == id);
            if (journey == null)
            {
                return NotFound();
            }

            return View(journey);
        }

        // GET: Journeys/Create
        public IActionResult Create()
        {
            ViewData["DeparturePointPostcode"] = new SelectList(_context.DeparturePoint, "Postcode", "Postcode");
            ViewData["UserIdUser"] = new SelectList(_context.User, "IdUser", "EMail");
            return View();
        }

        // POST: Journeys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idjourney,DepartureTime,Regularly,Smoker,DeparturePointPostcode,UserIdUser")] Journey journey)
        {
            if (ModelState.IsValid)
            {
                _context.Add(journey);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeparturePointPostcode"] = new SelectList(_context.DeparturePoint, "Postcode", "Postcode", journey.DeparturePointPostcode);
            ViewData["UserIdUser"] = new SelectList(_context.User, "IdUser", "EMail", journey.UserIdUser);
            return View(journey);
        }

        // GET: Journeys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journey = await _context.Journey.FindAsync(id);
            if (journey == null)
            {
                return NotFound();
            }
            ViewData["DeparturePointPostcode"] = new SelectList(_context.DeparturePoint, "Postcode", "Postcode", journey.DeparturePointPostcode);
            ViewData["UserIdUser"] = new SelectList(_context.User, "IdUser", "EMail", journey.UserIdUser);
            return View(journey);
        }

        // POST: Journeys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idjourney,DepartureTime,Regularly,Smoker,DeparturePointPostcode,UserIdUser")] Journey journey)
        {
            if (id != journey.Idjourney)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(journey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JourneyExists(journey.Idjourney))
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
            ViewData["DeparturePointPostcode"] = new SelectList(_context.DeparturePoint, "Postcode", "Postcode", journey.DeparturePointPostcode);
            ViewData["UserIdUser"] = new SelectList(_context.User, "IdUser", "EMail", journey.UserIdUser);
            return View(journey);
        }

        // GET: Journeys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journey = await _context.Journey
                .Include(j => j.DeparturePointPostcodeNavigation)
                .Include(j => j.UserIdUserNavigation)
                .FirstOrDefaultAsync(m => m.Idjourney == id);
            if (journey == null)
            {
                return NotFound();
            }

            return View(journey);
        }

        // POST: Journeys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var journey = await _context.Journey.FindAsync(id);
            _context.Journey.Remove(journey);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JourneyExists(int id)
        {
            return _context.Journey.Any(e => e.Idjourney == id);
        }
    }
}
