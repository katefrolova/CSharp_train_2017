using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) 
            :base(manager)
        {
           // this.driver = driver;
        }


        public ContactHelper CreateContact(ContactData contact)
        {
           InitContactsCreation();
           FillContactForm(contact);
           SubmitContactCreation();
           ReturnToHomePage();
           return this;
        }

        public int GetContactCount()
        {
            //return driver.FindElements(By.XPath(".//*[@id='maintable']/tbody/tr/td[3]")).Count;
            return driver.FindElements(By.Name("entry")).Count;
        }
        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                // List<ContactData> contact = new List<ContactData>();
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr"));
                int row = 0;
                foreach (IWebElement element in elements)
                {
                    if (row > 0)
                    {
                        string first = element.FindElement(By.XPath("td[3]")).Text;
                        string second = element.FindElement(By.XPath("td[2]")).Text;
                        contactCache.Add(new ContactData(first, second));
                    }
                    row++;
                }
            }
                //return contact;
                return new List<ContactData>(contactCache);
            
        }

        public ContactHelper DriverAlert()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper NoContactsCreated()
        {
            if (!IsElementPresent(By.Name("selected[]")))
            {
                CreateContact(new ContactData("tt", "zz"));
            }
            return this;
        }

        public ContactHelper ContactDelete(int l)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(l);
            RemoveContact();
            DriverAlert();
            return this;
        }

        public ContactHelper ContactModify(int p, ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(p);
            FillContactForm(contact);
            SubmitContactModification();
            manager.Navigator.GoToHomePage();
            return this;
        }


        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Secondname);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("photo"), contact.Photo);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("fax"), contact.Fax);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.Homepage);
            Type(By.Name("address2"), contact.Address2);
            Type(By.Name("phone2"), contact.Phone2);
            Type(By.Name("notes"), contact.Notes2);
            return this;
        }

        public ContactHelper InitContactsCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            contactCache = null;
            return this;
        }


        public ContactHelper SelectContact(int v)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (v+1) + "]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int t)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + t + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            return this;
        }
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
            return this;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();

            IList<IWebElement> cells = 
                driver.FindElements(By.Name("entry"))[index]
                      .FindElements(By.TagName("td"));

            string lastName  = cells[1].Text;
            string firstName = cells[2].Text;
            string address   = cells[3].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModificationB(0);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };
        }

        public void InitContactModificationB(int index)
        {
            //сначала нашлась нужная строка, потом взяли по индексу
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
        }

        //public int GetNumberOfResults()
        //{
        //    manager.Navigator.GoToHomePage();
        //    string text = driver.FindElement(By.TagName("label")).Text;
        //    Match m = new Regex(@"\d+").Match(text);
        //    return Int32.Parse(m.Value);

        //}

        public int GetNumberOfResults(string searchString)
        {
            manager.Navigator.GoToHomePage();
            //string text = driver.FindElement(By.TagName("label")).Text;
            //Match m = new Regex(@"\d+").Match(text);
            driver.FindElement(By.Name("searchstring")).SendKeys(searchString);
            string text = driver.FindElement(By.Id("search_count")).Text;
            return Int32.Parse(text);
           // return Int32.Parse(m.Value);

        }

        public int GetNumberOfContactsSearch()
        {
            int countContacts = GetContactCount();
            int countHidedContacts = driver.FindElement(By.CssSelector("#maintable")).FindElements(By.CssSelector("tr[style]")).Count;
            return countContacts - countHidedContacts;
        }

        public string GetContactInformationFromProperty(int index)
        {
            manager.Navigator.GoToHomePage();
            driver.FindElement(By.XPath("(//img[@alt='Details'])[" + (index + 1) + "]")).Click();
            return driver.FindElement(By.Id("content")).Text;
        }


    }
}
