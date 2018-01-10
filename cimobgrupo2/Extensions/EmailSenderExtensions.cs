using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using cimobgrupo2.Services;
using cimobgrupo2.Models;

namespace cimobgrupo2.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirmar Conta",
                $"<h1>CIMOB - Sistema de apoio à mobilidade </h1> <br />" +
                $"Obrigado por se registar no nosso sistema. <br /> " +
                $"Por favor ative a sua conta através do seguinte link: <a href='{HtmlEncoder.Default.Encode(link)}'>ATIVAR CONTA</a>");
        }

        public static Task SendEmailForgotPasswordAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Recuperação de Password",
                $"<h1>CIMOB - Sistema de apoio à mobilidade </h1> <br />" +
                $"Recebemos o seu pedido de recuperação de password. <br /> " +
                $"Pode proceder através do seguinte link: <a href='{HtmlEncoder.Default.Encode(link)}'>RECUPERAR PASSWORD</a>" + 
                $"<br /><br />Caso não tenha feito nenhum pedido de recuperação de password, ignore este e-mail");

        }

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
    }
}
