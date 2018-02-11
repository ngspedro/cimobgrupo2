using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cimobgrupo2.Controllers;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>Extens�o para auxilio a gerar links para opera��es como emails de confirma��o, etc.</summary>
    public static class UrlHelperExtensions
    {
        /// <summary>M�todo para gerar um email de confirma��o de registo</summary>
        /// <param name="urlHelper">Objeto da classe urlHelper</param>
        /// <param name="userId">Id do utilizador cuja conta se pretende ativar</param>
        /// <param name="code">C�digo para ativa��o de conta</param>
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

        /// <summary>M�todo para gerar um email de reset de password</summary>
        /// <param name="urlHelper">Objeto da classe urlHelper</param>
        /// <param name="userId">Id do utilizador cuja password de acesso se pretende recuperar</param>
        /// <param name="code">C�digo para reset de password</param>
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
