using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactTestBase : AuthTestBase
    {
        public bool IsCompareUiWithDb = true;
        [TearDown]
        public void CompareGroupsFromUiWithDB()
        {
            if (!IsCompareUiWithDb) return;
            var fromUi = app.Contacts.GetContactList();
            var fromDb = ContactData.GetAll();
            fromUi.Sort();
            fromDb.Sort();
            Assert.AreEqual(fromUi, fromDb);
        }
    }
}
