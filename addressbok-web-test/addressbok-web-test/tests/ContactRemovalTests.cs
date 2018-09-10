using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contacts.NoContactsCreated();
            //List<ContactData> oldContacts = ContactData.GetAll();
            var oldContacts = ContactData.GetAll();
            var contactToRemove = oldContacts[0];
            app.Contacts.ContactDelete(contactToRemove);

            //app.Contacts.ContactDelete(0);

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
            app.Navigator.GoToHomePage();
        }
    }
}
