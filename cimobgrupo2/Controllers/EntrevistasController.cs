using cimobgrupo2.Data;
using cimobgrupo2.Models;
using cimobgrupo2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Controllers
{
    public class EntrevistasController : BaseController
    {
        private readonly List<Entrevista>  _entrevistas;
        private readonly IEmailSender _emailSender;
        List<string> listFromUsers = new List<string>();
        List<string> listaProgramas = new List<string>();
        List<string> listaEstados = new List<string>();

        public EntrevistasController(ApplicationDbContext _context, IEmailSender emailSender,
            IFileProvider fileProvider):base(_context, fileProvider, "Entrevistas"){
            _entrevistas= _context.Entrevistas.Include(e=> e.Candidatura).Include(e=> e.Estado).Include(e=>e.Candidatura.User)
                .Include(e=>e.Candidatura.Programa).ToList();
            _emailSender = emailSender;
        }

        // GET: Entrevistas
        public async Task<IActionResult> Index()
        {
          
                var applicationDbContext = _context.Entrevistas.Include(e => e.Candidatura.User).Include(e => e.Candidatura.Programa)
                    .Include(e => e.Candidatura.Curso).Include(e => e.Candidatura.Entrevistas)
                    .Include(e => e.Estado)
                    .Include(e => e.Candidatura.Curso.EscolasParceiras)
                    .ToList();
                return View(ProperView("Index"), _entrevistas);
        }

        // GET: Entrevistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entrevista = await _context.Entrevistas
                .Include(e => e.Candidatura).Include(e => e.Candidatura.User).Include(e => e.Candidatura.Programa)
                .Include(e => e.Candidatura.Curso).Include(e => e.Candidatura.Curso.EscolasParceiras)
                .Include(e => e.Estado)
                .SingleOrDefaultAsync(m => m.EntrevistaId == id);
            if (entrevista == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Estudante"))
            {
                ViewBag.AgendarEntrevistas = _context.Entrevistas.Include(e => e.Candidatura.User).Include(e => e.Candidatura.Programa)
                        .Include(e => e.Candidatura.Curso).Include(e => e.Candidatura.Entrevistas).
                        Include(e => e.Candidatura.Curso.EscolasParceiras)
                    .ThenInclude(e => e.EscolaParceira.Programas).ToList();
                return View(ProperView("Details"), entrevista);
            }
            return RedirectToAction(nameof(Index));
                }

        // GET: Entrevistas/MarcarEntrevista
        public IActionResult MarcarEntrevista()
        {
            //listas de estados 
            listaEstados = (from estados in _context.Estados select estados.Nome).ToList();
            listaEstados.Sort();
            ViewBag.ListaEstados = listaEstados;
            //Entrevista entrevistaAux = _entrevistas.SingleOrDefault(e=>e);
            ViewData["CandidaturaId"] = new SelectList(_context.Candidaturas, "CandidaturaId", "CandidaturaId");
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadoId", "EstadoId");
            //ViewBag dos users 
            
            listFromUsers = (from users in _context.Users select users.Nome).ToList();
            listFromUsers.Sort();
            ViewBag.ListOfUsers = listFromUsers;
            // ViewBag dos programas
           
            listaProgramas = (from programas in _context.Programas select programas.Nome).ToList();
            listaProgramas.Sort();
            ViewBag.ListaProgramas = listaProgramas;
            return View(ProperView("MarcarEntrevista"));
        }

        // POST: Entrevistas/MarcarEntrevista
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarcarEntrevista([Bind("EntrevistaId,UserId, ProgramaId,DataEntrevista,Estado,CandidaturaId")] Entrevista entrevista)
        {
            //listas de estados 
            var aux = _entrevistas.Find(e=>e.EntrevistaId==entrevista.EntrevistaId);
            if (entrevista == null)
            {
               return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Add(entrevista);
               await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entrevista);
        }

        // GET: Entrevistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Entrevista entrevista = _context.Entrevistas.SingleOrDefault(e=>e.EntrevistaId== id);
            listaEstados = (from estados in _context.Estados select estados.Nome).ToList();
            listaEstados.Sort();
            ViewBag.ListaEstados = listaEstados;
            return View(ProperView("Edit"), entrevista);

        }

        // POST: Entrevistas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("EntrevistaId,DataEntrevista, CandidaturaId,Estado")] Entrevista entrevista)
        {
            if (ModelState.IsValid)
            {
                Entrevista novaEntrevista = _context.Entrevistas.SingleOrDefault(e => e.EntrevistaId == entrevista.EntrevistaId);
                string nomeUser= _context.Candidaturas.Where(c => c.CandidaturaId == entrevista.CandidaturaId).SingleOrDefault()?.User.Nome;
                string nomeProgram= _context.Candidaturas.Where(p => p.CandidaturaId == entrevista.CandidaturaId).SingleOrDefault()?.Programa.Nome;
                novaEntrevista.Candidatura.User.Nome = nomeUser;
                novaEntrevista.Candidatura.Programa.Nome = nomeUser;
                novaEntrevista.DataEntrevista = entrevista.DataEntrevista;
                novaEntrevista.Estado = entrevista.Estado;
                _context.SaveChanges();
                SetSuccessMessage("Entrevista editada");
                return RedirectToAction(nameof(Index));
            }
            SetErrorMessage("003");
            return View(entrevista);

        }



        // GET: Entrevistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrevista = await _context.Entrevistas
                .Include(e => e.Candidatura)
                .SingleOrDefaultAsync(m => m.EntrevistaId == id);
            if (entrevista == null)
            {
                return NotFound();
            }

            return View(entrevista);
        }

        // POST: Entrevistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entrevista = await _context.Entrevistas.SingleOrDefaultAsync(m => m.EntrevistaId == id);
            _context.Entrevistas.Remove(entrevista);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntrevistaExists(int id)
        {
            return _context.Entrevistas.Any(e => e.EntrevistaId == id);
        }

        public IActionResult Adiar(int EntrevistaId)
        {
            Entrevista entrevista = _entrevistas.SingleOrDefault(p => p.EntrevistaId == EntrevistaId);
            if (entrevista != null)
            {
                entrevista.Estado = _context.Estados.SingleOrDefault(e => e.Nome == "Recusada");
                _context.SaveChanges();
                SetSuccessMessage("Entrevista adiada");
                return RedirectToAction(nameof(Details), new { id = EntrevistaId });
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DetalhesEstudante( int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entrevista = await _context.Entrevistas
                .Include(e => e.Candidatura.User).Include(e => e.Candidatura.Programa)
                .Include(e => e.Candidatura.Curso.EscolasParceiras)
                .Include(e => e.Estado)
                .SingleOrDefaultAsync(m => m.EntrevistaId ==id);
            if (entrevista == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Estudante"))
            {
                ViewBag.ConfirEntrevista = _context.Entrevistas.Include(e => e.Candidatura.User).Include(e => e.Candidatura.Programa)

                        .Include(e => e.Candidatura.Curso).Include(e => e.Candidatura.Entrevistas).Include(e => e.Estado)
                        .Include(e => e.Candidatura.Curso.EscolasParceiras)
                    .ThenInclude(e => e.EscolaParceira.Programas).ToList();
                return View(ProperView("DetalhesEstudante"), entrevista);
            }
            return RedirectToAction(nameof(Index));

        }
        }
    }

