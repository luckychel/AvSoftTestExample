using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CountersController : Controller
    {
        private readonly CountersContext _context;

        public CountersController(CountersContext context)
        {
            _context = context;
        }

        // GET: Counters
        public async Task<IActionResult> Index()
        {
              return View(await _context.GetCounters());
        }

        // GET: Counters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Counters == null)
            {
                return NotFound();
            }

            var counters = await _context.Counters
                .FirstOrDefaultAsync(m => m.Id == id);

            if (counters == null)
            {
                return NotFound();
            }

            return View(counters);
        }

        // GET: Counters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Counters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Key,Value")] Counters counters)
        {
            if (ModelState.IsValid)
            {
                await _context.SaveCounters(counters);
                return RedirectToAction(nameof(Index));
            }
            return View(counters);
        }

        // GET: Counters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Counters == null)
            {
                return NotFound();
            }

            var counters = await _context.Counters.FindAsync(id);
            if (counters == null)
            {
                return NotFound();
            }
            return View(counters);
        }

        // POST: Counters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Key,Value")] Counters counters)
        {
            if (id != counters.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(counters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.CountersExists(counters.Id))
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
            return View(counters);
        }

        // GET: Counters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Counters == null)
            {
                return NotFound();
            }

            var counters = await _context.Counters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (counters == null)
            {
                return NotFound();
            }

            return View(counters);
        }

        // POST: Counters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Counters == null)
            {
                return Problem("Entity set 'CountersContext.Counters'  is null.");
            }
            var counters = await _context.Counters.FindAsync(id);
            if (counters != null)
            {
                _context.Counters.Remove(counters);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Report()
        {
            return View(await _context.GetReport());
        }

        public async Task<JsonResult> CountersJson()
        {
            return Json(await _context.GetCounters());
        }

        public async Task<JsonResult> ReportJson()
        {
            return Json(await _context.GetReport());
        }

    }
}
