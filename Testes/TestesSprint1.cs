using cimobgrupo2.Controllers;
using Xunit;
using cimobgrupo2.Models;
using cimobgrupo2.Data;
using Microsoft.Extensions.Logging;
using cimobgrupo2.Services;
using Microsoft.AspNetCore.Identity;
using cimobgrupo2.Models.ManageViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestesSprint1
{
    public class TestesSprint1
    {
        private AccountController _accountController;
        private ManageController _manageController;

        public TestesSprint1()
        {
            var user = new ApplicationUser
            {
                Nome = "testeNome",
                DataNascimento = "01/01/2000",
                Email = "teste@teste.com",
                Contato = "123456789",
                UserName = "teste"
            };


            var userManager = new Mock<UserManager<ApplicationUser>>();
            userManager.Setup(um => um.CreateAsync(user, "teste123"));
                
            var signInManager = new Mock<SignInManager<ApplicationUser>>();
            signInManager.Setup(sim => sim.SignInAsync(user, false, null));

            var logger = new Mock<ILogger<ManageController>>();
            var applicationDbContext = new Mock<ApplicationDbContext>();

            //_accountController = new AccountController();
            _manageController = new ManageController(userManager.Object, signInManager.Object, logger.Object, applicationDbContext.Object);
        }

        [Fact]
        public void DeleteAccountFailed()
        {
            IndexViewModel indexViewModel = new IndexViewModel
            {
                DeleteAccount = new DeleteAccountViewModel()
            };

            indexViewModel.DeleteAccount.Password = "passwordAtual";

            var result = _manageController.DeleteAccount(indexViewModel).Result as ViewResult;

            Assert.Equal("Index", result.ViewName);
        }
    }
}

