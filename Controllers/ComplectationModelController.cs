using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToyotaProject.Data;
using ToyotaProject.Models.MyModels;

namespace ToyotaProject.Controllers
{
    public class ComplectationModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComplectationModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ComplectationModel
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ComplectationModels.Include(c => c.Model);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ComplectationModel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complectationModel = await _context.ComplectationModels
                .Include(c => c.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complectationModel == null)
            {
                return NotFound();
            }

            return View(complectationModel);
        }

        // GET: ComplectationModel/Create
        public IActionResult Create()
        {
            ViewBag.NamesCompl = new SelectList(_context.ToyotaModels,"Id","NameModel");
            return View();
        }

        // POST: ComplectationModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameComplectation,ModelId")] ComplectationModel complectationModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complectationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complectationModel);
        }

        // GET: ComplectationModel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complectationModel = await _context.ComplectationModels.FindAsync(id);
            if (complectationModel == null)
            {
                return NotFound();
            }
            ViewData["ModelId"] = new SelectList(_context.ToyotaModels, "Id", "NameModel", complectationModel.ModelId);
            return View(complectationModel);
        }

        // POST: ComplectationModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameComplectation,ModelId")] ComplectationModel complectationModel)
        {
            if (id != complectationModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complectationModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplectationModelExists(complectationModel.Id))
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
            ViewData["ModelId"] = new SelectList(_context.ToyotaModels, "Id", "Id", complectationModel.ModelId);
            return View(complectationModel);
        }

        // GET: ComplectationModel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complectationModel = await _context.ComplectationModels
                .Include(c => c.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complectationModel == null)
            {
                return NotFound();
            }

            return View(complectationModel);
        }

        // POST: ComplectationModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complectationModel = await _context.ComplectationModels.FindAsync(id);
            if (complectationModel != null)
            {
                _context.ComplectationModels.Remove(complectationModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplectationModelExists(int id)
        {
            return _context.ComplectationModels.Any(e => e.Id == id);
        }
    }
}
