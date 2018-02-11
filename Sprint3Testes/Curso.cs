using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sprint3Testes
{
    public class Curso
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
            driver.FindElement(By.Id("lnkCursos")).Click();
        }

        [Fact]
        public void VisualizarCursos()
        {
            try
            {

                CommonCode();
                Assert.NotNull(driver.FindElement(By.Id("tabela-cursos")));
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }

        }

        [Fact]
        public void NovoCurso()
        {
            try
            {
                CommonCode();
                driver.FindElement(By.Id("btnCriar")).Click();
                driver.FindElement(By.Id("txtNome")).SendKeys("Engenharia de Instrumentação");
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
        public void EditarCurso()
        {
            try
            {
                CommonCode();
                driver.FindElement(By.Id("Editar")).Click();
                driver.FindElement(By.Id("txtNome")).Clear();
                driver.FindElement(By.Id("txtNome")).SendKeys("Teste");
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
        public void EliminarCurso()
        {
            try
            {
                CommonCode();
                driver.FindElement(By.Id("Remover")).Click();
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
