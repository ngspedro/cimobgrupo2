using cimobgrupo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.AjudaInputs.Any())
            {
                context.AjudaInputs.Add(new AjudaInput("Account", "Login", "Username", "Username associado à conta"));
                context.AjudaInputs.Add(new AjudaInput("Account", "Login", "Password", "Password associada à conta"));
                context.AjudaInputs.Add(new AjudaInput("Account", "Login", "RememberMe", "Seleccione esta opção caso pretende que o login fique feito mesmo após fechar o navegador"));

                context.AjudaInputs.Add(new AjudaInput("Account", "Registo", "Nome", "Preencha com o seu nome"));
                context.AjudaInputs.Add(new AjudaInput("Account", "Registo", "DataNascimentoPicker", "Preencha com a sua data de nascimento"));
                context.AjudaInputs.Add(new AjudaInput("Account", "Registo", "Email", "Preencha com um email válido"));
                context.AjudaInputs.Add(new AjudaInput("Account", "Registo", "Contato", "Preencha com o seu contato"));
                context.AjudaInputs.Add(new AjudaInput("Account", "Registo", "Username", "Preencha com o username de acesso à conta que pretende"));
                context.AjudaInputs.Add(new AjudaInput("Account", "Registo", "Password", "Preencha com a password que pretende. Note que esta tem que ter pelo menos 5 caracteres"));
                context.AjudaInputs.Add(new AjudaInput("Account", "Registo", "ConfirmarPassword", "Repita a password que introduziu acima"));

                context.AjudaInputs.Add(new AjudaInput("Account", "ForgotPassword", "Email", "Preencha com o email pertencente à conta cuja password pretende recuperar"));

                context.AjudaInputs.Add(new AjudaInput("Account", "ResetPassword", "Password", "Preencha com a nova password. Note que esta tem que ter pelo menos 5 caracteres"));
                context.AjudaInputs.Add(new AjudaInput("Account", "ResetPassword", "ConfirmarPassword", "Repita a nova password"));

                context.AjudaInputs.Add(new AjudaInput("Manage", "ChangeDetails", "Nome", "Aqui pode editar o seu nome"));
                context.AjudaInputs.Add(new AjudaInput("Manage", "ChangeDetails", "DataNascimentoPicker", "Aqui pode editar a sua data de nascimento"));
                context.AjudaInputs.Add(new AjudaInput("Manage", "ChangeDetails", "Email", "Aqui pode editar o seu email. Note que este tem que ser um email válido"));
                context.AjudaInputs.Add(new AjudaInput("Manage", "ChangeDetails", "Contato", "Aqui pode editar o contato associado à sua conta"));

                context.AjudaInputs.Add(new AjudaInput("Manage", "ChangePassword", "PasswordAntiga", "Para alterar a sua password, tem que introduzir neste campo a sua password atual"));
                context.AjudaInputs.Add(new AjudaInput("Manage", "ChangePassword", "NovaPassword", "Preencha com a password para a qual deseja alterar"));
                context.AjudaInputs.Add(new AjudaInput("Manage", "ChangePassword", "ConfirmarNovaPassword", "Confirme a password que introduziu no campo acima"));

                context.AjudaInputs.Add(new AjudaInput("Manage", "DeleteAccount", "PasswordAtual", "Por motivos de segurança é necessário que preencha este campo com a sua password atual para poder eliminar a sua conta"));

            }

            if (!context.Erros.Any())
            {
                context.Erros.Add(new Erro("001", "Conta inexistente!"));
                context.Erros.Add(new Erro("002", "Login falhou. Verifique se preencheu todos os campos!"));
                context.Erros.Add(new Erro("003", "Verifique se os dados introduzidos estão corretos."));
            }
                context.SaveChanges();
        }
    }
}
