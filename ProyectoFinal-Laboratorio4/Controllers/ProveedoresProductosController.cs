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
    public class ProveedoresProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProveedoresProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProveedoresProductos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProveedoresProductos.Include(p => p.Proveedor).Include(p => p.Producto);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProveedoresProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedorProducto = await _context.ProveedoresProductos
                .Include(p => p.Producto)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (proveedorProducto == null)
            {
                return NotFound();
            }

            return View(proveedorProducto);
        }

        [Authorize]
        // GET: ProveedoresProductos/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre");
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre");
            return View();
        }

        // POST: ProveedoresProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProveedorId,ProductoId")] ProveedorProducto proveedorProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedorProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", proveedorProducto.ProductoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre", proveedorProducto.ProveedorId);
            return View(proveedorProducto);
        }

        [Authorize]
        // GET: ProveedoresProductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedorProducto = await _context.ProveedoresProductos.FirstOrDefaultAsync(m => m.Id == id);

            if (proveedorProducto == null)
            {
                return NotFound();
            }

            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", proveedorProducto.ProductoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre", proveedorProducto.ProveedorId);
            return View(proveedorProducto);
        }

        // POST: ProveedoresProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,ProveedorId,ProductoId")] ProveedorProducto proveedorProducto)
        {
            if (id != proveedorProducto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedorProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorProductoExists(proveedorProducto.Id))
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
            
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", proveedorProducto.ProductoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre", proveedorProducto.ProveedorId);
            return View(proveedorProducto);
        }

        [Authorize]
        // GET: ProveedoresProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedorProducto = await _context.ProveedoresProductos
                .Include(p => p.Producto)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedorProducto == null)
            {
                return NotFound();
            }

            return View(proveedorProducto);
        }

        // POST: ProveedoresProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var proveedorProducto = await _context.ProveedoresProductos.FirstOrDefaultAsync(m => m.Id == id);
            _context.ProveedoresProductos.Remove(proveedorProducto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedorProductoExists(int id)
        {
            return _context.ProveedoresProductos.Any(e => e.Id == id);
        }
    }
}
