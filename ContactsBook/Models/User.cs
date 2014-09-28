using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ContactsBook.Models
{
    public class User : IdentityUser
    {
        //public int? ContactId { get; set; }
        //public virtual Contact Contact { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        //public int? GroupId { get; set; }
        //public virtual ContactGroup ContactGroup { get; set; }
        public virtual ICollection<ContactGroup> ContactGroups { get; set; }
        //public String Name { get; set; }
        //public String Email { get; set; }

    }
}