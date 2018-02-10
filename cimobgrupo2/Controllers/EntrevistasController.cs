using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cimobgrupo2.Models;
using cimobgrupo2.Data;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace cimobgrupo2.Controllers
{
    /// <summary>Controlador para entrevistas</summary>
    /// <remarks>Extende de BaseController</remarks>
    public class EntrevistasController : BaseController
    {
        /// <summary>Atributo para a lista de entrevistas</summary>
        private List<Entrevista> _entrevistas;

        /// <summary>Construtor com parametros - EntrevistasController</summary>
        /// <param name="context">Context da Bd</param>
        /// <param name="fileProvider">File Provider</param>
        public EntrevistasController(ApplicationDbContext context, IFileProvider fileProvider) : base(context, fileProvider, "Entrevistas")
        {
            _entrevistas = context.Entrevistas.Include(e => e.Candidatura).ThenInclude(c => c.User).ToList();
        }


        /// <summary>Action que prepara e mostra o index</summary>
        /// <returns>Retorna a view</returns>
        public IActionResult Index()
        {
            List<Candidatura> listaCandidaturas = _context.Candidaturas.Include(c => c.User).ToList();
            ViewBag.ListOfUsers = listaCandidaturas;
            SetHelpModal("Index");
            return View(ProperView("Index"), _context.Entrevistas.ToList());
        }

        /// <summary>Action responsável por exibir o modal de confirmação de desmarcar entrevista</summary>
        /// <param name="id">Id da entrevista a desmarcar</param>
        /// <returns>Partialview com o devido modal</returns>
        public IActionResult DesmarcarEntrevistaModal(int? Id)
        {
            return PartialView(ProperView("DesmarcarEntrevistaModal"), _context.Entrevistas.SingleOrDefault(e => e.EntrevistaId == Id));
        }

        /// <summary>Action responsável pelo processo de desmarcar uma entrevista</summary>
        /// <param name="EntrevistaId">Id da entrevista a desmarcar</param>
        /// <returns>Redirecciona para a action index</returns>
        public IActionResult DesmarcarEntrevista(int EntrevistaId)
        {
            _context.Entrevistas.Remove(_context.Entrevistas.SingleOrDefault(e => e.EntrevistaId == EntrevistaId));
            _context.SaveChanges();
            SetSuccessMessage("Entrevista desmarcada.");
            return RedirectToAction(nameof(Index));
        }

        /// <summary>Action responsável por exibir o modal de avaliação de entrevista</summary>
        /// <param name="id">Id da entrevista a avaliar</param>
        /// <returns>Partialview com o devido modal</returns>
        public IActionResult AvaliarEntrevistaModal(int? Id)
        {
            return PartialView(ProperView("AvaliarEntrevistaModal"), _context.Entrevistas.SingleOrDefault(e => e.EntrevistaId == Id));
        }

        /// <summary>Action responsável pelo processo de avaliaçãode uma entrevista</summary>
        /// <param name="EntrevistaId">Id da entrevista a avaliar</param>
        /// <param name="Pontuacao">Pontuação a atribuir à entrevista</param>
        /// <param name="Comentarios">Comentarios a adicionar à avaliaçao da entrevista</param>
        /// <returns>Redirecciona para a action index</returns>
        public IActionResult AvaliarEntrevista(int EntrevistaId, int Pontuacao, string Comentarios)
        {
            Entrevista Entrevista = _context.Entrevistas.SingleOrDefault(e => e.EntrevistaId == EntrevistaId);
            if (Pontuacao >= 0 && Pontuacao <= 10)
            {
                Entrevista.Estado = EstadoEntrevista.Realizada;
                Entrevista.Pontuacao = Pontuacao;
                Entrevista.Comentarios = Comentarios;
                SetSuccessMessage("Entrevista avaliada.");
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            SetErrorMessage("003");
            return RedirectToAction(nameof(Index));
        }

        /// <summary>Action responsável por exibir o modal de edição de entrevista</summary>
        /// <param name="id">Id da entrevista a editar</param>
        /// <returns>Partialview com o devido modal</returns>
        public IActionResult EditarEntrevistaModal(int? Id)
        {
            return PartialView(ProperView("EditarEntrevistaModal"), _context.Entrevistas.SingleOrDefault(e => e.EntrevistaId == Id));
        }

        /// <summary>Action responsável pelo processo de edição de uma entrevista</summary>
        /// <param name="EntrevistaId">Id da entrevista a avaliar</param>
        /// <param name="Data">Nova Data</param>
        /// <param name="Hora">Nova Hora</param>
        /// <param name="Local">Novo Local</param>
        /// <param name="Pontuacao">Nova Pontuação</param>
        /// <param name="Comentarios">Novos Comentários</param>
        /// <returns>Redirecciona para a action index</returns>
        public IActionResult EditarEntrevista(int EntrevistaId, string Data, string Hora, string Local, int Pontuacao, string Comentarios)
        {
            Entrevista Entrevista = _context.Entrevistas.SingleOrDefault(e => e.EntrevistaId == EntrevistaId);

            bool sucesso = false;
            if (Entrevista.Estado == EstadoEntrevista.Pendente)
            {
                if (Data != null && Hora != null && Local != null)
                {
                    Entrevista.Data = Data;
                    Entrevista.Hora = Hora;
                    Entrevista.Local = Local;
                    sucesso = true;
                }
            }
            else
            {
                if (Pontuacao >= 0 && Pontuacao <= 10)
                {
                    Entrevista.Pontuacao = Pontuacao;
                    Entrevista.Comentarios = Comentarios;
                    sucesso = true;
                }
            }

            if (sucesso)
            {
                SetSuccessMessage("Entrevista editada.");
                _context.SaveChanges();
            }
            else
            {
                SetErrorMessage("003");
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>Action responsável pela marcação de uma nova entrevista</summary>
        /// <param name="Entrevista">Bind dos campos preenchidos no formulário para um objeto Entrevista</param>
        /// <param name="CandidaturaId">Id da candidatura à qual se pretende associar</param>
        /// <returns>Redirecciona para a action index</returns>
        public IActionResult MarcarEntrevista(int CandidaturaId, [Bind("Data", "Hora", "Local")] Entrevista Entrevista)
        {
            if (ModelState.IsValid)
            {
                Entrevista.CandidaturaId = CandidaturaId;
                Entrevista.Estado = EstadoEntrevista.Pendente;
                _context.Add(Entrevista);
                _context.SaveChanges();
                SetSuccessMessage("Entrevista marcada.");
            }
            else
            {
                SetErrorMessage("003");
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>Método que coloca a informação nas tooltips dos campos relacionados com entrevistas</summary>
        private void SetHelpTooltips()
        {
            ViewData["Candidato"] = _ajudas.Single(ai => ai.Action == "*" && ai.Elemento == "Candidato").Texto;
            ViewData["Data"] = _ajudas.Single(ai => ai.Action == "*" && ai.Elemento == "Data").Texto;
            ViewData["Hora"] = _ajudas.Single(ai => ai.Action == "*" && ai.Elemento == "Hora").Texto;
            ViewData["Local"] = _ajudas.Single(ai => ai.Action == "*" && ai.Elemento == "Local").Texto;
            ViewData["Pontuacao"] = _ajudas.Single(ai => ai.Action == "*" && ai.Elemento == "Pontuacao").Texto;
            ViewData["Comentarios"] = _ajudas.Single(ai => ai.Action == "*" && ai.Elemento == "Comentarios").Texto;
        }
    }
}