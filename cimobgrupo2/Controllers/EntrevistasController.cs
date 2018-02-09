using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cimobgrupo2.Data;
using Microsoft.Extensions.FileProviders;
using cimobgrupo2.Models;
using Microsoft.EntityFrameworkCore;

namespace cimobgrupo2.Controllers
{
    public class EntrevistasController : BaseController
    {
        private List<Entrevista> _entrevistas;

        public EntrevistasController(ApplicationDbContext context, IFileProvider fileProvider) : base(context, fileProvider, "Entrevistas")
        {
            _entrevistas = context.Entrevistas.Include(e => e.User).ToList();
        }

        public IActionResult Index()
        {
            return View(ProperView("Index"), _entrevistas);
        }

        public IActionResult EditarEntrevistaModal(int? Id)
        {
            return PartialView(ProperView("EditarEntrevistaModal"), _context.Entrevistas.SingleOrDefault(e => e.EntrevistaId == Id));
        }

        [HttpPost]
        public IActionResult EditarEntrevista([Bind("EntrevistaId, Local, DataEntrevista, Hora")] Entrevista Entrevista)
        {
            if (ModelState.IsValid)
            {
                Entrevista actual = _context.Entrevistas.SingleOrDefault(e => e.EntrevistaId == Entrevista.EntrevistaId);
                actual.DataEntrevista = Entrevista.DataEntrevista;
                actual.Local = Entrevista.Local;
                actual.Hora = Entrevista.Hora;
                _context.SaveChanges();
                SetSuccessMessage("Entrevista editada."); 
            }
            else
            {
                SetErrorMessage("003");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}