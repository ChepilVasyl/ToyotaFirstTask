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
    public class ComplectationColorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComplectationColorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ComplectationColor
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ComplectationColorModels.Include(c => c.Color).Include(c => c.Complectation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ComplectationColor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complectationColorModel = await _context.ComplectationColorModels
                .Include(c => c.Color)
                .Include(c => c.Complectation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complectationColorModel == null)
            {
                return NotFound();
            }

            return View(complectationColorModel);
        }

        // GET: ComplectationColor/Create
        public IActionResult Create()
        {
            ViewData["ColorId"] = new SelectList(_context.ColorModels, "Id", "NameColor");
            ViewData["ComplectationId"] = new SelectList(_context.ComplectationModels, "Id", "NameComplectation");
            return View();
        }

        // POST: ComplectationColor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ColorId,ComplectationId")] ComplectationColorModel complectationColorModel,IFormFile file)
        {
            if (file != null && file.Length != 0)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Cars", file.FileName);
                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            complectationColorModel.MainImageUrl = "Cars/" + file.FileName;
            if (ModelState.IsValid)
            {
                _context.Add(complectationColorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColorId"] = new SelectList(_context.ColorModels, "Id", "NameColor", complectationColorModel.ColorId);
            ViewData["ComplectationId"] = new SelectList(_context.ComplectationModels, "Id", "NameComplectation", complectationColorModel.ComplectationId);
            return View(complectationColorModel);
        }

        // GET: ComplectationColor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complectationColorModel = await _context.ComplectationColorModels.FindAsync(id);
            if (complectationColorModel == null)
            {
                return NotFound();
            }
            ViewData["ColorId"] = new SelectList(_context.ColorModels, "Id", "NameColor", complectationColorModel.ColorId);
            ViewData["ComplectationId"] = new SelectList(_context.ComplectationModels, "Id", "NameComplectation", complectationColorModel.ComplectationId);
            return View(complectationColorModel);
        }

        // POST: ComplectationColor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MainImageUrl,ColorId,ComplectationId")] ComplectationColorModel complectationColorModel)
        {
            if (id != complectationColorModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complectationColorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplectationColorModelExists(complectationColorModel.Id))
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
            ViewData["ColorId"] = new SelectList(_context.ColorModels, "Id", "Id", complectationColorModel.ColorId);
            ViewData["ComplectationId"] = new SelectList(_context.ComplectationModels, "Id", "Id", complectationColorModel.ComplectationId);
            return View(complectationColorModel);
        }

        // GET: ComplectationColor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complectationColorModel = await _context.ComplectationColorModels
                .Include(c => c.Color)
                .Include(c => c.Complectation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complectationColorModel == null)
            {
                return NotFound();
            }

            return View(complectationColorModel);
        }

        // POST: ComplectationColor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complectationColorModel = await _context.ComplectationColorModels.FindAsync(id);
            if (complectationColorModel != null)
            {
                _context.ComplectationColorModels.Remove(complectationColorModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplectationColorModelExists(int id)
        {
            return _context.ComplectationColorModels.Any(e => e.Id == id);
        }
    }
}
