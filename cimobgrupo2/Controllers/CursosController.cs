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
    /// <summary>Controlador para cursos</summary>
    /// <remarks>Extende de BaseController</remarks>
    public class CursosController : BaseController
    {
        /// <summary>Atributo para a lista de cursos</summary>
        private List<Curso> _cursos;

        /// <summary>Construtor com parametros - CursosController</summary>
        /// <param name="context">Context da Bd</param>
        /// <param name="fileProvider">File Provider</param>
        public CursosController(ApplicationDbContext context, IFileProvider fileProvider) : base(context, fileProvider, "Cursos")
        {
            _cursos = context.Cursos.ToList();
        }

        /// <summary>Action que prepara e mostra o index</summary>
        /// <returns>Retorna a view</returns>
        public IActionResult Index()
        {
            SetHelpModal("Index");
            return View(ProperView("Index"), _context.Cursos.ToList());
        }

        /// <summary>Action responsável por preparar e mostrar a página de detalhes de um curso</summary>
        /// <param name="id">id do curso cujos detalhes se pretendem visualizar</param>
        /// <returns>Caso o curso seja válido, retorna a view de detalhes do mesmo. Caso contrário redirecciona para o index</returns>
        public IActionResult Detalhes(int? id)
        {
            Curso curso = _context.Cursos.ToList().Find(c => c.CursoId == id);
            if (curso != null)
            {
                return View(ProperView("Detalhes"), curso.CursoId);
            }
            return View(nameof(Index));
        }

        /// <summary>Action responsável por exibir o modal de criação de curso</summary>
        /// <returns>Partialview com o devido modal</returns>
        public IActionResult NovoCursoModal()
        {
            SetHelpTooltips();
            return PartialView(ProperView("NovoCursoModal"));
        }

        /// <summary>Action que trata da criação de um novo curso e da sua adição à bd</summary>
        /// <param name="Curso">Bind dos campos do formulário preenchidos pelo utilizador, num objeto Curso</param>
        /// <returns>Redireciona para a acção Index</returns>
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

        /// <summary>Action responsável por exibir o modal de edição de curso</summary>
        /// <param name="Id">Id do curso a editar</param>
        /// <returns>Partialview com o devido modal</returns>
        public IActionResult EditarCursoModal(int? Id)
        {
            SetHelpTooltips();
            return PartialView(ProperView("EditarCursoModal"), _context.Cursos.SingleOrDefault(c => c.CursoId == Id));
        }

        /// <summary>Action que trata da edição de um curso e da sua atualização na bd</summary>
        /// <param name="Curso">Bind dos campos do formulário preenchidos pelo utilizador, num objeto Curso</param>
        /// <returns>Redireciona para a acção Index</returns>
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

        /// <summary>Action responsável por exibir o modal de confirmação de remoção de curso</summary>
        /// <param name="Id">Id do curso a remover</param>
        /// <returns>Partialview com o devido modal</returns>
        public IActionResult RemoverCursoModal(int? Id)
        {
            return PartialView(ProperView("RemoverCursoModal"), _context.Cursos.SingleOrDefault(c => c.CursoId == Id));
        }

        /// <summary>Action que trata da remoção de um curso da bd</summary>
        /// <param name="CursoId">Id do curso a remover</param>
        /// <returns>Redireciona para a acção Index</returns>
        public IActionResult RemoverCurso(int CursoId)
        {
            _context.Cursos.Remove(_context.Cursos.SingleOrDefault(m => m.CursoId == CursoId));
            _context.SaveChanges();
            SetSuccessMessage("Curso removido.");
            return RedirectToAction(nameof(Index));
        }

        /// <summary>Método que coloca a informação nas tooltips dos campos relacionados com cursos</summary>
        private void SetHelpTooltips()
        {
            ViewData["Nome"] = _ajudas.Single(ai => ai.Action == "*" && ai.Elemento == "Nome").Texto;
        }
    }
}