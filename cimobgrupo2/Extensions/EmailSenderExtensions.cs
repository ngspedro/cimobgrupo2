using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using cimobgrupo2.Services;
using cimobgrupo2.Models;

namespace cimobgrupo2.Services
{
    /// <summary>Extensão para ajudar no envio de emails</summary>
    /// <remarks>Possui métodos para envio de emails de confirmação, resultado candidatura, etc.</remarks> 
    public static class EmailSenderExtensions
    {
        /// <summary>Método para envio assincrono de um email de confirmação de conta</summary>
        /// <param name="emailSender">Objeto com o emailsender (classe para envio de emails)</param>
        /// <param name="email">Endereço email para onde se pretende enviar o mail de confirmação</param>
        /// <param name="link">Link callback (para quando clicado ativar a conta)</param>
        /// <returns>Objeto da tarefa realizada (Task)</returns>
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirmar Conta",
                $"<h1>CIMOB - Sistema de apoio à mobilidade </h1> <br />" +
                $"Obrigado por se registar no nosso sistema. <br /> " +
                $"Por favor ative a sua conta através do seguinte link: <a href='{HtmlEncoder.Default.Encode(link)}'>ATIVAR CONTA</a>");
        }

        /// <summary>Método para envio assincrono de um email de recuperação de password</summary>
        /// <param name="emailSender">Objeto com o emailsender (classe para envio de emails)</param>
        /// <param name="email">Endereço email para onde se pretende enviar o mail de confirmação</param>
        /// <param name="link">Link callback (para quando clicado ir para o formulário de recuperação de password)</param>
        /// <returns>Objeto da tarefa realizada (Task)</returns>
        public static Task SendEmailForgotPasswordAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Recuperação de Password",
                $"<h1>CIMOB - Sistema de apoio à mobilidade </h1> <br />" +
                $"Recebemos o seu pedido de recuperação de password. <br /> " +
                $"Pode proceder através do seguinte link: <a href='{HtmlEncoder.Default.Encode(link)}'>RECUPERAR PASSWORD</a>" + 
                $"<br /><br />Caso não tenha feito nenhum pedido de recuperação de password, ignore este e-mail");

        }

        /// <summary>Método para envio assincrono de um email com o resultado de uma candidatura</summary>
        /// <param name="emailSender">Objeto com o emailsender (classe para envio de emails)</param>
        /// <param name="c">Candidatura cujo resultado se pretende "publicar"</param>
        /// <returns>Objeto da tarefa realizada (Task)</returns>
        public static Task SendEmailCandidaturaResultado(this IEmailSender emailSender, Candidatura c)
        {
            return emailSender.SendEmailAsync(c.User.Email, "Resultado da Candidatura",
                $"<h1>CIMOB - Sistema de apoio à mobilidade </h1> <br />" +
                $"<br /><strong>Detalhes da Candidatura</strong>: <br /><br/>" +
                $"Nome: " + c.User.Nome + "<br />" +
                $"Programa: " + c.Programa.Nome + "<br />" +
                $"Escola: " + c.EscolaParceira.Nome + "<br />" +
                $"Curso: " + c.Curso.Nome + "<br />" +
                $"Resultado: " + c.Estado.Nome + "<br />" +
                $"<br /> <strong>Nota:</strong> Para mais informações, dirija-se à sua área pessoal, na aplicação.");

        }
        public static Task SendEmailMarcacaoEntrevista(this IEmailSender emailSender, Entrevista e)
        {
            // por completar a cena
            return null;
        }
    }
}
