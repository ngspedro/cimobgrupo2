using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    /// <summary>Classe para redefinir as mensagens de erro predefinidas do Identity do asp</summary>
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        /// <summary>Método para traduzir o erro em que a password precisa de uma maiuscula</summary>
        /// <returns>IdentityError traduzido</returns>
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "A password tem que ter pelo menos uma maiúscula."
            };
        }

        /// <summary>Método para traduzir o erro em que a password precisa de pelo menos um caracter especial</summary>
        /// <returns>IdentityError traduzido</returns>
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "A password tem que ter pelo menos um caracter especial."
            };
        }

        /// <summary>Método para traduzir o erro em que a password precisa de pelo menos um numero</summary>
        /// <returns>IdentityError traduzido</returns>
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "A password tem que ter pelo menos um número."
            };
        }

        /// <summary>Método para traduzir o erro em que o utilizador já existe</summary>
        /// <returns>IdentityError traduzido</returns>
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = "Já existe um utilizador com o username '" + userName + "'."
            };
        }

        /// <summary>Método para traduzir o erro em que a password é demasiado curta</summary>
        /// <returns>IdentityError traduzido</returns>
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = "A password tem que ter pelo menos 6 caracteres"
            };
        }
    }
}
