using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_Laboratorio4.Data;
using ProyectoFinal_Laboratorio4.Models;

namespace ProyectoFinal_Laboratorio4.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductosController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Productos
        public IActionResult Index(string? filtrarNombre, int? filtrarMarca, int? filtrarCategoría)
        {
            var applicationDbContext = _context.Productos.Include(p => p.CategoríaProducto).Include(p => p.MarcaProducto).Select(p => p);

            if (!string.IsNullOrEmpty(filtrarNombre))
            {
                applicationDbContext = applicationDbContext.Where(x => x.Nombre.Contains(filtrarNombre));
            }
            if (filtrarMarca.HasValue)
            {
                applicationDbContext = applicationDbContext.Where(x => x.MarcaId == filtrarMarca.Value);
            }
            if (filtrarCategoría.HasValue)
            {
                applicationDbContext = applicationDbContext.Where(x => x.CategoríaId == filtrarCategoría.Value);
            }

            ProductoView model = new ProductoView()
            {
                Producto = applicationDbContext.ToList(),
                Nombre = filtrarNombre,
                Categoría = new SelectList(_context.Categorías, "Id", "Descripción", filtrarCategoría),
                Marca = new SelectList(_context.Marcas, "Id", "Nombre", filtrarMarca),
                MarcaId = filtrarMarca,
                CategoríaId = filtrarCategoría
            };

            return View(model);
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.CategoríaProducto)
                .Include(p => p.MarcaProducto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [Authorize]
        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["CategoríaId"] = new SelectList(_context.Categorías, "Id", "Descripción");
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio,Descripción,Imagen,Favorito,CategoríaId,MarcaId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                agregarImagen(producto);
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoríaId"] = new SelectList(_context.Categorías, "Id", "Descripción", producto.CategoríaId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre", producto.MarcaId);
            return View(producto);
        }

        [Authorize]
        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CategoríaId"] = new SelectList(_context.Categorías, "Id", "Descripción", producto.CategoríaId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre", producto.MarcaId);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Precio,Descripción,Imagen,Favorito,CategoríaId,MarcaId")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    agregarImagen(producto);
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            ViewData["CategoríaId"] = new SelectList(_context.Categorías, "Id", "Descripción", producto.CategoríaId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre", producto.MarcaId);
            return View(producto);
        }

        [Authorize]
        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.CategoríaProducto)
                .Include(p => p.MarcaProducto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }

        private void agregarImagen(Producto producto)
        {
            var files = HttpContext.Request.Form.Files; 
            if (files != null && files.Count > 0)
            {
                var archivoImagen = files[0];

                if (archivoImagen.Length > 0)
                {
                    var pathDestino = Path.Combine(_env.WebRootPath, "images\\productos"); 

                    var archivoDestino = Guid.NewGuid().ToString().Replace("-", ""); 

                    archivoDestino += Path.GetExtension(archivoImagen.FileName); 
                                                                                
                    var rutaDestino = Path.Combine(pathDestino, archivoDestino);

                    using (var filestream = new FileStream(rutaDestino, FileMode.Create)) 
                    {
                        archivoImagen.CopyTo(filestream);

                        producto.Imagen = archivoDestino;
                    }
                }
            }
        }
    }
}
