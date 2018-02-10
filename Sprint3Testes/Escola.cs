using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sprint3Testes
{
    public class Escola
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
            driver.FindElement(By.Id("lnkEscolas")).Click(); 
        }

        [Fact]
        public void VisualizarEscola()
        {
            try
            {

                CommonCode();
                Assert.NotNull(driver.FindElement(By.Id("tabela-escolas")));
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }

        }

        [Fact]
        public void NovaEscola()
        {
            try
            {
                CommonCode();
                driver.FindElement(By.Id("btnCriar")).Click();
                driver.FindElement(By.Id("Nome")).SendKeys("Engenharia de Instrumentação");
                driver.FindElement(By.Id("Pais")).SendKeys("Portugal");
                driver.FindElement(By.Id("Localidade")).SendKeys("Setúbal");
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
        public void EditarEscola()
        {
            try
            {
                CommonCode();
                driver.FindElement(By.Id("Editar")).Click();
                driver.FindElement(By.Id("Nome")).SendKeys("Teste");
                driver.FindElement(By.Id("Pais")).SendKeys("Poland");
                driver.FindElement(By.Id("Localidade")).SendKeys("sosnowiec");
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
        public void EliminarEscola()
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

        [Fact]
        public void DetalhesEscola()
        {
            try
            {
                CommonCode();
                driver.FindElement(By.Id("DesmarcarModal")).Click();
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
