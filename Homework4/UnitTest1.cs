using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Interactions;
using System.Linq;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace Homework4
{
    [TestClass]
    public class UnitTest1
    {
        public IWebDriver driver;
        public Actions actions;
        public WebDriverWait wait;
        

        [TestInitialize]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 15));
            actions = new Actions(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(5000);
            wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException), typeof(NoSuchElementException), typeof(ElementNotVisibleException), typeof(ElementNotSelectableException), typeof(ElementClickInterceptedException));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.phptravels.net/");
        }

        [TestMethod]
        public void Homework4()
        {
            var myDate = DateTime.Now;
            string currentDay = myDate.ToString(format: "d MM yyyy");
            var futureDay = DateTime.Now.AddDays(7);

            IWebElement bttn = wait.Until(drv => driver.FindElement(By.XPath("//*[@id='cookyGotItBtn']")));
            bttn.Click();

            IWebElement hotelbox = wait.Until(drv => driver.FindElement(By.XPath("//div[@id='s2id_location']//a[@class='select2-choice select2-default']")));
            hotelbox.Click();
            hotelbox.SendKeys("Tamp");
            //driver.FindElement(By.XPath("//div[@id='s2id_location']//a[@class='select2-choice select2-default']")).SendKeys("Tamp");

            Thread.Sleep(2000);
            IWebElement city = wait.Until(drv =>driver.FindElement(By.XPath("//div[contains(text(),'a, United States')]")));
            actions.MoveToElement(city).Perform();
            city.Click();



            IWebElement checkIn = wait.Until(drv => driver.FindElement(By.XPath("//div[@id='dpd1']//input[@placeholder='Check in']")));
            checkIn.SendKeys(currentDay);
            checkIn.Click();

            IWebElement checkOut = wait.Until(drv => driver.FindElement(By.XPath("//input[@name='checkout']")));
            checkOut.SendKeys(futureDay.ToString("d MM yyyy"));
            checkOut.Click();

            IWebElement submit = wait.Until(drv => driver.FindElement(By.XPath("//button[@class='btn btn-lg btn-block btn-primary pfb0 loader']")));
            submit.Click();

            IList<IWebElement> hotelList = driver.FindElements(By.XPath("//table[@id='listing']"));
            Assert.IsNotNull(hotelList);
            
        }




        [TestCleanup]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();

        }

 

    }
}
