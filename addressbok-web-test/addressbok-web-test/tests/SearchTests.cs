using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class SearchTests: AuthTestBase
    {
        [Test]
        public void TestSearch()
        {
            //  System.Console.Out.Write(app.Contacts.GetNumberOfResults("p"));
            int countNumber = app.Contacts.GetNumberOfResults("p");
            int trNumber = app.Contacts.GetNumberOfContactsSearch();
            Assert.AreEqual(countNumber, trNumber);
        }
    }
}
