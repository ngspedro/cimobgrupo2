using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cimobgrupo2.Data;
using cimobgrupo2.Models;
using Microsoft.Extensions.FileProviders;

namespace cimobgrupo2.Controllers
{
    /// <summary>Controlador base para todos os outros controllers</summary>
    /// <remarks>Possui as propriedades / métodos comuns a todos eles</remarks> 
    public class BaseController : Controller
    {
        /// <summary>Atributo para o manter o nome do controller atual (usado na exibição de mensagens, caminhos, etc.)</summary>
        protected string _controllerName;

        /// <summary>Atributo para o Context da Bd</summary>
        protected ApplicationDbContext _context;

        /// <summary>Atributo para o File Controller (para realizar operações com ficheiros)</summary>
        protected FileController _fileController;

        /// <summary>Atributo para a lista de ajudas</summary>
        protected readonly List<Ajuda> _ajudas;

        /// <summary>Atributo para a lista de erros</summary>
        protected readonly List<Erro> _erros;

        /// <summary>Construtor com parametros - BaseController</summary>
        /// <param name="context">Context da Bd</param>
        /// <param name="fileProvider">File Provider</param>
        /// <param name="controllerName">Nome do controller atual (usado na exibição de mensagens, caminhos, etc.)</param>
        public BaseController(ApplicationDbContext context, IFileProvider fileProvider, string controllerName)
        {
            _fileController = new FileController(fileProvider);
            _context = context;
            _ajudas = context.Ajudas.Where(ai => ai.Controller == controllerName).ToList();
            _erros = context.Erros.ToList();
            _controllerName = controllerName;
        }

        /// <summary>Método para identificar o caminho para a view pretendida, tendo em conta a Role do utilizador</summary>
        /// <param name="viewName">View a tentar ser acedida</param>
        /// <returns>String com o caminho</returns>
        protected String ProperView(String viewName)
        {
            if (User.IsInRole("CIMOB") || User.IsInRole("Admin"))
                return "~/Views/" + _controllerName + "/Cimob/" + viewName + ".cshtml";

            return viewName;
        }

        /// <summary>Método utilizado para mostrar mensagens de erro.</summary>
        /// <param name="Code">Código do erro a mostrar</param>
        protected void SetErrorMessage(String Code)
        {
            var Erro = _erros.SingleOrDefault(e => e.Codigo == Code);
            TempData["Error_Code"] = Erro.Codigo;
            TempData["Error_Message"] = Erro.Mensagem;
        }

        /// <summary>Método utilizado para mostrar mensagens de sucesso.</summary>
        /// <param name="Message">Mensagem a mostrar</param>
        protected void SetSuccessMessage(String Message)
        {
            TempData["Success"] = Message;
        }
    }
}