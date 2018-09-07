using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        private string name;
        private string header = "";
        private string footer = "";

        public GroupData()
        {
        }

        public GroupData(string name)
        {
            this.name = name;
        }
        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name;
           // return Name.Equals(other.Name, StringComparison.Ordinal); 
        }

        public override string ToString()
        {
            return "name=" + Name + "\nheader = " + Header + "\nfooter=" +footer;
        }

        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        public override int GetHashCode()
        {
            // return 0;
            return Name.GetHashCode();
        }

        public GroupData(string name, string header, string footer)
        {
            this.name = name;
            this.header = header;
            this.footer = footer;
        }

        [Column(Name="group_name"),NotNull]

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        [Column(Name = "group_header")]
        public string Header
        {
            get
            {
                return header;
            }
            set
            {
                header = value;
            }
        }

        [Column(Name = "group_footer")]
        public string Footer
        {
            get
            {
                return footer;
            }
            set
            {
                footer = value;
            }
        }

        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }

        // [Column(Name = "group_id"), PrimaryKey,Identity]
        public static List<GroupData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB()){
               return (from g in db.Groups select g).ToList();
            }
        }

        public List<ContactData> GetContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                       from gcr in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.Id && c.Deprecated == "0000-00-00 00:00:00")
                       select c).Distinct().ToList();
            }
        }

    }
}
