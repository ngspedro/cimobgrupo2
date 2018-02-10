using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using cimobgrupo2.Models;
using cimobgrupo2.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using cimobgrupo2.Services;

namespace cimobgrupo2.Controllers
{
    /// <summary>Controlador para candidaturas</summary>
    /// <remarks>Extende de BaseController</remarks>
    public class CandidaturasController : BaseController
    {
        /// <summary>Atributo para o caminho base de gravação de ficheiros carregados pelos utilizadores associados a candidaturas</summary>
        private string BASE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "candidaturas");

        /// <summary>Atributo para lista de candidaturas</summary>
        private List<Candidatura> _candidaturas;

        /// <summary>Atributo para o Email Sender</summary>
        private readonly IEmailSender _emailSender;

        /// <summary>Construtor com parametros - CandidaturasController</summary>
        /// <param name="context">Context da Bd</param>
        /// <param name="emailSender">Email Sender</param>
        /// <param name="fileProvider">File Provider</param>
        public CandidaturasController(ApplicationDbContext context, IEmailSender emailSender, 
            IFileProvider fileProvider) : base(context, fileProvider, "Candidaturas")
        {
            //vai buscar a lista de candidaturass
            _candidaturas = context.Candidaturas.Include(c => c.Entrevistas).Include(c => c.Curso).Include(c => c.Programa).Include(c => c.EscolaParceira).Include(c => c.User).Include(c => c.Estado).ToList();
            _emailSender = emailSender;
        }

        /// <summary>Action que prepara e mostra o index, tendo em conta a role, se é candidato ou não, etc.</summary>
        /// <returns>Redireciona para a action / retorna a view adequada</returns>
        public IActionResult Index()
        {
            if (!User.IsInRole("Estudante"))
            {
                ViewBag.ProgramasPublicar = _context.Programas.Include(p => p.Candidaturas).Include(e => e.EscolasParceiras).ThenInclude(e => e.EscolaParceira)
                .ThenInclude(e => e.Cursos).ThenInclude(e => e.Curso).ToList();
                SetHelpModal("Index");
                return View(ProperView("Index"), _candidaturas.Where(c => c.Estado != _context.Estados.SingleOrDefault(e => e.Nome == "Em Criação")));
            }
            return RedirectToAction(nameof(Criar));
        }

        /// <summary>Action que prepara e mostra a view de criação de candidatura</summary>
        /// <returns>Redireciona para a action / retorna a view adequada</returns>
        public IActionResult Criar()
        {
            if (!User.IsInRole("Estudante"))
                return RedirectToAction(nameof(Index));
            

            Candidatura aux = _candidaturas.SingleOrDefault(c => c.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (aux == null)
            {
                _context.Add(new Candidatura()
                {
                    UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Programa = null,
                    EscolaParceira = null,
                    Curso = null,
                    Estado = _context.Estados.SingleOrDefault(e => e.Nome == "Em Criação")
                    
                });
                _context.SaveChanges();
                return RedirectToAction(nameof(Criar));
            }
            else if (aux != null && aux.Estado != _context.Estados.SingleOrDefault(e => e.Nome == "Em Criação"))
            {
                return RedirectToAction(ProperView("Detalhes"), new { id = aux.CandidaturaId });
            }

            Candidatura Candidatura = _candidaturas.SingleOrDefault(c => c.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            List<Programa> listaProgramas = _context.Programas.Include(p => p.EscolasParceiras).ThenInclude(p => p.EscolaParceira).ToList();
            List<EscolaParceira> listaEscolas = _context.EscolasParceiras.Include(e => e.Cursos).ThenInclude(e => e.Curso).ToList();
            List<Curso> listaCursos = _context.Cursos.ToList();

            if (Candidatura.ProgramaId != null)
                listaEscolas = listaEscolas.Where(e => e.Programas.Where(p => p.ProgramaId == Candidatura.ProgramaId).Count() != 0).ToList();

            if (Candidatura.EscolaParceiraId != null)
                listaCursos = listaCursos.Where(e => e.EscolasParceiras.Where(p => p.EscolaParceiraId == Candidatura.EscolaParceiraId).Count() != 0).ToList();

            ViewBag.Programas = listaProgramas;
            ViewBag.Escolas = listaEscolas;
            ViewBag.Cursos = listaCursos;

            var caminho = "candidaturas/" + this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Documentos = _fileController.GetFiles(caminho);

            SetHelpModal("Criar");
            return View(ProperView("Criar"), Candidatura);
        }

        /// <summary>Action que trata da finalização de uma candidatura por parte de um utilizador</summary>
        /// <param name="CandidaturaId">Id da candidatura a finalizar</param>
        /// <returns>Redirecciona para a ação de detalhes de candidatura, se finalizada com sucesso. Caso contrário fica na página de criação</returns>
        [HttpPost]
        public IActionResult Submeter(int CandidaturaId)
        {
            Candidatura Candidatura = _candidaturas.SingleOrDefault(c => c.CandidaturaId == CandidaturaId);
            if (Candidatura != null && Candidatura.Programa != null && Candidatura.EscolaParceira != null && Candidatura.Curso != null)
            {
                Candidatura.EstadoId = 1; //estado pendente
                _context.SaveChanges();
                SetSuccessMessage("Candidatura Submetida. Por favor aguarde pela avaliação da mesma.");
                return RedirectToAction(nameof(Detalhes), new { id = Candidatura.CandidaturaId });
            }
            
            SetErrorMessage("003");
            return RedirectToAction(nameof(Criar));
        }

        /// <summary>Action que escolhe um programa para uma determinada candidatura</summary>
        /// <param name="CandidaturaId">Id da candidatura à qual o programa será associado</param>
        /// <param name="ProgramaEscolhido">Id do programa a associar</param>
        /// <returns>Redirecciona para a ação Criar (mostra a view de criação, já com o programa associado)</returns>
        public IActionResult EscolherPrograma(int CandidaturaId, int ProgramaEscolhido)
        {
            Candidatura c = _candidaturas.SingleOrDefault(cand => cand.CandidaturaId == CandidaturaId);
            if (c != null && ProgramaEscolhido != 0 && ProgramaEscolhido != c.ProgramaId)
            {
                c.ProgramaId = ProgramaEscolhido;
                c.EscolaParceiraId = null;
                c.CursoId = null;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Criar));
        }

        /// <summary>Action que escolhe uma escola para uma determinada candidatura</summary>
        /// <param name="CandidaturaId">Id da candidatura à qual a escola será associada</param>
        /// <param name="EscolaEscolhida">Id da escola a associar</param>
        /// <returns>Redirecciona para a ação Criar (mostra a view de criação, já com a escola associada)</returns>
        public IActionResult EscolherEscola(int CandidaturaId, int EscolaEscolhida)
        {
            Candidatura c = _candidaturas.SingleOrDefault(cand => cand.CandidaturaId == CandidaturaId);
            if (c != null && EscolaEscolhida != 0 && EscolaEscolhida != c.EscolaParceiraId)
            {
                c.EscolaParceiraId = EscolaEscolhida;
                c.CursoId = null;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Criar));
        }

        /// <summary>Action que escolhe um curso para uma determinada candidatura</summary>
        /// <param name="CandidaturaId">Id da candidatura à qual o programa será associado</param>
        /// <param name="CursoEscolhido">Id do curso a associar</param>
        /// <returns>Redirecciona para a ação Criar (mostra a view de criação, já com o curso associado)</returns>
        public IActionResult EscolherCurso(int CandidaturaId, int CursoEscolhido)
        {
            if (CandidaturaId != 0 && CursoEscolhido != 0)
            {
                Candidatura c = _candidaturas.SingleOrDefault(cand => cand.CandidaturaId == CandidaturaId);
                c.CursoId = CursoEscolhido;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Criar));
        }

        /// <summary>Action responsável por preparar e mostrar a página de detalhes de uma candidatura</summary>
        /// <param name="id">id da candidatura cujos detalhes se pretendem visualizar</param>
        /// <returns>Caso a candidatura seja válida, retorna a view de detalhes da mesma. Caso contrário redirecciona para o index</returns>
        public IActionResult Detalhes(int? id)
        {
            Candidatura candidatura = _candidaturas.Find(p => p.CandidaturaId == id); 
            if (candidatura != null)
            {
                if (!User.IsInRole("Estudante") || candidatura.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    var caminho = "candidaturas/" + candidatura.UserId;
                    ViewBag.Documentos = _fileController.GetFiles(caminho);
                    SetHelpModal("Detalhes");
                    return View(ProperView("Detalhes"), candidatura);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>Action responsável por carregar e associar uma lista de ficheiros submetidos pelo utilizador à candidatura</summary>
        /// <param name="files">Lista de ficheiros a carregar e associar</param>
        /// <returns>Redireciona para a action Criar, já com os ficheiros carregados e associados</returns>
        [HttpPost]
        public async Task<IActionResult> AssociarDocumentos(List<IFormFile> files)
        {
            if (files.Count() > 0)
            {
                var caminho = Path.Combine(BASE_PATH, this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                foreach (IFormFile f in files)
                {
                    await _fileController.UploadFile(caminho, f);
                }
                //SetSuccessMessage(files.Count() + " documentos adicionados.");
            }

            return RedirectToAction(nameof(Criar));
        }

        /// <summary>Action responsável por remover e desassociar uma lista de ficheiros, de uma determinada candidatura</summary>
        /// <param name="ficheiros">Lista de ficheiros a desassociar</param>
        /// <returns>Redireciona para a action Criar, já com os ficheiros removidos</returns>
        public IActionResult RemoverDocumentos(string[] ficheiros)
        {
            if (ficheiros.Count() > 0)
            {
                var caminho = Path.Combine(BASE_PATH, User.FindFirstValue(ClaimTypes.NameIdentifier));
                foreach (string s in ficheiros)
                {

                    _fileController.Delete(caminho, s);

                }
                //SetSuccessMessage(ficheiros.Count() + " documentos removidos.");
            }
            return RedirectToAction(nameof(Criar));
        }

        /// <summary>Action responsável pela aceitação de uma candidatura</summary>
        /// <param name="id">Id da candidatura a aceitar</param>
        /// <returns>Redireciona para a action Detalhes, caso a candidatura exista e tenha sido aceite com sucesso.</returns>
        public IActionResult Aceitar(int? id)
        {
            Candidatura candidatura = _candidaturas.SingleOrDefault(p => p.CandidaturaId == id);
            if (candidatura != null)
            {
                Programa programa = _context.Programas.Include(p => p.EscolasParceiras).ThenInclude(p => p.EscolaParceira).SingleOrDefault(p => p.ProgramaId == candidatura.ProgramaId);
                ProgramaEscolaParceira assoc = programa.EscolasParceiras.SingleOrDefault(e => e.EscolaParceiraId == candidatura.EscolaParceiraId);
                if(assoc.NumeroVagas > 0)
                {
                    assoc.NumeroVagas--;
                    candidatura.Estado = _context.Estados.SingleOrDefault(e => e.Nome == "Aceite");
                    _context.SaveChanges();
                    SetSuccessMessage("Candidatura aceite.");
                    return RedirectToAction(nameof(Detalhes), new { id = id });
                }

                SetErrorMessage("006");
               return RedirectToAction(nameof(Detalhes), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>Action responsável pela recusa de uma candidatura</summary>
        /// <param name="CandidaturaId">Id da candidatura a aceitar</param>
        /// <param name="motivo">Motivo da rejeição</param>
        /// <returns>Redireciona para a action Detalhes, caso a candidatura exista e tenha sido recusada com sucesso.</returns>
        public IActionResult Recusar(int CandidaturaId, string motivo)
        {
            Candidatura candidatura = _candidaturas.SingleOrDefault(p => p.CandidaturaId == CandidaturaId);
            if (candidatura != null)
            {
                candidatura.Estado = _context.Estados.SingleOrDefault(e => e.Nome == "Recusada");
                candidatura.Motivo = motivo;
                _context.SaveChanges();
                SetSuccessMessage("Candidatura recusada.");
                return RedirectToAction(nameof(Detalhes), new { id = CandidaturaId });
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>Action responsável pela publicação dos resultados das candidaturas aos vários programas de mobilidade</summary>
        /// <param name="programasPublicar">Array com os ids dos programas a publicar</param>
        /// <returns>Redirecciona para a action index</returns>
        [HttpPost]
        public async Task<IActionResult> PublicarResultados(int[] programasPublicar)
        {
            if (programasPublicar.Count() > 0)
            {
                foreach (Candidatura c in _candidaturas)
                {
                    if (programasPublicar.Where(p => p == c.ProgramaId).Any() && c.Estado != _context.Estados.SingleOrDefault(e => e.Nome == "Pendente"))
                    {
                        await _emailSender.SendEmailCandidaturaResultado(c);
                    }
                   
                }
                SetSuccessMessage("Resultados Publicados (" + programasPublicar.Count() + " programas)");
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>Action responsável pela associação de uma nova entrevista à candidatura</summary>
        /// <param name="Entrevista">Bind dos campos preenchidos no formulário para um objeto Entrevista</param>
        /// <param name="CandidaturaId">Id da candidatura à qual se pretende associar</param>
        /// <returns>Redirecciona para a action detalhes da candidatura em questão</returns>
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

            return RedirectToAction(nameof(Detalhes), new { id = CandidaturaId });
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
        /// <returns>Redirecciona para a action detalhes da candidatura em questão</returns>
        public IActionResult DesmarcarEntrevista(int EntrevistaId)
        {
            int CandidaturaId = _context.Entrevistas.SingleOrDefault(e => e.EntrevistaId == EntrevistaId).CandidaturaId;
            _context.Entrevistas.Remove(_context.Entrevistas.SingleOrDefault(e => e.EntrevistaId == EntrevistaId));
            _context.SaveChanges();
            SetSuccessMessage("Entrevista desmarcada.");
            return RedirectToAction(nameof(Detalhes), new { id = CandidaturaId });
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
        /// <returns>Redirecciona para a action detalhes da candidatura em questão</returns>
        public IActionResult AvaliarEntrevista(int EntrevistaId, int Pontuacao, string Comentarios)
        {
            Entrevista Entrevista = _context.Entrevistas.SingleOrDefault(e => e.EntrevistaId == EntrevistaId);
            if (Pontuacao >=0 && Pontuacao <= 10)
            {
                Entrevista.Estado = EstadoEntrevista.Realizada;
                Entrevista.Pontuacao = Pontuacao;
                Entrevista.Comentarios = Comentarios;
                SetSuccessMessage("Entrevista avaliada.");
                _context.SaveChanges();
                return RedirectToAction(nameof(Detalhes), new { id = Entrevista.CandidaturaId });
            }

            SetErrorMessage("003");
            return RedirectToAction(nameof(Detalhes), new { id = Entrevista.CandidaturaId });
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
        /// <returns>Redirecciona para a action detalhes da candidatura em questão</returns>
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
                        sucesso = true;
                    }
                } else {
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
            } else
            {
                SetErrorMessage("003");
            }
            return RedirectToAction(nameof(Detalhes), new { id = Entrevista.CandidaturaId });
        }

        /// <summary>Método que coloca a informação nas tooltips dos campos relacionados com candidaturas</summary>
        private void SetHelpTooltips()
        {
            ViewData["Programa"] = _ajudas.Single(ai => ai.Action == "*" && ai.Elemento == "Programa").Texto;
            ViewData["EscolaParceira"] = _ajudas.Single(ai => ai.Action == "*" && ai.Elemento == "EscolaParceira").Texto;
            ViewData["Curso"] = _ajudas.Single(ai => ai.Action == "*" && ai.Elemento == "Curso").Texto;
        }
    }
}