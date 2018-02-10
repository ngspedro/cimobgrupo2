using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using cimobgrupo2.Models;
using cimobgrupo2.Models.AccountViewModels;
using cimobgrupo2.Services;
using cimobgrupo2.Data;
using Microsoft.Extensions.FileProviders;
using cimobgrupo2.Extensions;

namespace cimobgrupo2.Controllers
{
    /// <summary>Controlador para contas de acesso ao sistema</summary>
    /// <remarks>Extende de BaseController</remarks>
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : BaseController
    {
        /// <summary>Atributo para o User Manager</summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>Atributo para o SignIn Manager</summary>
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>Atributo para o Email Sender</summary>
        private readonly IEmailSender _emailSender;

        /// <summary>Atributo para o Logger</summary>
        private readonly ILogger _logger;

        /// <summary>Construtor com parametros - AccountController</summary>
        /// <param name="userManager">User Manager</param>
        /// <param name="signInManager">SignIn Manager</param>
        /// <param name="emailSender">Email Sender</param>
        /// <param name="logger">Logger</param>
        /// <param name="context">Context da Bd</param>
        /// <param name="fileProvider">File Provider</param>
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            ApplicationDbContext context,
             IFileProvider fileProvider) : base(context, fileProvider, "Account")
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        /// <summary>Action que prepara e mostra a view para fazer login</summary>
        /// <param name="returnUrl">Return Url</param>
        /// <returns>View para fazer login</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            SetHelpTooltipsLogin();
            SetHelpModal("Login");
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>Action que trata do processo de login e redirecciona para a área pessoal (sessão iniciada)</summary>
        /// <param name="model">LoginViewModel preenchido com os dados submetidos no formulário de login</param>
        /// <param name="returnUrl">Return Url</param>
        /// <returns>View da área pessoal se o login foi feito com sucesso. (Caso contrário fica na mesma página)</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                else
                    SetErrorMessage("001");
            }
            else
                SetErrorMessage("002");

            SetHelpTooltipsLogin();
            SetHelpModal("Login");
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>Action que prepara e mostra a view para fazer registo</summary>
        /// <param name="returnUrl">Return Url</param>
        /// <returns>View para fazer registo</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            SetHelpTooltipsRegisto();
            SetHelpModal("Register");
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>Action que trata do processo de registo</summary>
        /// <param name="model">RegisterViewModel preenchido com os dados submetidos no formulário de registo</param>
        /// <param name="returnUrl">Return Url</param>
        /// <returns>Redireciona para a acção de login com a mensagem de que a conta tem que ser ativada, caso o processo de registo seja bem sucedido.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    Nome = model.Nome,
                    DataNascimento = model.DataNascimento,
                    Email = model.Email,
                    Contato = model.Contato,
                    UserName = model.Username,
                    PasswordHashAux = PasswordHashExtensions.Encode(model.Password)
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                   var newUserRole = await _userManager.AddToRoleAsync(user, "Estudante");

                    
                    SetSuccessMessage("Conta criada com sucesso. Note que esta tem que ser ativada no email antes de poder ser utilizada.");
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToAction("Login");
                } else
                {
                    if (result.Errors.Where(r => r.Code == "DuplicateUsername").ToList().Count != 0){ 
                        SetErrorMessage("004");
                    } else
                    {
                        SetErrorMessage("003");
                    }
                    AddErrors(result);
                }
            } else
                SetErrorMessage("003");

            SetHelpTooltipsRegisto();
            SetHelpModal("Register");
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>Action que trata do processo de logout</summary>
        /// <returns>Redireciona para a acção de login</returns>
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Login", "Account");
        }

        /// <summary>Action que trata do processo de confirmação de email</summary>
        /// <param name="userId">Id do utilizador a ativar a conta</param>
        /// <param name="code">Código de confirmação (usado para verificar se a ativação é legitima)</param>
        /// <returns>View com resultado da ativação</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
                SetErrorMessage("005");

            return View("ConfirmEmail");
        }

        /// <summary>Action que prepara e mostra a view para inserção do email cuja conta se pretende recuperar</summary>
        /// <returns>View para fazer pedido de recuperação</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            SetHelpTooltipsForgotPassword();
            SetHelpModal("RecoverPassword");
            return View();
        }

        /// <summary>Action que trata do processo de pedido de recuperação de conta</summary>
        /// <param name="model">ForgotPasswordViewModel preenchido com o email da conta que se pretende recuperar</param>
        /// <returns>Redirecciona para a acção ForgotPasswordConfirmation, se o processo for bem sucedido.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailForgotPasswordAsync(model.Email, callbackUrl);
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            } else
                SetErrorMessage("003");

            SetHelpTooltipsForgotPassword();
            SetHelpModal("RecoverPassword");
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>Action que prepara e mostra a view com a mensagem de que tem que ir ao email clicar no link para repor a password</summary>
        /// <returns>View com a mensagem</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        /// <summary>Action que prepara e mostra a view na qual o utilizador pode repor a sua password (ao clicar no link de reposiçao no email)</summary>
        /// <param name="userId">Id do user que está a repor a password</param>
        /// <param name="code">Codigo de reposição de password (para efeitos de validação)</param>
        /// <returns>View com o formulário de reposição de password</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string userId = null, string code = null)
        {
            if (code == null || userId == null)
            {
                //throw new ApplicationException("A code/userID must be supplied for password reset.");
                //return RedirectToAction(nameof(Login));
            }
            SetHelpTooltipsResetPassword();
            SetHelpModal("RecoverPassword");
            var model = new ResetPasswordViewModel { UserId = userId, Code = code };
            return View(model);
        }

        /// <summary>Action que trata do processo de reposição de password</summary>
        /// <param name="model">ResetPasswordViewModel preenchido com a nova password e a sua confirmação</param>
        /// <returns>Redirecciona para a acção ResetPasswordConfirmation, se o processo for bem sucedido.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMessage("003");
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            user.PasswordHashAux = PasswordHashExtensions.Encode(model.Password);
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                await _userManager.UpdateAsync(user);
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            else
                SetErrorMessage("005");

            SetHelpTooltipsResetPassword();
            SetHelpModal("RecoverPassword");
            AddErrors(result);
            return View();
        }

        /// <summary>Action que prepara e mostra a view com a mensagem de sucesso na reposição de password</summary>
        /// <returns>View com a mensagem</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #region SetHelp

        /// <summary>Método que coloca a informação nas tooltips de ajuda do login</summary>
        private void SetHelpTooltipsLogin()
        {
            ViewData["Username"] = _ajudas.Single(ai => ai.Action == "Login" && ai.Elemento == "Username").Texto;
            ViewData["Password"] = _ajudas.Single(ai => ai.Action == "Login" && ai.Elemento == "Password").Texto;
            ViewData["RememberMe"] = _ajudas.Single(ai => ai.Action == "Login" && ai.Elemento == "RememberMe").Texto;
        }

        /// <summary>Método que coloca a informação nas tooltips de ajuda do registo</summary>
        private void SetHelpTooltipsRegisto()
        {
            ViewData["Nome"] = _ajudas.Single(ai => ai.Action == "Registo" && ai.Elemento == "Nome").Texto;
            ViewData["DataNascimentoPicker"] = _ajudas.Single(ai => ai.Action == "Registo" && ai.Elemento == "DataNascimentoPicker").Texto;
            ViewData["Email"] = _ajudas.Single(ai => ai.Action == "Registo" && ai.Elemento == "Email").Texto;
            ViewData["Contato"] = _ajudas.Single(ai => ai.Action == "Registo" && ai.Elemento == "Contato").Texto;
            ViewData["Username"] = _ajudas.Single(ai => ai.Action == "Registo" && ai.Elemento == "Username").Texto;
            ViewData["Password"] = _ajudas.Single(ai => ai.Action == "Registo" && ai.Elemento == "Password").Texto;
            ViewData["ConfirmarPassword"] = _ajudas.Single(ai => ai.Action == "Registo" && ai.Elemento == "ConfirmarPassword").Texto;
        }

        /// <summary>Método que coloca a informação nas tooltips de ajuda do pedido de reposição de password</summary>
        private void SetHelpTooltipsForgotPassword()
        {
            ViewData["Email"] = _ajudas.Single(ai => ai.Action == "ForgotPassword" && ai.Elemento == "Email").Texto;
        }

        /// <summary>Método que coloca a informação nas tooltips de ajuda da reposição de password</summary>
        private void SetHelpTooltipsResetPassword()
        {
            ViewData["Password"] = _ajudas.Single(ai => ai.Action == "ResetPassword" && ai.Elemento == "Password").Texto;
            ViewData["ConfirmarPassword"] = _ajudas.Single(ai => ai.Action == "ResetPassword" && ai.Elemento == "ConfirmarPassword").Texto;
        }

        /// <summary>Método que coloca a informação no modal de ajuda</summary>
        /// <param name="Action">Acção atual (para se saber que ajuda colocar)</param>
        private void SetHelpModal(String Action)
        {
            ViewData["TextoModalAjuda"] = _ajudas.Single(ai => ai.Action == Action && ai.Elemento == "ModalAjuda").Texto;
        }
        #endregion

        #region Helpers

        /// <summary>Helper para adição de erros</summary>
        /// <param name="result">IdentityResult</param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        /// <summary>Helper para redirecionar</summary>
        /// <param name="returnUrl">Url</param>
        /// <returns>Redirecciona para o url fornecido por parametro, se válido. Caso contrário redireciona para o index do HomeController</returns>
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}