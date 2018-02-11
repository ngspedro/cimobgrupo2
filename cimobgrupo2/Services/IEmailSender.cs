using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cimobgrupo2.Models;

namespace cimobgrupo2.Services
{
    /// <summary>Interface que define os métodos a serem implementados pelas classes de envio de email</summary>
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
