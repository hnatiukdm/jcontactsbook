using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContactsBook.Models
{
    public class Contact
    {

        #region Fields

        #endregion

        #region Properties

        public int Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Address { get; set; }
        public String Number { get; set; }
        public String Email { get; set; }
        //public String ContactsGroup { get; set; }

        public virtual User User { get; set; }
        public string UserId { get; set; }

        public int? GroupId { get; set; }
        public virtual ContactGroup ContactGroup { get; set; }

        #endregion

        #region Constructor
        #endregion

        public void ChangeName(string firstName, string secondName)
        {
            Name = firstName;
            Surname = secondName;
        }
    }
}