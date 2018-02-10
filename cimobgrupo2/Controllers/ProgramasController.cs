using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using cimobgrupo2.Models;
using cimobgrupo2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using cimobgrupo2.Models.FilesViewModels;
using cimobgrupo2.Extensions;

namespace cimobgrupo2.Controllers
{
    /// <summary>Controlador para programas</summary>
    /// <remarks>Extende de BaseController</remarks>
    public class ProgramasController : BaseController
    {
        /// <summary>Atributo para o caminho base de gravação de ficheiros associados a programas</summary>
        private string BASE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "programas");

        /// <summary>Atributo para a lista de programas</summary>
        private List<Programa> _programas;

        /// <summary>Construtor com parametros - ProgramasController</summary>
        /// <param name="context">Context da Bd</param>
        /// <param name="fileProvider">File Provider</param>
        public ProgramasController(ApplicationDbContext context, IFileProvider fileProvider) : base(context, fileProvider, "Programas")
        {
            _programas = context.Programas.Include(e => e.EscolasParceiras).ThenInclude(e => e.EscolaParceira)
                .ThenInclude(e => e.Cursos).ThenInclude(e => e.Curso).ToList();
        }

        /// <summary>Action que prepara e mostra o index</summary>
        /// <returns>Retorna a view</returns>
        public IActionResult Index()
        {
            SetHelpModal("Index");
            return View(ProperView("Index"), _programas);
        }

        /// <summary>Action responsável por preparar e mostrar a página de detalhes de um programa</summary>
        /// <param name="id">id do programa cujos detalhes se pretendem visualizar</param>
        /// <returns>Caso o programa seja válido, retorna a view de detalhes do mesmo. Caso contrário redirecciona para o index</returns>
        public IActionResult Detalhes(int? id)
        {
            Programa programa = _programas.Find(p => p.ProgramaId == id);
            if (programa != null)
            {
                SetHelpModal("Detalhes");
                var caminho = "programas/" + id;
                ViewBag.Edital = _fileController.GetFile(caminho, programa.Edital);
                ViewBag.Documentos = _fileController.GetFiles(caminho, programa.Edital);
                return View(ProperView("Detalhes"), programa);
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>Action responsável por preparar e exibir a página de criação de programa</summary>
        /// <returns>View com o formulário de criação</returns>
        public IActionResult Adicionar()
        {
            SetHelpModal("Adicionar");
            SetHelpTooltips();
            return View(ProperView("Adicionar"));
        }

        /// <summary>Action que trata da criação de um programa e da sua adição à bd</summary>
        /// <param name="Programa">Bind dos campos do formulário preenchidos pelo utilizador, num objeto Programa</param>
        /// <param name="file">Edital do programa</param>
        /// <returns>Redireciona para a acção Index, se adicionada com sucesso.</returns>
        [HttpPost]
        public async Task<IActionResult> Adicionar([Bind("ProgramaId,Nome,Duracao,Descricao")] Programa Programa, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Programa);
                _context.SaveChanges();

                if (file != null)
                {
                    Programa.Edital = file.GetFilename();

                    var caminho = Path.Combine(BASE_PATH, Programa.ProgramaId.ToString());
                    await _fileController.UploadFile(caminho, file);
                }

                _context.SaveChanges();
                SetSuccessMessage("Programa adicionado.");
                return RedirectToAction(nameof(Index));
            }
            SetErrorMessage("003");
            SetHelpModal("Adicionar");
            SetHelpTooltips();
            return View(Programa);
        }

        /// <summary>Action responsável por preparar e exibir a página de edição de programa</summary>
        /// <param name="Id">Id do programa a editar</param>
        /// <returns>View com o formulário de edição</returns>
        public IActionResult Editar(int? Id)
        {
            Programa Programa = _context.Programas.SingleOrDefault(p => p.ProgramaId == Id);
            ViewBag.EscolasAssociar = _context.EscolasParceiras.Where(e => e.Programas.Where(p => p.Programa == Programa).Count() == 0);

            var caminho = "programas/" + Id;
            ViewBag.Edital = _fileController.GetFile(caminho, Programa.Edital);
            ViewBag.Documentos = _fileController.GetFiles(caminho, Programa.Edital);
            SetHelpModal("Editar");
            SetHelpTooltips();
            return View(ProperView("Editar"), Programa);
        }

        /// <summary>Action que trata da edição de um programa e da sua atualização na bd</summary>
        /// <param name="Programa">Bind dos campos do formulário preenchidos pelo utilizador, num objeto Programa</param>
        /// <param name="file">Ficheirod do edital do programa</param>
        /// <returns>Redireciona para a acção Index, se editada com sucesso</returns>
        [HttpPost]
        public async Task<IActionResult> Editar([Bind("ProgramaId,Nome,Duracao,Descricao")] Programa Programa, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Programa atual = _context.Programas.SingleOrDefault(p => p.ProgramaId == Programa.ProgramaId);
                atual.Nome = Programa.Nome;
                atual.Duracao = Programa.Duracao;
                atual.Descricao = Programa.Descricao;


                if (file != null)
                {
                    var caminho = Path.Combine(BASE_PATH, atual.ProgramaId.ToString());
                    _fileController.Delete(caminho, atual.Edital);
                    atual.Edital = file.GetFilename();
                    await _fileController.UploadFile(caminho, file);
                }

                _context.SaveChanges();
                SetSuccessMessage("Programa editado.");
                return RedirectToAction(nameof(Index));
            }

            SetErrorMessage("003");
            SetHelpModal("Editar");
            SetHelpTooltips();
            return View(Programa);
        }

        /// <summary>Action responsável por exibir o modal de confirmação de remoção de programa</summary>
        /// <param name="Id">Id do programa a remover</param>
        /// <returns>Partialview com o devido modal</returns>
        public IActionResult RemoverModal(int? Id)
        {
            return PartialView(ProperView("RemoverModal"), _context.Programas.SingleOrDefault(p => p.ProgramaId == Id));
        }

        /// <summary>Action que trata da remoção de um problema da bd</summary>
        /// <param name="ProgramaId">Id do programa a remover</param>
        /// <returns>Redireciona para a acção Index</returns>
        public IActionResult Remover(int ProgramaId)
        {
            _context.Programas.Remove(_context.Programas.SingleOrDefault(p => p.ProgramaId == ProgramaId));
            _context.SaveChanges();

            foreach (FileDetails f in _fileController.GetFiles("programas/" + ProgramaId))
            {
                _fileController.Delete(Path.Combine(BASE_PATH, ProgramaId.ToString()), f.Name);
            }

            SetSuccessMessage("Programa removido.");
            return RedirectToAction(nameof(Index));
        }

        /// <summary>Action responsável por desassociar escolas de um determinada programa</summary>
        /// <param name="ProgramaId">Id do programaa</param>
        /// <param name="escolasRemover">Lista com ids das escolas a desassociar</param>
        /// <returns>Redireciona para a action de editar de determinado programa</returns>
        public IActionResult RemoverAssociacoes(int ProgramaId, int[] escolasRemover)
        {
            if (escolasRemover.Count() > 0)
            {
                Programa Programa = _context.Programas.SingleOrDefault(p => p.ProgramaId == ProgramaId);
                foreach (int i in escolasRemover)
                {
                    Programa.EscolasParceiras.Remove(Programa.EscolasParceiras.SingleOrDefault(e => e.ProgramaId == ProgramaId && e.EscolaParceiraId == i));

                }
                _context.SaveChanges();
                SetSuccessMessage(escolasRemover.Count() + " associações removidas.");
            }
            return RedirectToAction(nameof(Editar), new { Id = ProgramaId });
        }


        /// <summary>Action responsável por remover e desassociar uma lista de ficheiros, de um determinado programa</summary>
        /// <param name="ProgramaId">Id do programa</param>
        /// <param name="ficheirosRemover">Lista de ficheiros a desassociar</param>
        /// <returns>Redireciona para a action Editar de um determinado programa, já com os ficheiros removidos</returns>
        public IActionResult RemoverDocumentos(int ProgramaId, string[] ficheirosRemover)
        {
            if (ficheirosRemover.Count() > 0)
            {
                var caminho = Path.Combine(BASE_PATH, ProgramaId.ToString());
                foreach (string s in ficheirosRemover)
                {

                    _fileController.Delete(caminho, s);

                }
                SetSuccessMessage(ficheirosRemover.Count() + " documentos removidos.");
            }
            return RedirectToAction(nameof(Editar), new { Id = ProgramaId });
        }

        /// <summary>Action responsável por carregar e associar uma lista de ficheiros submetidos pelo utilizador ao programa</summary>
        /// <param name="ProgramaId">Id do programa</param>
        /// <param name="files">Lista de ficheiros a carregar e associar</param>
        /// <returns>Redireciona para a action Editar de determinado programa, já com os ficheiros carregados e associados</returns>
        [HttpPost]
        public async Task<IActionResult> AssociarDocumentos(int ProgramaId, List<IFormFile> files)
        {
            if (files.Count() > 0)
            {
                var caminho = Path.Combine(BASE_PATH, ProgramaId.ToString());
                foreach (IFormFile f in files)
                {
                    await _fileController.UploadFile(caminho, f);
                }
                SetSuccessMessage(files.Count() + " documentos associados.");
            }

            return RedirectToAction(nameof(Editar), new { Id = ProgramaId });
        }

        /// <summary>Action responsável por associar escolas a um determinado programa</summary>
        /// <param name="ProgramaId">Id do programa</param>
        /// <param name="escolasAssociar">Lista com ids das escolas a associar</param>
        /// <returns>Redireciona para a action de editar de determinado programa</returns>
        public IActionResult AssociarEscolas(int ProgramaId, int[] escolasAssociar)
        {
            if (escolasAssociar.Count() > 0)
            {
                Programa Programa = _context.Programas.SingleOrDefault(p => p.ProgramaId == ProgramaId);
                foreach (int i in escolasAssociar)
                {
                    _context.Add(new ProgramaEscolaParceira()
                    {
                        Programa = Programa,
                        EscolaParceira = _context.EscolasParceiras.SingleOrDefault(e => e.EscolaParceiraId == i)
                    });
                }
                _context.SaveChanges();
                SetSuccessMessage("Escolas Parceiras associadas.");
            }

            return RedirectToAction(nameof(Editar), new { Id = ProgramaId });
        }

        /// <summary>Action responsável por exibir o modal de edição de vagas do programa, para uma determinada escola</summary>
        /// <param name="idPrograma">Id do programa </param>
        /// <param name="idEscola">Id da escola</param>
        /// <returns>Partialview com o devido modal</returns>
        public IActionResult EditarVagasModal(int idPrograma, int idEscola)
        {
            Programa programa = _programas.SingleOrDefault(p => p.ProgramaId == idPrograma);
            return PartialView(ProperView("EditarVagasModal"), programa.EscolasParceiras.SingleOrDefault(ep => ep.EscolaParceiraId == idEscola));
        }

        /// <summary>Action que trata da edição de vagas do programa, para uma determinada escola</summary>
        /// <param name="ProgramaId">Id do programa</param>
        /// <param name="EscolaParceiraId">Id da escola parceira</param>
        /// <param name="NumeroDeVagas">Novo número de vagas</param>
        /// <returns>Redireciona para a acção Index</returns>
        public IActionResult EditarVagas(int ProgramaId, int EscolaParceiraId, int NumeroVagas)
        {
            ProgramaEscolaParceira Assoc = _programas.SingleOrDefault(p => p.ProgramaId == ProgramaId).EscolasParceiras.SingleOrDefault(ep => ep.EscolaParceiraId == EscolaParceiraId);

            if (Assoc != null)
            {
                Assoc.NumeroVagas = NumeroVagas;
                _context.SaveChanges();
                SetSuccessMessage("Número de Vagas alterado.");
               
            }
            return RedirectToAction(nameof(Editar), new { Id = ProgramaId });
        }

        /// <summary>Método que coloca a informação nas tooltips dos campos relacionados com programas</summary>
        private void SetHelpTooltips()
        {
            ViewData["Nome"] = _ajudas.Single(ai => ai.Action == "*" && ai.Elemento == "Nome").Texto;
            ViewData["Duracao"] = _ajudas.Single(ai => ai.Action == "*" && ai.Elemento == "Duracao").Texto;
            ViewData["EditalTooltip"] = _ajudas.Single(ai => ai.Action == "*" && ai.Elemento == "Edital").Texto;
            ViewData["Descricao"] = _ajudas.Single(ai => ai.Action == "*" && ai.Elemento == "Descricao").Texto;
        }
    }
}