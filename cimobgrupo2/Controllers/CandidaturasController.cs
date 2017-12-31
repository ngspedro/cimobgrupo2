using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cimobgrupo2.Data;
using cimobgrupo2.Models;

namespace cimobgrupo2.Controllers
{
    public class CandidaturasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CandidaturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Candidaturas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Candidatura.Include(c => c.Curso).Include(c => c.EscolaParceira).Include(c => c.Estado).Include(c => c.Programa);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Candidaturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidatura = await _context.Candidatura
                .Include(c => c.Curso)
                .Include(c => c.EscolaParceira)
                .Include(c => c.Estado)
                .Include(c => c.Programa)
                .SingleOrDefaultAsync(m => m.CandidaturaId == id);
            if (candidatura == null)
            {
                return NotFound();
            }

            return View(candidatura);
        }

        // GET: Candidaturas/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId");
            ViewData["EscolaParceiraId"] = new SelectList(_context.EscolasParceiras, "EscolaParceiraId", "EscolaParceiraId");
            ViewData["EstadoId"] = new SelectList(_context.Set<Estado>(), "EstadoId", "EstadoId");
            ViewData["ProgramaId"] = new SelectList(_context.Programas, "ProgramaId", "ProgramaId");
            return View();
        }

        // POST: Candidaturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CandidaturaId,ProgramaId,EscolaParceiraId,CursoId,EstadoId")] Candidatura candidatura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidatura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId", candidatura.CursoId);
            ViewData["EscolaParceiraId"] = new SelectList(_context.EscolasParceiras, "EscolaParceiraId", "EscolaParceiraId", candidatura.EscolaParceiraId);
            ViewData["EstadoId"] = new SelectList(_context.Set<Estado>(), "EstadoId", "EstadoId", candidatura.EstadoId);
            ViewData["ProgramaId"] = new SelectList(_context.Programas, "ProgramaId", "ProgramaId", candidatura.ProgramaId);
            return View(candidatura);
        }

        // GET: Candidaturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidatura = await _context.Candidatura.SingleOrDefaultAsync(m => m.CandidaturaId == id);
            if (candidatura == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId", candidatura.CursoId);
            ViewData["EscolaParceiraId"] = new SelectList(_context.EscolasParceiras, "EscolaParceiraId", "EscolaParceiraId", candidatura.EscolaParceiraId);
            ViewData["EstadoId"] = new SelectList(_context.Set<Estado>(), "EstadoId", "EstadoId", candidatura.EstadoId);
            ViewData["ProgramaId"] = new SelectList(_context.Programas, "ProgramaId", "ProgramaId", candidatura.ProgramaId);
            return View(candidatura);
        }

        // POST: Candidaturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CandidaturaId,ProgramaId,EscolaParceiraId,CursoId,EstadoId")] Candidatura candidatura)
        {
            if (id != candidatura.CandidaturaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidatura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidaturaExists(candidatura.CandidaturaId))
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
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId", candidatura.CursoId);
            ViewData["EscolaParceiraId"] = new SelectList(_context.EscolasParceiras, "EscolaParceiraId", "EscolaParceiraId", candidatura.EscolaParceiraId);
            ViewData["EstadoId"] = new SelectList(_context.Set<Estado>(), "EstadoId", "EstadoId", candidatura.EstadoId);
            ViewData["ProgramaId"] = new SelectList(_context.Programas, "ProgramaId", "ProgramaId", candidatura.ProgramaId);
            return View(candidatura);
        }

        // GET: Candidaturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidatura = await _context.Candidatura
                .Include(c => c.Curso)
                .Include(c => c.EscolaParceira)
                .Include(c => c.Estado)
                .Include(c => c.Programa)
                .SingleOrDefaultAsync(m => m.CandidaturaId == id);
            if (candidatura == null)
            {
                return NotFound();
            }

            return View(candidatura);
        }

        // POST: Candidaturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidatura = await _context.Candidatura.SingleOrDefaultAsync(m => m.CandidaturaId == id);
            _context.Candidatura.Remove(candidatura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidaturaExists(int id)
        {
            return _context.Candidatura.Any(e => e.CandidaturaId == id);
        }
    }
}
