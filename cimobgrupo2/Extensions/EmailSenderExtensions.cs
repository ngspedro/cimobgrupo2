using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using cimobgrupo2.Services;

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
    }
}
