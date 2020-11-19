using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkTime.Data;
using WorkTime.Models;

namespace WorkTime.Controllers
{
    [Authorize]
    public class FlexTimeController : Controller
    {
        private readonly WorkTimeContext _context;

        public FlexTimeController(WorkTimeContext context)
        {
            _context = context;
        }

        // GET: FlexTime
        public async Task<IActionResult> Index()
        {
            return View(await _context.FlexTimes.ToListAsync());
        }

        // GET: FlexTime/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flexTimeModel = await _context.FlexTimes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flexTimeModel == null)
            {
                return NotFound();
            }

            return View(flexTimeModel);
        }

        // GET: FlexTime/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FlexTime/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Date,FlexBalance,UpdatedAt")] FlexTimeModel flexTimeModel)
        {
            if (ModelState.IsValid)
            {
                flexTimeModel.Id = Guid.NewGuid();
                _context.Add(flexTimeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flexTimeModel);
        }

        // GET: FlexTime/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flexTimeModel = await _context.FlexTimes.FindAsync(id);
            if (flexTimeModel == null)
            {
                return NotFound();
            }
            return View(flexTimeModel);
        }

        // POST: FlexTime/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,Date,FlexBalance,UpdatedAt")] FlexTimeModel flexTimeModel)
        {
            if (id != flexTimeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flexTimeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlexTimeModelExists(flexTimeModel.Id))
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
            return View(flexTimeModel);
        }

        // GET: FlexTime/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flexTimeModel = await _context.FlexTimes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flexTimeModel == null)
            {
                return NotFound();
            }

            return View(flexTimeModel);
        }

        // POST: FlexTime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var flexTimeModel = await _context.FlexTimes.FindAsync(id);
            _context.FlexTimes.Remove(flexTimeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlexTimeModelExists(Guid id)
        {
            return _context.FlexTimes.Any(e => e.Id == id);
        }
    }
}
