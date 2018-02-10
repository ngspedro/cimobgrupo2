using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Services
{
    /// <summary>Classe para envio de emails</summary>
    public class EmailSender : IEmailSender
    {
        /// <summary>Construtor com parametros para criaçao do EmailSender</summary>
        /// <param name="optionsAcessor">Options Acessor</param>
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        /// <summary>Propriedade correspondente ao objeto que contem os dados de acesso à conta de envio sendgrid</summary>
        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        /// <summary>Método para envio assincrono de um email</summary>
        /// <param name="email">Email para o qual se pretende enviar o email</param>
        /// <param name="subject">Assunto do email</param>
        /// <param name="message">Mensagem do email</param>
        /// <returns>Objeto da tarefa realizada (Task)</returns>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        /// <summary>Método para execução do envio de um email no sendgrid</summary>
        /// <param name="apiKey">Key de envio sendgrid</param>
        /// <param name="subject">Assunto do email</param>
        /// <param name="message">Mensagem do email</param>
        /// <param name="email">Email para o qual se pretende enviar o email</param>
        /// <returns>Objeto da tarefa realizada (Task)</returns>
        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("cimob@no-reply.ips.pt", "CIMOB"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }
    }
}
