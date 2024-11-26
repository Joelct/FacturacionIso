using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FacturacionIso.Data;
using FacturacionIso.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FacturacionIso.Controllers
{
    public class FacturacionController : Controller
    {
        private readonly AppDbContext _context;

        public FacturacionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Facturacion
        public async Task<IActionResult> Index()
        {
            return View(await _context.Facturacion.ToListAsync());
        }

        // GET: Facturacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturacion = await _context.Facturacion
                .FirstOrDefaultAsync(m => m.IdFacturacion == id);
            if (facturacion == null)
            {
                return NotFound();
            }

            return View(facturacion);
        }

        // GET: Facturacion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facturacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFacturacion,IdVendedor,IdCliente,IdArticulo,Fecha,Comentario,Cantidad,PrecioUnitario")] Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facturacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facturacion);
        }

        // GET: Facturacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturacion = await _context.Facturacion.FindAsync(id);
            if (facturacion == null)
            {
                return NotFound();
            }
            return View(facturacion);
        }

        // POST: Facturacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFacturacion,IdVendedor,IdCliente,IdArticulo,Fecha,Comentario,Cantidad,PrecioUnitario")] Facturacion facturacion)
        {
            if (id != facturacion.IdFacturacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturacionExists(facturacion.IdFacturacion))
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
            return View(facturacion);
        }

        // GET: Facturacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturacion = await _context.Facturacion
                .FirstOrDefaultAsync(m => m.IdFacturacion == id);
            if (facturacion == null)
            {
                return NotFound();
            }

            return View(facturacion);
        }

        // POST: Facturacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facturacion = await _context.Facturacion.FindAsync(id);
            if (facturacion != null)
            {
                _context.Facturacion.Remove(facturacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturacionExists(int id)
        {
            return _context.Facturacion.Any(e => e.IdFacturacion == id);
        }
    }
}
