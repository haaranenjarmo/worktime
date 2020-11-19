using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkTime.Data;
using WorkTime.Models;

namespace WorkTime.Controllers
{
    public class WorkHourController : Controller
    {
        private readonly WorkTimeContext _context;

        public WorkHourController(WorkTimeContext context)
        {
            _context = context;
        }

        // GET: WorkHour
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkHours.ToListAsync());
        }

        // GET: WorkHour/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workHourModel = await _context.WorkHours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workHourModel == null)
            {
                return NotFound();
            }

            return View(workHourModel);
        }

        // GET: WorkHour/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkHour/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,StartTime,EndTime,WorkedTime,ProjectId,Note,Billable,CreatedAt,UpdatedAt,UpdatedBy")] WorkHourModel workHourModel)
        {
            if (ModelState.IsValid)
            {
                workHourModel.Id = Guid.NewGuid();
                _context.Add(workHourModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workHourModel);
        }

        // GET: WorkHour/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workHourModel = await _context.WorkHours.FindAsync(id);
            if (workHourModel == null)
            {
                return NotFound();
            }
            return View(workHourModel);
        }

        // POST: WorkHour/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,StartTime,EndTime,WorkedTime,ProjectId,Note,Billable,CreatedAt,UpdatedAt,UpdatedBy")] WorkHourModel workHourModel)
        {
            if (id != workHourModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workHourModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkHourModelExists(workHourModel.Id))
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
            return View(workHourModel);
        }

        // GET: WorkHour/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workHourModel = await _context.WorkHours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workHourModel == null)
            {
                return NotFound();
            }

            return View(workHourModel);
        }

        // POST: WorkHour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var workHourModel = await _context.WorkHours.FindAsync(id);
            _context.WorkHours.Remove(workHourModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkHourModelExists(Guid id)
        {
            return _context.WorkHours.Any(e => e.Id == id);
        }
    }
}
