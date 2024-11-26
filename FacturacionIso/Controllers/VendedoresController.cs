using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FacturacionIso.Data;
using FacturacionIso.Models;

namespace FacturacionIso.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly AppDbContext _context;

        public VendedoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Vendedores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vendedores.ToListAsync());
        }

        // GET: Vendedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedores = await _context.Vendedores
                .FirstOrDefaultAsync(m => m.IdVendedor == id);
            if (vendedores == null)
            {
                return NotFound();
            }

            return View(vendedores);
        }

        // GET: Vendedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVendedor,Nombre,PorcientoComision,Estado")] Vendedores vendedores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendedores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendedores);
        }

        // GET: Vendedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedores = await _context.Vendedores.FindAsync(id);
            if (vendedores == null)
            {
                return NotFound();
            }
            return View(vendedores);
        }

        // POST: Vendedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVendedor,Nombre,PorcientoComision,Estado")] Vendedores vendedores)
        {
            if (id != vendedores.IdVendedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendedores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendedoresExists(vendedores.IdVendedor))
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
            return View(vendedores);
        }

        // GET: Vendedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedores = await _context.Vendedores
                .FirstOrDefaultAsync(m => m.IdVendedor == id);
            if (vendedores == null)
            {
                return NotFound();
            }

            return View(vendedores);
        }

        // POST: Vendedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendedores = await _context.Vendedores.FindAsync(id);
            if (vendedores != null)
            {
                _context.Vendedores.Remove(vendedores);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendedoresExists(int id)
        {
            return _context.Vendedores.Any(e => e.IdVendedor == id);
        }
    }
}
