using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FacturacionIso.Data;
using FacturacionIso.Models;
using ServiceReference1;

namespace FacturacionIso.Controllers
{
    public class AsientosContablesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly AsientoContableServiceSoapClient _serviceClient;

        public AsientosContablesController(AppDbContext context, AsientoContableServiceSoapClient serviceClient)
        {
            _context = context;
            _serviceClient = serviceClient;
        }

        // POST: AsientosContables/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAsiento,Descripcion,IdCliente,Cuenta,TipoMovimiento,FechaAsiento,MontoAsiento,Estado,cuentaDB,cuentaCR")] AsientosContables asientosContables)
        {
            int resultado = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    // Invoca el servicio SOAP
                    var response = await _serviceClient.RegistrarAsientoAsync(
                        idAuxiliar: 3, // Enviando el valor fijo
                        descripcion: asientosContables.Descripcion,
                        cuentaDB: asientosContables.CuentaDB,
                        cuentaCR: asientosContables.CuentaCR,
                        monto: (decimal)asientosContables.MontoAsiento);

                    resultado = response.Body.RegistrarAsientoResult;

                    // Aquí estás pasando el mensaje como un mensaje de éxito
                    TempData["SuccessMessage"] = $"Resultado del servicio SOAP: {resultado}";

                    // Escribe el resultado en la consola para depuración
                    //Console.WriteLine($"Resultado del servicio SOAP: {response.Body.RegistrarAsientoResult}");


                    // Verifica la respuesta del servicio
                    if (response.Body.RegistrarAsientoResult > 0)
                    {
                        _context.Add(asientosContables);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        Console.WriteLine("Error: El servicio devolvió un resultado negativo.");
                        ModelState.AddModelError("", "Error al registrar el asiento contable en el sistema externo.");
                    }
                }
                catch (Exception ex)
                {
                    // Captura y muestra el error exacto
                    Console.WriteLine($"Excepción al intentar registrar en el servicio externo: {ex.Message}");
                    ModelState.AddModelError("", "Ocurrió un error al conectar con el servicio externo. Revisa los detalles.");
                }
            }
            return View(asientosContables);

        }


        // GET: AsientosContables
        public async Task<IActionResult> Index()
        {
            return View(await _context.AsientosContables.ToListAsync());
        }

        // GET: AsientosContables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asientosContables = await _context.AsientosContables
                .FirstOrDefaultAsync(m => m.IdAsiento == id);
            if (asientosContables == null)
            {
                return NotFound();
            }

            return View(asientosContables);
        }

        // GET: AsientosContables/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: AsientosContables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asientosContables = await _context.AsientosContables.FindAsync(id);
            if (asientosContables == null)
            {
                return NotFound();
            }
            return View(asientosContables);
        }

        // POST: AsientosContables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAsiento,Descripcion,IdCliente,Cuenta,TipoMovimiento,FechaAsiento,MontoAsiento,Estado,CuentaDB,CuentaCR")] AsientosContables asientosContables)
        {
            if (id != asientosContables.IdAsiento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asientosContables);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsientosContablesExists(asientosContables.IdAsiento))
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
            return View(asientosContables);
        }

        // GET: AsientosContables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asientosContables = await _context.AsientosContables
                .FirstOrDefaultAsync(m => m.IdAsiento == id);
            if (asientosContables == null)
            {
                return NotFound();
            }

            return View(asientosContables);
        }

        // POST: AsientosContables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asientosContables = await _context.AsientosContables.FindAsync(id);
            if (asientosContables != null)
            {
                _context.AsientosContables.Remove(asientosContables);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsientosContablesExists(int id)
        {
            return _context.AsientosContables.Any(e => e.IdAsiento == id);
        }
    }
}