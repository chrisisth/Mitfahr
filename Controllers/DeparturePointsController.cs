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
    public class DeparturePointsController : Controller
    {
        private readonly mitfahrgelegenheitContext _context;

        public DeparturePointsController(mitfahrgelegenheitContext context)
        {
            _context = context;
        }

        // GET: DeparturePoints
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeparturePoint.ToListAsync());
        }

        // GET: DeparturePoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departurePoint = await _context.DeparturePoint
                .FirstOrDefaultAsync(m => m.Postcode == id);
            if (departurePoint == null)
            {
                return NotFound();
            }

            return View(departurePoint);
        }

        // GET: DeparturePoints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeparturePoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Postcode,City")] DeparturePoint departurePoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departurePoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departurePoint);
        }

        // GET: DeparturePoints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departurePoint = await _context.DeparturePoint.FindAsync(id);
            if (departurePoint == null)
            {
                return NotFound();
            }
            return View(departurePoint);
        }

        // POST: DeparturePoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Postcode,City")] DeparturePoint departurePoint)
        {
            if (id != departurePoint.Postcode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departurePoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeparturePointExists(departurePoint.Postcode))
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
            return View(departurePoint);
        }

        // GET: DeparturePoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departurePoint = await _context.DeparturePoint
                .FirstOrDefaultAsync(m => m.Postcode == id);
            if (departurePoint == null)
            {
                return NotFound();
            }

            return View(departurePoint);
        }

        // POST: DeparturePoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departurePoint = await _context.DeparturePoint.FindAsync(id);
            _context.DeparturePoint.Remove(departurePoint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeparturePointExists(int id)
        {
            return _context.DeparturePoint.Any(e => e.Postcode == id);
        }
    }
}
