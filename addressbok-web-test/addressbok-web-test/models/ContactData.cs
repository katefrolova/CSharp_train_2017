using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData: IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allData;
        // private string allEmails;
        public ContactData()
        {
        }
        public ContactData(string firstname, string secondname)
        {
            Firstname = firstname;
            Secondname = secondname;
        }


        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Secondname == other.Secondname && Firstname == other.Firstname;
        }

        //public int CompareTo(ContactData other)
        //{
        //    if (Object.ReferenceEquals(other, null))
        //    {
        //        return 1;
        //    }
        //    if (Object.ReferenceEquals(this.Secondname, other.Secondname) == true)
        //    {
        //        return Firstname.CompareTo(other.Firstname);
        //    }
        //    return Secondname.CompareTo(other.Secondname);
        //    //return Firstname.CompareTo(other.Firstname) & Secondname.CompareTo(other.Secondname);
        //}

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            string fs = Firstname + Secondname;
            string fsRef = other.Firstname + other.Secondname;
            return fs.CompareTo(fsRef);
        }

        public override string ToString()
        {
            return "firstname=" + Firstname + " " + "\nsecondname=" + Secondname;
        }

        public override int GetHashCode()
        {
            // return 0;
            return Firstname.GetHashCode() & Secondname.GetHashCode();
        }

        [Column(Name = "id"),PrimaryKey]
        public string Id { get; set; }
        [Column(Name = "firstname")]
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        [Column(Name = "lastname")]
        public string Secondname { get; set; }

        public string Nickname { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public string Fax { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Homepage { get; set; }
        public string Address2 { get; set; }
        public string Phone2 { get; set; }
        public string Notes2 { get; set; }
        public string AllPhones
        {
            get
            {
                if (allPhones != null )
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }

            set
            {
                allPhones = value;
            }
        }

        private string CleanUp(string phone)
        {
            if(phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[- ()]", "") + "\r\n";
            //phone.Replace(" ", "").Replace("-", "").Replace("(","").Replace(")","") + "\r\n";
        }

        public string AllData
        {
            get
            {
                if (allData != null)
                {
                    return allData;
                }
                else
                {
                    //return (Firstname + " " + Secondname);
                    return (Firstname + " " + Secondname + "\r\n"
                        + Address + "\r\n"
                        + "\r\n"
                        + "H: " + HomePhone + "\r\n"
                        + "M: " + MobilePhone + "\r\n"
                        + "W: " + WorkPhone + "\r\n"
                        + "\r\n"
                        + Email + "\r\n"
                        + Email2 + "\r\n"
                        + Email3);
                }
            }
            set
            {
                allData = value;
            }
        }


        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00")  select c).ToList();
            }
        }

    }
}
