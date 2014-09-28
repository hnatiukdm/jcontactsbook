using System.Collections.Generic;

namespace ContactsBook.Models
{
    public class ContactGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}