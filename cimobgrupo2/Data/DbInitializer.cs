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
            if (!context.Ajudas.Any())
            {
                //inputs
                context.Ajudas.Add(new Ajuda("Account", "Login", "Username", "Username associado à conta"));
                context.Ajudas.Add(new Ajuda("Account", "Login", "Password", "Password associada à conta"));
                context.Ajudas.Add(new Ajuda("Account", "Login", "RememberMe", "Seleccione esta opção caso pretende que o login fique feito mesmo após fechar o navegador"));

                context.Ajudas.Add(new Ajuda("Account", "Registo", "Nome", "Preencha com o seu nome"));
                context.Ajudas.Add(new Ajuda("Account", "Registo", "DataNascimentoPicker", "Preencha com a sua data de nascimento"));
                context.Ajudas.Add(new Ajuda("Account", "Registo", "Email", "Preencha com um email válido"));
                context.Ajudas.Add(new Ajuda("Account", "Registo", "Contato", "Preencha com o seu contato"));
                context.Ajudas.Add(new Ajuda("Account", "Registo", "Username", "Preencha com o username de acesso à conta que pretende"));
                context.Ajudas.Add(new Ajuda("Account", "Registo", "Password", "Preencha com a password que pretende. Note que esta tem que ter pelo menos 5 caracteres"));
                context.Ajudas.Add(new Ajuda("Account", "Registo", "ConfirmarPassword", "Repita a password que introduziu acima"));

                context.Ajudas.Add(new Ajuda("Account", "ForgotPassword", "Email", "Preencha com o email pertencente à conta cuja password pretende recuperar"));

                context.Ajudas.Add(new Ajuda("Account", "ResetPassword", "Password", "Preencha com a nova password. Note que esta tem que ter pelo menos 5 caracteres"));
                context.Ajudas.Add(new Ajuda("Account", "ResetPassword", "ConfirmarPassword", "Repita a nova password"));

                context.Ajudas.Add(new Ajuda("Manage", "ChangeDetails", "Nome", "Aqui pode editar o seu nome"));
                context.Ajudas.Add(new Ajuda("Manage", "ChangeDetails", "DataNascimentoPicker", "Aqui pode editar a sua data de nascimento"));
                context.Ajudas.Add(new Ajuda("Manage", "ChangeDetails", "Email", "Aqui pode editar o seu email. Note que este tem que ser um email válido"));
                context.Ajudas.Add(new Ajuda("Manage", "ChangeDetails", "Contato", "Aqui pode editar o contato associado à sua conta"));

                context.Ajudas.Add(new Ajuda("Manage", "ChangePassword", "PasswordAntiga", "Para alterar a sua password, tem que introduzir neste campo a sua password atual"));
                context.Ajudas.Add(new Ajuda("Manage", "ChangePassword", "NovaPassword", "Preencha com a password para a qual deseja alterar"));
                context.Ajudas.Add(new Ajuda("Manage", "ChangePassword", "ConfirmarNovaPassword", "Confirme a password que introduziu no campo acima"));

                context.Ajudas.Add(new Ajuda("Manage", "DeleteAccount", "PasswordAtual", "Por motivos de segurança é necessário que preencha este campo com a sua password atual para poder eliminar a sua conta"));

                //modals
                context.Ajudas.Add(new Ajuda("Manage", "Index", "ModalAjuda", 
                    "<p>Esta página permite-lhe alterar os dados da sua conta.</p>" +
                    "<p>Uma das secções permite-lhe alterar apenas os detalhes da sua conta (nome, contato, etc.),  " +
                    "outra permite-lhe alterar a sua password e tem ainda uma terceira secção onde pode eliminar a sua conta, caso pretenda.</p> <br />" +
                    "<p> Qualquer dúvida que tenha sobre o preenchimento de qualquer um dos campos, passe o rato por cima e consulte as orientações detalhadamente para cada um deles.</p>"
                    ));

                context.Ajudas.Add(new Ajuda("Account", "Login", "ModalAjuda",
                    "<p>Esta página permite-lhe introduzir os seus dados e aceder ao sistema. Para tal precisa de estar registado.</p><br />" +
                    "<strong><p>Observações:</p></strong>  " +
                    "<ul><li>Caso não possua uma conta, pode criá-la clicando em registar;</li>" +
                    "<li>Caso possua uma conta mas não se lembre da sua password, pode sempre recuperá-la através da opção de recuperação da mesma;" +
                    "<li>Caso se tenha acabado de registar mas não estiver a conseguir aceder, lembre-se que tem que a ativar no email que nos forneceu;</li></ul>"
                    ));

                context.Ajudas.Add(new Ajuda("Account", "Register", "ModalAjuda",
                    "<p>Esta página permite-lhe criar uma conta de acesso ao sistema.</p>" + 
                    "<p>Para tal basta preencher o formulário corretamente com a sua informação e submeter o mesmo.</p>" +
                    "<p><strong>Nota:</strong> Depois da submissão do formulário terá que ativar a conta no email que nos forneceu, clicando no link fornecido para o efeito.</p>"
                    ));

                context.Ajudas.Add(new Ajuda("Account", "RecoverPassword", "ModalAjuda",
                    "<p>Esta página permite-lhe recuperar a password de acesso à sua conta.</p>" +
                    "<p>Para tal basta preencher o formulário com o email associado à sua conta, de seguida será-lhe enviado um link para re-definir a mesma.</p>"
                    ));
            }

            if (!context.Erros.Any())
            {
                context.Erros.Add(new Erro("001", "Credenciais Inválidas."));
                context.Erros.Add(new Erro("002", "Login falhou. Verifique se preencheu todos os campos!"));
                context.Erros.Add(new Erro("003", "Verifique se os dados introduzidos estão corretos."));
                context.Erros.Add(new Erro("004", "Registo falhou. Já existe uma conta com esse username."));
                context.Erros.Add(new Erro("005", "Ocorreu um erro inesperado! Tente novamente mais tarde."));
            }
                context.SaveChanges();
        }
    }
}
