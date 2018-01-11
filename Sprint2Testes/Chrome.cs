using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace Sprint2Testes
{
    public class Chrome
    {
        private static String CAMINHO = @"C:\Users\nuno\Desktop\ESW\PROJETO\CIMOBgrupo2\Sprint1Testes\bin\Debug\netcoreapp2.0";
        private ChromeDriver driver;

        [Fact]
        public void VisualizarCandidaturas()
        {
            try
            {
                driver = new ChromeDriver(CAMINHO);
                string url = "http://eswt4g2.azurewebsites.net/Account/Login";
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("Username")).SendKeys("testecimob");
                driver.FindElement(By.Id("Password")).SendKeys("@Abc123");
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
    }
}
