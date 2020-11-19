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
    public class BillingRateController : Controller
    {
        private readonly WorkTimeContext _context;

        public BillingRateController(WorkTimeContext context)
        {
            _context = context;
        }

        // GET: BillingRate
        public async Task<IActionResult> Index()
        {
            return View(await _context.BillingRates.ToListAsync());
        }

        // GET: BillingRate/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingRateModel = await _context.BillingRates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billingRateModel == null)
            {
                return NotFound();
            }

            return View(billingRateModel);
        }

        // GET: BillingRate/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillingRate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,BillingRate,Date,UpdatedAt")] BillingRateModel billingRateModel)
        {
            if (ModelState.IsValid)
            {
                billingRateModel.Id = Guid.NewGuid();
                _context.Add(billingRateModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billingRateModel);
        }

        // GET: BillingRate/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingRateModel = await _context.BillingRates.FindAsync(id);
            if (billingRateModel == null)
            {
                return NotFound();
            }
            return View(billingRateModel);
        }

        // POST: BillingRate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,BillingRate,Date,UpdatedAt")] BillingRateModel billingRateModel)
        {
            if (id != billingRateModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billingRateModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillingRateModelExists(billingRateModel.Id))
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
            return View(billingRateModel);
        }

        // GET: BillingRate/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingRateModel = await _context.BillingRates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billingRateModel == null)
            {
                return NotFound();
            }

            return View(billingRateModel);
        }

        // POST: BillingRate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var billingRateModel = await _context.BillingRates.FindAsync(id);
            _context.BillingRates.Remove(billingRateModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillingRateModelExists(Guid id)
        {
            return _context.BillingRates.Any(e => e.Id == id);
        }
    }
}
