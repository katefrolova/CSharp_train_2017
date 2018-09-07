using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            app.Contacts.NoContactsCreated();
            ContactData contact = new ContactData("d", "dd");
            contact.Middlename = null;

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.ContactModify(1, contact);

            Assert.AreEqual(oldContacts.Count , app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[0].Secondname = contact.Secondname;
            oldContacts[0].Firstname = contact.Firstname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
