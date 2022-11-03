using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
//using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;

namespace SeleniumExplicitWaits
{
    [TestClass]
    public class SeleniumWebDriverWaits
    {
        private static IWebDriver driver;
        public static string gridURL = "@hub.lambdatest.com/wd/hub";
        public static string LT_USERNAME = Environment.GetEnvironmentVariable("LT_USERNAME");
        public static string LT_ACCESS_KEY = Environment.GetEnvironmentVariable("LT_ACCESS_KEY");

        [TestInitialize]
        public void SetUp()
        {
            var desiredCapabilities = new DesiredCapabilities();
            desiredCapabilities.SetCapability("browserName", "Chrome");
            desiredCapabilities.SetCapability("platform", "Windows 11");
            desiredCapabilities.SetCapability("version", "101.0");
            desiredCapabilities.SetCapability("screenResolution", "1280x800");
            desiredCapabilities.SetCapability("user", LT_USERNAME);
            desiredCapabilities.SetCapability("accessKey", LT_ACCESS_KEY);
            desiredCapabilities.SetCapability("build", "Selenium C-Sharp");
            desiredCapabilities.SetCapability("name", "Selenium Test");
            driver = new RemoteWebDriver(new Uri($"https://{LT_USERNAME}:{LT_ACCESS_KEY}{gridURL}"), desiredCapabilities, TimeSpan.FromSeconds(600));

        }

        [TestMethod]
        public void DownloadFile()
        {
            driver.Navigate().GoToUrl("https://www.lambdatest.com/selenium-playground/jquery-download-progress-bar-demo");
            driver.FindElement(By.Id("downloadButton")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Close') and @class='ui-button ui-corner-all ui-widget']"))).Click();
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }
    }
}
