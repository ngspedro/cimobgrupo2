using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cimobgrupo2.Controllers;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>Extensão para auxilio a gerar links para operações como emails de confirmação, etc.</summary>
    public static class UrlHelperExtensions
    {
        /// <summary>Método para gerar um email de confirmação de registo</summary>
        /// <param name="urlHelper">Objeto da classe urlHelper</param>
        /// <param name="userId">Id do utilizador cuja conta se pretende ativar</param>
        /// <param name="code">Código para ativação de conta</param>
        /// <param name="scheme">Protocolo</param>
        /// <returns>String com o link gerado</returns>
        public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ConfirmEmail),
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }

        /// <summary>Método para gerar um email de reset de password</summary>
        /// <param name="urlHelper">Objeto da classe urlHelper</param>
        /// <param name="userId">Id do utilizador cuja password de acesso se pretende recuperar</param>
        /// <param name="code">Código para reset de password</param>
        /// <param name="scheme">Protocolo</param>
        /// <returns>String com o link gerado</returns>
        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ResetPassword),
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }
    }
}
