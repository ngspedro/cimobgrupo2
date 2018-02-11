using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using cimobgrupo2.Services;
using cimobgrupo2.Models;

namespace cimobgrupo2.Services
{
    /// <summary>Extens�o para ajudar no envio de emails</summary>
    /// <remarks>Possui m�todos para envio de emails de confirma��o, resultado candidatura, etc.</remarks> 
    public static class EmailSenderExtensions
    {
        /// <summary>M�todo para envio assincrono de um email de confirma��o de conta</summary>
        /// <param name="emailSender">Objeto com o emailsender (classe para envio de emails)</param>
        /// <param name="email">Endere�o email para onde se pretende enviar o mail de confirma��o</param>
        /// <param name="link">Link callback (para quando clicado ativar a conta)</param>
        /// <returns>Objeto da tarefa realizada (Task)</returns>
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirmar Conta",
                $"<h1>CIMOB - Sistema de apoio � mobilidade </h1> <br />" +
                $"Obrigado por se registar no nosso sistema. <br /> " +
                $"Por favor ative a sua conta atrav�s do seguinte link: <a href='{HtmlEncoder.Default.Encode(link)}'>ATIVAR CONTA</a>");
        }

        /// <summary>M�todo para envio assincrono de um email de recupera��o de password</summary>
        /// <param name="emailSender">Objeto com o emailsender (classe para envio de emails)</param>
        /// <param name="email">Endere�o email para onde se pretende enviar o mail de confirma��o</param>
        /// <param name="link">Link callback (para quando clicado ir para o formul�rio de recupera��o de password)</param>
        /// <returns>Objeto da tarefa realizada (Task)</returns>
        public static Task SendEmailForgotPasswordAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Recupera��o de Password",
                $"<h1>CIMOB - Sistema de apoio � mobilidade </h1> <br />" +
                $"Recebemos o seu pedido de recupera��o de password. <br /> " +
                $"Pode proceder atrav�s do seguinte link: <a href='{HtmlEncoder.Default.Encode(link)}'>RECUPERAR PASSWORD</a>" + 
                $"<br /><br />Caso n�o tenha feito nenhum pedido de recupera��o de password, ignore este e-mail");

        }

        /// <summary>M�todo para envio assincrono de um email com o resultado de uma candidatura</summary>
        /// <param name="emailSender">Objeto com o emailsender (classe para envio de emails)</param>
        /// <param name="c">Candidatura cujo resultado se pretende "publicar"</param>
        /// <returns>Objeto da tarefa realizada (Task)</returns>
        public static Task SendEmailCandidaturaResultado(this IEmailSender emailSender, Candidatura c)
        {
            return emailSender.SendEmailAsync(c.User.Email, "Resultado da Candidatura",
                $"<h1>CIMOB - Sistema de apoio � mobilidade </h1> <br />" +
                $"<br /><strong>Detalhes da Candidatura</strong>: <br /><br/>" +
                $"Nome: " + c.User.Nome + "<br />" +
                $"Programa: " + c.Programa.Nome + "<br />" +
                $"Escola: " + c.EscolaParceira.Nome + "<br />" +
                $"Curso: " + c.Curso.Nome + "<br />" +
                $"Resultado: " + c.Estado.Nome + "<br />" +
                $"<br /> <strong>Nota:</strong> Para mais informa��es, dirija-se � sua �rea pessoal, na aplica��o.");

        }
        public static Task SendEmailMarcacaoEntrevista(this IEmailSender emailSender, Entrevista e)
        {
            // por completar a cena
            return null;
        }
    }
}
