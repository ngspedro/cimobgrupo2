using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using cimobgrupo2.Models;
using cimobgrupo2.Models.ManageViewModels;
using cimobgrupo2.Services;

namespace cimobgrupo2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;

        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IEmailSender emailSender,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel();

            model.ChangeDetails = new ChangeDetailsViewModel
            {
                Nome = user.Nome,
                DataNascimento = user.DataNascimento,
                Email = user.Email,
                Contato = user.Contato
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeDetails(IndexViewModel model)
        {
            TempData["result"] = "error";
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (model.ChangeDetails.Email != null)
                user.Email = model.ChangeDetails.Email;

            if (model.ChangeDetails.Nome != null)
                user.Nome = model.ChangeDetails.Nome;

            if (model.ChangeDetails.DataNascimento != null)
                user.DataNascimento = model.ChangeDetails.DataNascimento;

            if(model.ChangeDetails.Contato != null)
                user.Contato = model.ChangeDetails.Contato;

            await _userManager.UpdateAsync(user);

            TempData["result"] = "success";
            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(IndexViewModel model)
        {
            TempData["result"] = "error";
            var user = await _userManager.GetUserAsync(User);
            model.ChangeDetails = new ChangeDetailsViewModel
            {
                Nome = user.Nome,
                DataNascimento = user.DataNascimento,
                Email = user.Email,
                Contato = user.Contato
            };


            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.ChangePassword.OldPassword, model.ChangePassword.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View("Index", model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            TempData["result"] = "success";
            return RedirectToAction(nameof(Index));
        }

       
        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion
    }
}
