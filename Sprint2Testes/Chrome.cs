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
        public void VisualizarProgramas()
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
                driver.FindElement(By.Id("lnkProgramas")).Click();

                Assert.NotNull(driver.FindElement(By.Id("tabela-programas")));
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }
        }

        [Fact]
        public void VisualizarDetalhesPrograma()
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
                driver.FindElement(By.Id("lnkProgramas")).Click();
                driver.FindElement(By.LinkText("Detalhes")).Click();

                Assert.NotNull(driver.FindElement(By.Id("table-info")));
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }

        }

        [Fact]
        public void VisualizarDetalhesProgramaInexistente()
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
                driver.Navigate().GoToUrl("http://eswt4g2.azurewebsites.net/Programas/Detalhes/12332");

                Assert.NotNull(driver.FindElement(By.Id("tabela-programas")));
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }
        }

        [Fact]
        public void VisualizarEscolasAssociadasPrograma()
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
                driver.FindElement(By.Id("lnkProgramas")).Click();
                driver.FindElement(By.LinkText("Detalhes")).Click();

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
        public void VisualizarDocumentosAssociadosPrograma()
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
                driver.FindElement(By.Id("lnkProgramas")).Click();
                driver.FindElement(By.LinkText("Detalhes")).Click();

                Assert.NotNull(driver.FindElement(By.ClassName("glyphicon-download")));
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }
        }

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
                driver.FindElement(By.Id("lnkCandidaturas")).Click();

                Assert.NotNull(driver.FindElement(By.Id("tabela-candidaturas")));
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }
        }

        [Fact]
        public void VisualizarDocumentosAssociadosCandidatura()
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
                driver.FindElement(By.Id("lnkCandidaturas")).Click();
                driver.FindElement(By.LinkText("Detalhes")).Click();

                Assert.NotNull(driver.FindElement(By.ClassName("glyphicon-download")));
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }
        }

        [Fact]
        public void VisualizarDetalhesCandidatura()
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
                driver.FindElement(By.Id("lnkCandidaturas")).Click();
                driver.FindElement(By.LinkText("Detalhes")).Click();

                Assert.NotNull(driver.FindElement(By.Id("table-info")));
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }
        }

        [Fact]
        public void VisualizarDetalhesCandidaturaInexistente()
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
                driver.Navigate().GoToUrl("http://eswt4g2.azurewebsites.net/Candidaturas/Detalhes/12332");

                Assert.NotNull(driver.FindElement(By.Id("tabela-candidaturas")));
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }
        }

        [Fact]
        public void AceitarCandidatura()
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
                driver.FindElement(By.Id("lnkCandidaturas")).Click();
                driver.FindElement(By.LinkText("Detalhes")).Click();
                driver.FindElement(By.Id("btnAceitar")).Click();
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
        public void RecusarCandidatura()
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
                driver.FindElement(By.Id("lnkCandidaturas")).Click();
                driver.FindElement(By.LinkText("Detalhes")).Click();
                driver.FindElement(By.Id("btnRecusar")).Click();
                driver.FindElement(By.Id("txtMotivo")).SendKeys("teste recusar");
                driver.FindElement(By.Id("txtMotivo")).Submit();

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
        public void VerMotivoRejeicao()
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
                driver.FindElement(By.Id("lnkCandidaturas")).Click();
                driver.FindElement(By.LinkText("Detalhes")).Click();
                driver.FindElement(By.LinkText("+info")).Click();

                Assert.NotNull(driver.FindElement(By.Id("motivoRejeicao")));
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                driver.Quit();
            }
        }

        [Fact]
        public void PublicarResultados()
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
                driver.FindElement(By.Id("lnkCandidaturas")).Click();
                driver.FindElement(By.LinkText("Publicar Resultados")).Click();
                driver.FindElement(By.Name("programasPublicar[]")).Click();
                driver.FindElement(By.Name("programasPublicar[]")).Submit();

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
        public void FazerCandidatura()
        {
            try
            {
                driver = new ChromeDriver(CAMINHO);
                string url = "http://eswt4g2.azurewebsites.net/Account/Login";
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("Username")).SendKeys("teste");
                driver.FindElement(By.Id("Password")).SendKeys("@Abc123");
                driver.FindElement(By.Id("btnLogin")).Click();
                driver.FindElement(By.Id("lnkCandidatura")).Click();

                driver.FindElement(By.Id("EscolherPrograma")).Click();
                driver.FindElement(By.Name("ProgramaEscolhido")).Click();
                driver.FindElement(By.Name("ProgramaEscolhido")).Submit();

                driver.FindElement(By.Id("EscolherEscola")).Click();
                driver.FindElement(By.Name("EscolaEscolhida")).Click();
                driver.FindElement(By.Name("EscolaEscolhida")).Submit();

                driver.FindElement(By.Id("EscolherCurso")).Click();
                driver.FindElement(By.Name("CursoEscolhido")).Click();
                driver.FindElement(By.Name("CursoEscolhido")).Submit();
                
                driver.FindElement(By.Name("CandidaturaId")).Submit();

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
