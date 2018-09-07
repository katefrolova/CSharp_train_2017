﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) 
            //
            : base(manager)
        {
            //this.driver = driver;
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if(IsLoggedIn(account))
                {
                    return;
                }
                Logout();
            }
            Type(By.Name("user"), account.Username);
            Type(By.Name("pass"), account.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }


        public void Logout()
        {
            if(IsLoggedIn())
            {
                driver.FindElement(By.LinkText("Logout")).Click();
            }
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.Name("logout"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggetUserName() == account.Username;
        }

        public string GetLoggetUserName()
        {
              string text = driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text;
            // == "(" + account.Username + ")";
            //== System.String.Format("(&{0})", account.Username);
              return text.Substring(1, text.Length - 2);
        }

    }

}