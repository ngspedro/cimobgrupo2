using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cimobgrupo2.Models;

namespace cimobgrupo2.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
