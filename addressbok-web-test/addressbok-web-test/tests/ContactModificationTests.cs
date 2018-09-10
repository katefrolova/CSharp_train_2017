using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            app.Contacts.NoContactsCreated();
            ContactData contact = new ContactData("d", "dd");
            contact.Middlename = null;

            // List<ContactData> oldContacts = app.Contacts.GetContactList();
            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData contactToModify = oldContacts[0];

            // app.Contacts.ContactModify(1, contact);
            app.Contacts.ContactModify(contactToModify, contact);

            Assert.AreEqual(oldContacts.Count , app.Contacts.GetContactCount());

            //List<ContactData> newContacts = app.Contacts.GetContactList();
            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[0].Secondname = contact.Secondname;
            oldContacts[0].Firstname = contact.Firstname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
