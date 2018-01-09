using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cimobgrupo2.Models;
using cimobgrupo2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace cimobgrupo2.Controllers
{
    public class CursosController : BaseController
    {
        private List<Curso> _cursos;

        public CursosController(ApplicationDbContext context, IFileProvider fileProvider) : base(context, fileProvider, "Cursos")
        {
            _cursos = context.Cursos.ToList();
        }
       

        public IActionResult Index()
        {
            return View(ProperView("Index"), _context.Cursos.ToList());
        }

        public IActionResult Detalhes(int? id)
        {
            return View(ProperView("Detalhes"), _context.Cursos.ToList().Find(c => c.CursoId == id));
        }

        public IActionResult NovoCursoModal()
        {
            return PartialView(ProperView("NovoCursoModal"));
        }

        public IActionResult NovoCurso([Bind("Nome")] Curso Curso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Curso);
                _context.SaveChanges();
                SetSuccessMessage("Curso adicionado.");
            } else
            {
                SetErrorMessage("003");
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarCursoModal(int? Id)
        {
            return PartialView(ProperView("EditarCursoModal"), _context.Cursos.SingleOrDefault(c => c.CursoId == Id));
        }

        public IActionResult EditarCurso([Bind("CursoId,Nome")] Curso Curso)
        {
            if (ModelState.IsValid)
            {
                _context.Cursos.SingleOrDefault(c => c.CursoId == Curso.CursoId).Nome = Curso.Nome;
                _context.SaveChanges();
                SetSuccessMessage("Curso editado.");
            } else
            {
                SetErrorMessage("003");
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoverCursoModal(int? Id)
        {
            return PartialView(ProperView("RemoverCursoModal"), _context.Cursos.SingleOrDefault(c => c.CursoId == Id));
        }
    
        public IActionResult RemoverCurso(int CursoId)
        {
            _context.Cursos.Remove(_context.Cursos.SingleOrDefault(m => m.CursoId == CursoId));
            _context.SaveChanges();
            SetSuccessMessage("Curso removido.");
            return RedirectToAction(nameof(Index));
        }
    }
}