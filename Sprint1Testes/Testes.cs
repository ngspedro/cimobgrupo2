using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace Sprint1Testes
{
    public class Testes
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [Fact]
        public void LoginChrome()
        {
            try
            {
                driver = new ChromeDriver(@"C:\Users\Costeira\Documents\CIMOBgrupo2\Sprint1Testes\bin\Debug\netcoreapp2.0");
                string url = "http://eswt4g2.azurewebsites.net/Account/Login";
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("Username")).SendKeys("ngspedro");
                driver.FindElement(By.Id("Password")).SendKeys("abc123");
                driver.FindElement(By.TagName("button")).Click();
                wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
                wait.Until(wt => wt.FindElement(By.Id("conteudo")));
                driver.Close();
                driver.Dispose();
            }
            catch
            {
                driver.Quit();
            }
        }
 
        [Fact]
        public void LoginFirefox()
        {
            try
            {
                driver = new FirefoxDriver(@"C:\Users\Costeira\Documents\CIMOBgrupo2\Sprint1Testes\bin\Debug\netcoreapp2.0");
                string url = "http://eswt4g2.azurewebsites.net/Account/Login";
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("Username")).SendKeys("ngspedro");
                driver.FindElement(By.Id("Password")).SendKeys("abc123");
                driver.FindElement(By.TagName("button")).Click();
                wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
                wait.Until(wt => wt.FindElement(By.Id("conteudo")));
                driver.Close();
                driver.Dispose();
            }
            catch
            {
                driver.Quit();
            }
        }

    }
}
