using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_Laboratorio4.Data;
using ProyectoFinal_Laboratorio4.Models;

namespace ProyectoFinal_Laboratorio4.Controllers
{
    public class CategoríasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoríasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categorías
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorías.ToListAsync());
        }

        // GET: Categorías/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoría = await _context.Categorías
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoría == null)
            {
                return NotFound();
            }

            return View(categoría);
        }

        [Authorize]
        // GET: Categorías/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorías/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripción")] Categoría categoría)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoría);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoría);
        }

        [Authorize]
        // GET: Categorías/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoría = await _context.Categorías.FindAsync(id);
            if (categoría == null)
            {
                return NotFound();
            }
            return View(categoría);
        }

        // POST: Categorías/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripción")] Categoría categoría)
        {
            if (id != categoría.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoría);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoríaExists(categoría.Id))
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
            return View(categoría);
        }

        [Authorize]
        // GET: Categorías/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoría = await _context.Categorías
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoría == null)
            {
                return NotFound();
            }

            return View(categoría);
        }

        // POST: Categorías/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoría = await _context.Categorías.FindAsync(id);
            _context.Categorías.Remove(categoría);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoríaExists(int id)
        {
            return _context.Categorías.Any(e => e.Id == id);
        }
    }
}
