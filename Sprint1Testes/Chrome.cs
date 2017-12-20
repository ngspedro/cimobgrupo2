using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace Sprint1Testes
{
    public class Chrome
    {
        private static String CAMINHO = @"C:\Users\nuno\Desktop\ESW\PROJETO\CIMOBgrupo2\Sprint1Testes\bin\Debug\netcoreapp2.0";
        private ChromeDriver driver;

        [Fact]
        public void Login()
        {
            try
            {
                driver = new ChromeDriver(CAMINHO);
                string url = "http://eswt4g2.azurewebsites.net/Account/Login";
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("Username")).SendKeys("ngspedro");
                driver.FindElement(By.Id("Password")).SendKeys("abc123");
                driver.FindElement(By.Id("btnLogin")).Click();

                Assert.NotNull(driver.FindElement(By.Id("conteudo")));
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }

        }

        [Fact]
        public void LoginCredenciaisInvalidas()
        {
            try
            {
                driver = new ChromeDriver(CAMINHO);
                string url = "http://eswt4g2.azurewebsites.net/Account/Login";
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("Username")).SendKeys("contanaoexistente");
                driver.FindElement(By.Id("Password")).SendKeys("contanaoexistente");
                driver.FindElement(By.Id("btnLogin")).Click();

                Assert.NotNull(driver.FindElement(By.ClassName("alert-danger")));
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }

        }

        [Fact]
        public void Registo()
        {
            try
            {
                driver = new ChromeDriver(CAMINHO);
                string url = "http://eswt4g2.azurewebsites.net/Account/Register";
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("Nome")).SendKeys("Selenium");
                driver.FindElement(By.Id("DataNascimentoPicker")).SendKeys("01/01/1990");
                driver.FindElement(By.Id("Email")).SendKeys("selenium@selenium.com");
                driver.FindElement(By.Id("Contato")).SendKeys("911234567");
                driver.FindElement(By.Id("Username")).SendKeys("selenium");
                driver.FindElement(By.Id("Password")).SendKeys("selenium");
                driver.FindElement(By.Id("ConfirmarPassword")).SendKeys("selenium");
                driver.FindElement(By.Id("btnRegisto")).Click();

                Assert.NotNull(driver.FindElement(By.ClassName("alert-success")));
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }

        }

        [Fact]
        public void RegistoNotOver17()
        {
            try
            {
                driver = new ChromeDriver(CAMINHO);
                string url = "http://eswt4g2.azurewebsites.net/Account/Register";
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("Nome")).SendKeys("SeleniumNotOver17");
                driver.FindElement(By.Id("DataNascimentoPicker")).SendKeys("01/01/2017");
                driver.FindElement(By.Id("Email")).SendKeys("selenium@selenium.com");
                driver.FindElement(By.Id("Contato")).SendKeys("911234567");
                driver.FindElement(By.Id("Username")).SendKeys("selenium");
                driver.FindElement(By.Id("Password")).SendKeys("selenium");
                driver.FindElement(By.Id("ConfirmarPassword")).SendKeys("selenium");
                driver.FindElement(By.Id("btnRegisto")).Click();

                Assert.NotNull(driver.FindElement(By.ClassName("alert-danger")));
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }

        }

        [Fact]
        public void ChangeAccountDetails()
        {
            try
            {
                driver = new ChromeDriver(CAMINHO);
                string url = "http://eswt4g2.azurewebsites.net/Account/Login";
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("Username")).SendKeys("ngspedro");
                driver.FindElement(By.Id("Password")).SendKeys("abc123");

                driver.FindElement(By.Id("btnLogin")).Click();

                driver.FindElement(By.Id("btnDefinicoes")).Click();

                driver.FindElement(By.Id("Nome")).Clear();
                driver.FindElement(By.Id("Nome")).SendKeys("changeDetailsTest");

                driver.FindElement(By.Id("DataNascimentoPicker")).Clear();
                driver.FindElement(By.Id("DataNascimentoPicker")).SendKeys("11/01/2000");

                driver.FindElement(By.Id("btnChangeDetails")).Click();

                Assert.NotNull(driver.FindElement(By.ClassName("alert-success")));

                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }

        }

        [Fact]
        public void ChangeAccountPassword()
        {
            try
            {
                driver = new ChromeDriver(CAMINHO);
                string url = "http://eswt4g2.azurewebsites.net/Account/Login";
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("Username")).SendKeys("ngspedro");
                driver.FindElement(By.Id("Password")).SendKeys("abc123");

                driver.FindElement(By.Id("btnLogin")).Click();

                driver.FindElement(By.Id("btnDefinicoes")).Click();

                driver.FindElement(By.Id("PasswordAntiga")).SendKeys("abc1238");
                driver.FindElement(By.Id("NovaPassword")).SendKeys("abc123");
                driver.FindElement(By.Id("ConfirmarNovaPassword")).SendKeys("abc123");

                driver.FindElement(By.Id("btnChangePassword")).Click();

                Assert.NotNull(driver.FindElement(By.ClassName("alert-success")));

                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }

        }

        [Fact]
        public void DeleteAccount()
        {
            try
            {
                driver = new ChromeDriver(CAMINHO);
                string url = "http://eswt4g2.azurewebsites.net/Account/Login";
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("Username")).SendKeys("ngspedro");
                driver.FindElement(By.Id("Password")).SendKeys("abc123");

                driver.FindElement(By.Id("btnLogin")).Click();

                driver.FindElement(By.Id("btnDefinicoes")).Click();

                driver.FindElement(By.Id("PasswordAtual")).SendKeys("abc123");

                driver.FindElement(By.Id("btnDeleteAccount")).Click();

                Assert.NotNull(driver.FindElement(By.ClassName("alert-success")));

                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }
        }

        [Fact]
        public void DeleteAccountPasswordErrada()
        {
            try
            {
                driver = new ChromeDriver(CAMINHO);
                string url = "http://eswt4g2.azurewebsites.net/Account/Login";
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("Username")).SendKeys("ngspedro");
                driver.FindElement(By.Id("Password")).SendKeys("abc123");

                driver.FindElement(By.Id("btnLogin")).Click();

                driver.FindElement(By.Id("btnDefinicoes")).Click();

                driver.FindElement(By.Id("PasswordAtual")).SendKeys("PASSWORDERRADA");

                driver.FindElement(By.Id("btnDeleteAccount")).Click();

                Assert.NotNull(driver.FindElement(By.ClassName("alert-danger")));

                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
