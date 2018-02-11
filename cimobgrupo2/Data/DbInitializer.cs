using cimobgrupo2.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Data
{
    /// <summary>Classe para inicializar a bd com dados, quando necessário.</summary>
    public class DbInitializer
    {
        /// <summary>Método para inicializar a bd, quando necessário</summary>
        /// <param name="context">Context da bd a ser inicializada</param>
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Ajudas.Any())
            {
                //inputs

                /*-------inserts da tabela User ou a criação de objeto da tabela--------------------------------------------*/
                if (!context.Users.Any())
                {

                    var utilizadores = new ApplicationUser[]
                    {

                 new ApplicationUser {
                     Id = "5d0a50c4-1222-476e-8eee-07fb06ba5906",
                     AccessFailedCount= 0,
                     ConcurrencyStamp= "10f6d9c4-eb68-4fce-a92f-5b26c28ee8cc",
                     Email ="cimobtestes@testes.com",
                     EmailConfirmed = false,
                     LockoutEnabled = true,
                     LockoutEnd = null,
                     NormalizedEmail = "USERTEST@EMAIL.COM",
                     NormalizedUserName = "USERTEST@EMAIL.COM",
                     PasswordHash = "@Abc123",
                     PhoneNumber=null,
                     PhoneNumberConfirmed = false,
                     SecurityStamp = "d1de02d6 -5f33-481e-b5ce-34fa65b03fc3",
                     TwoFactorEnabled = false,
                     UserName = "testeCimob" }
                    };
                    foreach (ApplicationUser au in utilizadores)
                    {

                        context.Users.Add(au);
                    }
                    context.SaveChanges();
                }

                context.Ajudas.Add(new Ajuda("Account", "Login", "Username", "Username associado à conta."));
                context.Ajudas.Add(new Ajuda("Account", "Login", "Password", "Password associada à conta."));
                context.Ajudas.Add(new Ajuda("Account", "Login", "RememberMe", "Seleccione esta opção caso pretende que o login fique feito mesmo após fechar o navegador."));

                context.Ajudas.Add(new Ajuda("Account", "Registo", "Nome", "Preencha com o seu nome."));
                context.Ajudas.Add(new Ajuda("Account", "Registo", "DataNascimentoPicker",
                    "Preencha com a sua data de nascimento." +
                    "<br />" +
                    "<strong>Nota:</strong> Esta tem que estar no formato dd/mm/yyyy"));
                context.Ajudas.Add(new Ajuda("Account", "Registo", "Email",
                    "Preencha com um email válido." +
                    "<br /> " +
                    "<strong>Nota:</strong> Terá que ativá-lo antes de poder aceder à sua conta."));
                context.Ajudas.Add(new Ajuda("Account", "Registo", "Contato",
                    "Preencha com o seu contato." +
                    "<br />" +
                    "<strong>Nota:</strong> É obrigatório que este seja um número português válido. Isto é, composto por 9 digitos (não é necessária a introdução de qualquer indicativo)"));
                context.Ajudas.Add(new Ajuda("Account", "Registo", "Username", "Preencha com o username de acesso à conta que pretende."));
                context.Ajudas.Add(new Ajuda("Account", "Registo", "Password",
                    "Preencha com a password que pretende." +
                    "<br />" +
                    "<strong>Nota:</strong> " +
                    "<br /><ul>" +
                    "<li>Tem que ter pelo menos 6 caracteres.</li>" +
                    "<li>Tem que ter pelo menos um número.</li>" +
                    "<li>Tem que ter pelo menos uma maiúscula</li>" +
                    "<li>Tem que ter pelo menos um caracter especial.</li>" +
                    "</ul>"));
                context.Ajudas.Add(new Ajuda("Account", "Registo", "ConfirmarPassword", "Repita a password que introduziu acima."));

                context.Ajudas.Add(new Ajuda("Account", "ForgotPassword", "Email", "Preencha com o email pertencente à conta cuja password pretende recuperar."));

                context.Ajudas.Add(new Ajuda("Account", "ResetPassword", "Password",
                    "Preencha com a nova password." +
                    "<br />" +
                    "<strong>Nota:</strong> " +
                    "<br /><ul>" +
                    "<li>Tem que ter pelo menos 6 caracteres.</li>" +
                    "<li>Tem que ter pelo menos um número.</li>" +
                    "<li>Tem que ter pelo menos uma maiúscula</li>" +
                    "<li>Tem que ter pelo menos um caracter especial.</li>" +
                    "</ul>"));
                context.Ajudas.Add(new Ajuda("Account", "ResetPassword", "ConfirmarPassword", "Repita a nova password."));

                context.Ajudas.Add(new Ajuda("Manage", "ChangeDetails", "Nome", "Aqui pode editar o seu nome."));
                context.Ajudas.Add(new Ajuda("Manage", "ChangeDetails", "DataNascimentoPicker",
                    "Aqui pode editar a sua data de nascimento." +
                    "<br />" +
                    "<strong>Nota:</strong> Esta tem que estar no formato dd/mm/yyyy"));
                context.Ajudas.Add(new Ajuda("Manage", "ChangeDetails", "Email",
                    "Aqui pode editar o seu email. " +
                    "<br />" +
                    "<strong>Nota:</strong> Este tem que ser um email válido. (example@example.com)"));
                context.Ajudas.Add(new Ajuda("Manage", "ChangeDetails", "Contato",
                    "Aqui pode editar o contato associado à sua conta." +
                    "<br />" +
                    "<strong>Nota:</strong> É obrigatório que este seja um número português válido. Isto é, composto por 9 digitos (não é necessária a introdução de qualquer indicativo)"));

                context.Ajudas.Add(new Ajuda("Manage", "ChangePassword", "PasswordAntiga", "Para alterar a sua password, tem que introduzir neste campo a sua password atual."));
                context.Ajudas.Add(new Ajuda("Manage", "ChangePassword", "NovaPassword",
                    "Preencha com a password para a qual deseja alterar." +
                    "<br />" +
                    "<strong>Nota:</strong> " +
                    "<br /><ul>" +
                    "<li>Tem que ter pelo menos 6 caracteres.</li>" +
                    "<li>Tem que ter pelo menos um número.</li>" +
                    "<li>Tem que ter pelo menos uma maiúscula</li>" +
                    "<li>Tem que ter pelo menos um caracter especial.</li>" +
                    "</ul>"));
                context.Ajudas.Add(new Ajuda("Manage", "ChangePassword", "ConfirmarNovaPassword", "Confirme a password que introduziu no campo acima."));

                context.Ajudas.Add(new Ajuda("Manage", "DeleteAccount", "PasswordAtual", "Por motivos de segurança é necessário que preencha este campo com a sua password atual para poder eliminar a sua conta."));

                context.Ajudas.Add(new Ajuda("Programas", "*", "Nome",
                   "Preencha com o nome do programa."));

                context.Ajudas.Add(new Ajuda("Programas", "*", "Duracao",
                   "Preencha com a duração do programa"));

                context.Ajudas.Add(new Ajuda("Programas", "*", "Edital",
                   "Seleccione o ficheiro do edital do programa."));

                context.Ajudas.Add(new Ajuda("Programas", "*", "Descricao",
                   "Preencha com a descrição do programa."));

                context.Ajudas.Add(new Ajuda("EscolasParceiras", "*", "Nome",
                  "Preencha com o nome da escola."));

                context.Ajudas.Add(new Ajuda("EscolasParceiras", "*", "Pais",
                  "Seleccione o país da escola."));

                context.Ajudas.Add(new Ajuda("EscolasParceiras", "*", "Localidade",
                  "Preencha com a localidade da escola."));

                context.Ajudas.Add(new Ajuda("Cursos", "*", "Nome",
                 "Preencha com o nome do curso."));

                context.Ajudas.Add(new Ajuda("Candidaturas", "*", "Programa",
                 "Seleccione o programa que pretende."));

                context.Ajudas.Add(new Ajuda("Candidaturas", "*", "EscolaParceira",
                 "Seleccione a escola parceira que pretende."));

                context.Ajudas.Add(new Ajuda("Candidaturas", "*", "Curso",
                 "Seleccione o curso que pretende."));

                context.Ajudas.Add(new Ajuda("Entrevistas", "*", "Candidato",
                 "Seleccione o candidato que pretende."));

                context.Ajudas.Add(new Ajuda("Entrevistas", "*", "Data",
                 "Preencha com a data de realização da entrevista." +
                    "<br />" +
                    "<strong>Nota:</strong> " +
                    "É obrigatório que a data inserida tem de ser superior ou igual data de hoje."
                 ));

                context.Ajudas.Add(new Ajuda("Entrevistas", "*", "Hora",
                 "Preencha com a hora para a entrevista ser realizada."));

                context.Ajudas.Add(new Ajuda("Entrevistas", "*", "Local",
                  "Preencha com o local da entrevista."));

                context.Ajudas.Add(new Ajuda("Entrevistas", "*", "Pontuacao",
                 "Preencha com a pontuação a atribuir à entrevista."));

                context.Ajudas.Add(new Ajuda("Entrevistas", "*", "Comentarios",
                 "Preencha com o comentários adicionais."));

                //MODALS
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

                context.Ajudas.Add(new Ajuda("Programas", "Index", "ModalAjuda",
                    "<p>Esta página permite-lhe visualizar todos os programas disponíveis.</p>" +
                    "<p>Note que a última coluna da tabela possui diversas operações que pode realizar com os programas, dependendo do seu tipo de conta.</p>"
                    ));

                context.Ajudas.Add(new Ajuda("Programas", "Adicionar", "ModalAjuda",
                    "<p>Esta página permite-lhe criar um novo programa de mobilidade.</p>" +
                    "<p>Preencha todos os campos do formulário corretamente. Carregue também o edital que diz respeito ao programa que está a adicionar.</p>" +
                    "<p>Qualquer dúvida que tenha sobre o preenchimento de qualquer um deles, passe o rato por cima e consulte as orientações detalhadamente.</p>"
                    ));

                context.Ajudas.Add(new Ajuda("Programas", "Detalhes", "ModalAjuda",
                   "<p>Esta página permite-lhe visualizar os detalhes de um programa.</p>" +
                   "<p>Esta possui duas seccções. A do lado esquerdo mostra a informação básica do programa (nome, duração, etc.) e a do lado direito mostra a lista de escolas disponíveis para tal programa.</p>"
                   ));

                context.Ajudas.Add(new Ajuda("Programas", "Editar", "ModalAjuda",
                   "<p>Esta página permite-lhe editar um programa.</p>" +
                   "<p>Esta possui duas seccções. A do lado esquerdo permite editar a informação básica do programa (nome, duração, etc.) e a do lado direito permite associar/desassociar escolas parceiras e documentos.</p>"
                   ));

                context.Ajudas.Add(new Ajuda("EscolasParceiras", "Index", "ModalAjuda",
                    "<p>Esta página permite-lhe visualizar todas as escolas parceiras disponíveis.</p>" +
                    "<p>Note que a última coluna da tabela possui diversas operações que pode realizar com as escolas, dependendo do seu tipo de conta.</p>"
                    ));

                context.Ajudas.Add(new Ajuda("EscolasParceiras", "Detalhes", "ModalAjuda",
                   "<p>Esta página permite-lhe visualizar os detalhes de uma escola parceira.</p>" +
                   "<p>Esta possui duas seccções. A do lado esquerdo mostra a informação básica da escola (nome, país, etc.) e a do lado direito mostra a lista de cursos que esta disponibiliza.</p>"
                   ));

                context.Ajudas.Add(new Ajuda("EscolasParceiras", "Editar", "ModalAjuda",
                   "<p>Esta página permite-lhe editar uma escola parceira.</p>" +
                   "<p>Esta possui duas seccções. A do lado esquerdo permite editar a informação básica do programa (nome, país, etc.) e a do lado direito permite associar/desassociar cursos.</p>"
                   ));

                context.Ajudas.Add(new Ajuda("EscolasParceiras", "Adicionar", "ModalAjuda",
                    "<p>Esta página permite-lhe criar uma nova escola parceira.</p>" +
                    "<p>Preencha todos os campos do formulário corretamente.</p>" +
                    "<p>Qualquer dúvida que tenha sobre o preenchimento de qualquer um deles, passe o rato por cima e consulte as orientações detalhadamente.</p>"
                    ));


                context.Ajudas.Add(new Ajuda("Cursos", "Index", "ModalAjuda",
                    "<p>Esta página permite-lhe visualizar todos os cursos disponíveis.</p>" +
                    "<p>Note que a última coluna da tabela possui diversas operações que pode realizar com os cursos, dependendo do seu tipo de conta.</p>"
                    ));

                context.Ajudas.Add(new Ajuda("Candidaturas", "Index", "ModalAjuda",
                    "<p>Esta página permite-lhe visualizar todas as candidaturas feitas pelos estudantes.</p>" +
                    "<strong><p>Fornece ainda acesso a diversas outras funcionalidades, por exemplo:</p></strong>  " +
                    "<ul><li>Publicar Resultados;</li>" +
                    "<li>Detalhes de Candidatura;</li>" +
                    "<li>...</li></ul>"
                    ));

                context.Ajudas.Add(new Ajuda("Candidaturas", "Detalhes", "ModalAjuda",
                    "<p>Esta página permite-lhe visualizar os detalhes de uma candidatura.</p>" +
                    "<p>Esta possui duas seccções. A do lado esquerdo permite visualizar a informação básica da candidatura (programa, escola, etc.) e a do lado direito permite visualizar as entrevistas marcadas.</p>"
                    ));

                context.Ajudas.Add(new Ajuda("Candidaturas", "Criar", "ModalAjuda",
                    "<p>Esta página permite-lhe fazer uma nova candidatura.</p>" +
                    "<p>Esta possui duas seccções. A do lado esquerdo permite lhe seleccionar o programa, escola e curso que pretende e a do lado direito permite-lhe carregar os documentos que precisa de associar à sua candidatura.</p>" +
                    "<p>Qualquer dúvida que tenha sobre o preenchimento de qualquer um dos campos, passe o rato por cima e consulte as orientações detalhadamente.</p>"
                    ));

                context.Ajudas.Add(new Ajuda("Entrevistas", "Index", "ModalAjuda",
                    "<p>Esta página permite-lhe visualizar todas as entrevistas marcadas.</p>" +
                    "<strong><p>Fornece ainda acesso a diversas outras funcionalidades, por exemplo:</p></strong>  " +
                    "<ul><li>Avaliar Entrevistas;</li>" +
                    "<li>Desmarcar Entrevistas;</li>" +
                    "<li>...</li></ul>"
                    ));

                context.SaveChanges();
            }

            if (!context.Erros.Any())
            {
                context.Erros.Add(new Erro("001", "Credenciais Inválidas."));
                context.Erros.Add(new Erro("002", "Login falhou. Verifique se preencheu todos os campos!"));
                context.Erros.Add(new Erro("003", "Verifique se os dados introduzidos estão corretos."));
                context.Erros.Add(new Erro("004", "Registo falhou. Já existe uma conta com esse username."));
                context.Erros.Add(new Erro("005", "Ocorreu um erro inesperado! Tente novamente mais tarde."));
                context.Erros.Add(new Erro("006", "Impossível aceitar candidatura. O programa pretendido não possui mais vagas para a escola em questão."));

                context.SaveChanges();
            }

            if (!context.Programas.Any())
            {
                var Cursos = new[]
                {
                new Curso { Nome = "Engenharia Informática" },
                new Curso { Nome = "Engenharia Biomédica" },
                new Curso { Nome = "Engenharia do Ambiente" },
                new Curso { Nome = "Engenharia Mecânica" }
                };

                var EscolasParceiras = new[]
                {
                new EscolaParceira { Nome = "Escola Parceira 1", Pais = "França", Localidade = "Paris" },
                new EscolaParceira { Nome = "Escola Parceira 2", Pais = "Espanha", Localidade = "Madrid" },
                new EscolaParceira { Nome = "Escola Parceira 3", Pais = "Alemanha", Localidade = "Berlim" }
                };

                var Programas = new[]
                {
                new Programa {
                    Nome = "ERASMUS",
                    Descricao = "O Programa Erasmus destina-se a apoiar as actividades europeias das instituições de ensino superior (IES), " +
                    "promovendo a mobilidade e o intercâmbio de estudantes, professores e funcionários das Instituições de Ensino Superior.",
                    Duracao = 6,
                    Edital = "edital.txt"
                },
                new Programa { Nome = "SANTANDER", Descricao = "descrição programa santander", Duracao = 12, Edital = "edital.txt" }
                };



                context.AddRange(
                    new EscolaParceiraCurso { EscolaParceira = EscolasParceiras[0], Curso = Cursos[0] },
                    new EscolaParceiraCurso { EscolaParceira = EscolasParceiras[0], Curso = Cursos[1] },
                    new EscolaParceiraCurso { EscolaParceira = EscolasParceiras[0], Curso = Cursos[2] },
                    new EscolaParceiraCurso { EscolaParceira = EscolasParceiras[0], Curso = Cursos[3] },
                    new EscolaParceiraCurso { EscolaParceira = EscolasParceiras[1], Curso = Cursos[2] },
                    new EscolaParceiraCurso { EscolaParceira = EscolasParceiras[2], Curso = Cursos[1] },
                    new EscolaParceiraCurso { EscolaParceira = EscolasParceiras[2], Curso = Cursos[0] },
                    new EscolaParceiraCurso { EscolaParceira = EscolasParceiras[2], Curso = Cursos[3] }
                );

                context.SaveChanges();

                context.AddRange(
                    new ProgramaEscolaParceira { Programa = Programas[0], EscolaParceira = EscolasParceiras[0], NumeroVagas = 30 },
                    new ProgramaEscolaParceira { Programa = Programas[0], EscolaParceira = EscolasParceiras[1], NumeroVagas = 3 },
                    new ProgramaEscolaParceira { Programa = Programas[1], EscolaParceira = EscolasParceiras[2], NumeroVagas = 55 }
                );

                context.SaveChanges();
            }

            if (!context.Estados.Any())
            {
                context.Estados.Add(new Estado() { Nome = "Pendente" });
                context.Estados.Add(new Estado() { Nome = "Aceite" });
                context.Estados.Add(new Estado() { Nome = "Recusada" });
                context.Estados.Add(new Estado() { Nome = "Em Criação" });
                context.Estados.Add(new Estado() { Nome = "Avaliada" });
                context.SaveChanges();
            }
            /*inserts a candidatura*/
            if (!context.Candidaturas.Any())
            {
                context.Candidaturas.Add(new Candidatura { UserId = "5d0a50c4-1222-476e-8eee-07fb06ba5906", ProgramaId = 1, EscolaParceiraId = 1, CursoId = 4, EstadoId = 1, Motivo = "Inter Cambio" });
                context.Candidaturas.Add(new Candidatura { UserId = "5d0a50c4-1222-476e-8eee-07fb06ba5906", ProgramaId = 2, EscolaParceiraId = 2, CursoId = 1, EstadoId = 2, Motivo = "Inter Cambio" });
                context.Candidaturas.Add(new Candidatura { UserId = "5d0a50c4-1222-476e-8eee-07fb06ba5906", ProgramaId = 1, EscolaParceiraId = 2, CursoId = 2, EstadoId = 1, Motivo = "Inter Cambio" });
                context.Candidaturas.Add(new Candidatura { UserId = "5d0a50c4-1222-476e-8eee-07fb06ba5906", ProgramaId = 2, EscolaParceiraId = 1, CursoId = 3, EstadoId = 2, Motivo = "Inter Cambio" });
                context.Candidaturas.Add(new Candidatura { UserId = "5d0a50c4-1222-476e-8eee-07fb06ba5906", ProgramaId = 1, EscolaParceiraId = 2, CursoId = 2, EstadoId = 1, Motivo = "Inter Cambio" });
                context.SaveChanges();
            }
            
                if (!context.Entrevistas.Any()){
                /*context.Entrevistas.Add(new Entrevista
                {
                    DataEntrevista = DateTime.ParseExact("2018/04/20 13:34:00 ", "yyyy/MM/dd HH:mm:ss",
                CultureInfo.InvariantCulture),
                    CandidaturaId = 1, EstadoId=4
                });*/
                context.Entrevistas.Add(new Entrevista { DataEntrevista = new DateTime(2017,12,06,16,12,00),CandidaturaId = 1,EstadoId=4});
                context.Entrevistas.Add(new Entrevista { DataEntrevista = new DateTime(2017,12,27,09,45,34), CandidaturaId = 3,EstadoId=5});
                context.Entrevistas.Add(new Entrevista { DataEntrevista = new DateTime(2018, 01, 29,14,30,25), CandidaturaId = 5,EstadoId=1});
                context.Entrevistas.Add(new Entrevista { DataEntrevista = new DateTime(2018,02,14,10,00,56,33), CandidaturaId = 4,EstadoId=2});
                context.Entrevistas.Add(new Entrevista { DataEntrevista = DateTime.Now, CandidaturaId = 2,EstadoId=3});
                context.SaveChanges();
        } 
        }
    }
}


