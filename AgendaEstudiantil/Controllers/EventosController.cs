
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgendaEstudiantil.Data;
using AgendaEstudiantil.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AgendaEstudiantil.Controllers
{
    [Authorize] 
    public class EventosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Eventos
        public async Task<IActionResult> Index(string? busqueda, int? mes)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var eventosUsuario = _context.Eventos
                                         .Where(e => e.UserId == userId);

            if (!string.IsNullOrEmpty(busqueda))
            {
                eventosUsuario = eventosUsuario
                                .Where(e => e.Titulo.Contains(busqueda));
            }

            if (mes.HasValue)
            {
                eventosUsuario = eventosUsuario.Where(e => e.Fecha.Month == mes.Value);
            }

            var listaOrdenada = await eventosUsuario
                                     .OrderBy(e => e.Fecha)
                                     .ToListAsync();

            if (!listaOrdenada.Any())
            {
                ViewBag.Mensaje = string.IsNullOrEmpty(busqueda) ? "No hay eventos disponibles" : "No se encontraron eventos";
            }

            return View(listaOrdenada);

        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(evento.Descripcion))
            {
                TempData["Mensaje"] = "Este evento no tiene descripción y no se puede mostrar.";
                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Fecha,Descripcion")] Evento evento)
        {
            if (ModelState.IsValid)
            {

                // Obtener el id del usuario autenticado
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                evento.UserId = userId;

                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var evento = await _context.Eventos
                .Where(e => e.Id == id && e.UserId == userId)
                .FirstOrDefaultAsync();

            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Fecha,Descripcion")] Evento evento)
        {
            if (id != evento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Buscar el evento original y asegurarse que pertenezca al usuario
                var eventoOriginal = await _context.Eventos
                    .Where(e => e.Id == id && e.UserId == userId)
                    .FirstOrDefaultAsync();

                if (eventoOriginal == null)
                {
                    return NotFound();
                }

                // Actualizar sólo los campos permitidos
                eventoOriginal.Titulo = evento.Titulo;
                eventoOriginal.Fecha = evento.Fecha;
                eventoOriginal.Descripcion = evento.Descripcion;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.Id))
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
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var evento = await _context.Eventos
                .Where(e => e.Id == id && e.UserId == userId)
                .FirstOrDefaultAsync();

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var evento = await _context.Eventos
                .Where(e => e.Id == id && e.UserId == userId)
                .FirstOrDefaultAsync();

            if (evento == null)
            {
                return NotFound();
            }

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Completar(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var evento = await _context.Eventos
                                       .Where(e => e.Id == id && e.UserId == userId)
                                       .FirstOrDefaultAsync();

            if (evento == null || evento.Fecha < DateTime.Today)
            {
                return NotFound();
            }

            evento.Completado = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.Id == id);
        }
    }
}
