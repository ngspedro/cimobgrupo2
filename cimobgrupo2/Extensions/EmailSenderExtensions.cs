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
                $"<h1>CIMOB - Sistema de apoio � mobilidade </h1> <br />" +
                $"Obrigado por se registar no nosso sistema. <br /> " +
                $"Por favor ative a sua conta atrav�s do seguinte link: <a href='{HtmlEncoder.Default.Encode(link)}'>ATIVAR CONTA</a>");
        }

        public static Task SendEmailForgotPasswordAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Recupera��o de Password",
                $"<h1>CIMOB - Sistema de apoio � mobilidade </h1> <br />" +
                $"Recebemos o seu pedido de recupera��o de password. <br /> " +
                $"Pode proceder atrav�s do seguinte link: <a href='{HtmlEncoder.Default.Encode(link)}'>RECUPERAR PASSWORD</a>" + 
                $"<br /><br />Caso n�o tenha feito nenhum pedido de recupera��o de password, ignore este e-mail");

        }
    }
}
