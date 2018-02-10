using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;

namespace Sprint3Testes
{
    public class Entrevistas
    {
        private static String CAMINHO = @"C:\Users\Costeira\Documents\CIMOBgrupo2\eswt4g2\Sprint3Testes\bin\Debug\netcoreapp2.0";
        private FirefoxDriver driver;

        public void CommonCode()
        {
            driver = new FirefoxDriver(CAMINHO);
            string url = "http://eswt4g2.azurewebsites.net/Account/Login";
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            driver.FindElement(By.Id("Username")).SendKeys("testecimob");
            driver.FindElement(By.Id("Password")).SendKeys("@Abc123");
            driver.FindElement(By.Id("btnLogin")).Click();
            driver.FindElement(By.Id("lnkEntrevistas")).Click();
        }
        [Fact]
        public void VisualizarEntrevistas()
        {
                try
                {

                    CommonCode();
                    Assert.NotNull(driver.FindElement(By.Id("tabela-entrevistas")));
                    driver.Close();
                    driver.Dispose();
                }
                finally
                {
                    driver.Quit();
                }

            }

        [Fact]
        public void MarcarEntrevista()
        {
            try
            {
                CommonCode();
                driver.FindElement(By.Id("btnMarcarEntrevista")).Click();
                driver.FindElement(By.Name("CandidaturaId")).SendKeys("Daniel");
                driver.FindElement(By.Id("DataPicker")).SendKeys("28/02/2018");
                driver.FindElement(By.Id("txtHora")).SendKeys("14:30");
                driver.FindElement(By.Id("txtLocal")).SendKeys("Est Setubal");
                driver.FindElement(By.Id("btnConfirmar")).Click();
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
        public void EditarEntrevista()
        {
            try
            {
                CommonCode();
                driver.FindElement(By.Id("btnMarcarEntrevista")).Click();
                driver.FindElement(By.Name("CandidaturaId")).SendKeys("Daniel");
                driver.FindElement(By.Id("DataPicker")).SendKeys("28/02/2018");
                driver.FindElement(By.Id("txtHora")).SendKeys("14:30");
                driver.FindElement(By.Id("txtLocal")).SendKeys("Est Setubal");
                driver.FindElement(By.Id("btnConfirmar")).Click();
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
        public void AvaliarEntrevista()
        {
            try
            {
                CommonCode();
                driver.FindElement(By.Id("AvaliarModal")).Click();
                driver.FindElement(By.Id("Pontuacao")).SendKeys("5");
                driver.FindElement(By.Name("Comentarios")).SendKeys("Isto é um teste!");
                driver.FindElement(By.Id("btnConfirmar")).Click();
                Assert.NotNull(driver.FindElement(By.ClassName("alert-success")));
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
