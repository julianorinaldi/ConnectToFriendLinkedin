using CoreApp;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace ConnectToFriendLinkedinLibrary
{
    public class ConnectToFriendLinkedin : IDisposable
    {
        private IWebDriver driver;
        private readonly IOutput _output;

        public ConnectToFriendLinkedin(IOutput output)
        {
            System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", @"chromedriver.exe");
            driver = new ChromeDriver();
            _output = output;
        }

        public bool Login(string userEmail, string passwordKey)
        {
            try
            {
                driver.Navigate().GoToUrl("https://www.linkedin.com");
                var username = driver.FindElement(By.Id("session_key"));
                username.SendKeys(userEmail);
                var password = driver.FindElement(By.Id("session_password"));
                password.SendKeys(passwordKey);
                var login = driver.FindElement(By.ClassName("sign-in-form__submit-button"));
                login.Click();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void RunConnect(string url, string messagemToConnect)
        {
            try
            {
                driver.Navigate().GoToUrl(url);
                driver.FindElement(By.CssSelector("button[class*='pv-s-profile-actions pv-s-profile-actions--connect ml2 artdeco-button artdeco-button--2 artdeco-button--primary ember-view']")).Click();
                driver.FindElement(By.CssSelector("button[class*='mr1 artdeco-button artdeco-button--muted artdeco-button--3 artdeco-button--secondary ember-view']")).Click();
                driver.FindElement(By.Id("custom-message")).Click();
                driver.FindElement(By.Id("custom-message")).Clear();
                driver.FindElement(By.Id("custom-message")).SendKeys(messagemToConnect);
                driver.FindElement(By.CssSelector("button[class*='ml1 artdeco-button artdeco-button--3 artdeco-button--primary ember-view']")).Click();
            }
            catch (Exception e)
            {
                _output.WriteText($"Não foi possível se conectar com {url}.\tErro {e.Message}");
                _output.WriteText(Environment.NewLine);
            }
        }

        public void Dispose()
        {
            if (driver != null)
                driver.Quit();
        }
    }
}