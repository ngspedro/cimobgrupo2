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

namespace cimobgrupo2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly List<Ajuda> _ajudas;
        private readonly List<Erro> _erros;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _ajudas = context.Ajudas.Where(ai => ai.Controller == "Account").ToList();
            _erros = context.Erros.ToList();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            SetHelpTooltipsLogin();
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

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
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            SetHelpTooltipsRegisto();
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

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
                    UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);


                    TempData["Message"] = "Conta criada com sucesso. Note que esta tem que ser ativada no email antes de poder ser utilizada.";
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToAction("Login");
                } else
                {
                    SetErrorMessage("004");
                    AddErrors(result);
                }
            } else
                SetErrorMessage("003");

            SetHelpTooltipsRegisto();
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Login", "Account");
        }

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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            SetHelpTooltipsForgotPassword();
            return View();
        }

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
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

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
            var model = new ResetPasswordViewModel { UserId = userId, Code = code };
            return View(model);
        }

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
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            else
                SetErrorMessage("005");

            SetHelpTooltipsResetPassword();
            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        private void SetErrorMessage(String Code)
        {
            var Erro = _erros.SingleOrDefault(e => e.Codigo == Code);
            ViewData["Error_Code"] = Erro.Codigo;
            ViewData["Error_Message"] = Erro.Mensagem;
        }

        #region SetHelpToolTips
        private void SetHelpTooltipsLogin()
        {
            ViewData["Username"] = _ajudas.Single(ai => ai.Action == "Login" && ai.Elemento == "Username").Texto;
            ViewData["Password"] = _ajudas.Single(ai => ai.Action == "Login" && ai.Elemento == "Password").Texto;
            ViewData["RememberMe"] = _ajudas.Single(ai => ai.Action == "Login" && ai.Elemento == "RememberMe").Texto;
        }

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

        private void SetHelpTooltipsForgotPassword()
        {
            ViewData["Email"] = _ajudas.Single(ai => ai.Action == "ForgotPassword" && ai.Elemento == "Email").Texto;
        }

        private void SetHelpTooltipsResetPassword()
        {
            ViewData["Password"] = _ajudas.Single(ai => ai.Action == "ResetPassword" && ai.Elemento == "Password").Texto;
            ViewData["ConfirmarPassword"] = _ajudas.Single(ai => ai.Action == "ResetPassword" && ai.Elemento == "ConfirmarPassword").Texto;
        }
        #endregion

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

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
