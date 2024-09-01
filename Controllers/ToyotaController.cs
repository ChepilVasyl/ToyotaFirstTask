using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToyotaProject.Data;
using ToyotaProject.Models.MyModels;

namespace ToyotaProject.Controllers
{
    public class ToyotaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToyotaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Toyota
        public async Task<IActionResult> Index(
            string sortCol="NameModel", string sortDirec="asc",
            int pageSize = 3, int pageNumber = 1
            )
        {
            var qwery = _context.ToyotaModels.AsQueryable();
            qwery = sortCol switch
            {
                "Id" => sortDirec=="asc" ? qwery.OrderBy(c => c.Id):qwery.OrderByDescending(c => c.Id),
                "NameModel" => sortDirec=="asc" ? qwery.OrderBy(c => c.NameModel):qwery.OrderByDescending(c => c.NameModel),
                _ => sortDirec=="asc" ? qwery.OrderBy(c => c.NameModel):qwery.OrderByDescending(c => c.NameModel)
            };
            var countItems = await _context.ToyotaModels.CountAsync();
            var rez = await qwery
                .Skip((pageNumber - 1)*pageSize)
                .Take(pageSize)
                .ToListAsync();
            ViewData["paginateRez"] = new PaginateViewModel
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                SortColumn = sortCol,
                SortDirection = sortDirec,
                TotalItems = countItems,
                TotalPage = (int)Math.Ceiling(countItems / (double)pageSize)
            };
            return View(rez);
        }

        // GET: Toyota/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toyotaModel = await _context.ToyotaModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toyotaModel == null)
            {
                return NotFound();
            }

            return View(toyotaModel);
        }

        // GET: Toyota/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Toyota/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameModel")] ToyotaModel toyotaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toyotaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toyotaModel);
        }

        // GET: Toyota/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toyotaModel = await _context.ToyotaModels.FindAsync(id);
            if (toyotaModel == null)
            {
                return NotFound();
            }
            return View(toyotaModel);
        }

        // POST: Toyota/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameModel")] ToyotaModel toyotaModel)
        {
            if (id != toyotaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toyotaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToyotaModelExists(toyotaModel.Id))
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
            return View(toyotaModel);
        }

        // GET: Toyota/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toyotaModel = await _context.ToyotaModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toyotaModel == null)
            {
                return NotFound();
            }

            return View(toyotaModel);
        }

        // POST: Toyota/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toyotaModel = await _context.ToyotaModels.FindAsync(id);
            if (toyotaModel != null)
            {
                _context.ToyotaModels.Remove(toyotaModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToyotaModelExists(int id)
        {
            return _context.ToyotaModels.Any(e => e.Id == id);
        }
    }
}
