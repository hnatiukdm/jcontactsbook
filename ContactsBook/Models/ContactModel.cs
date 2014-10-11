using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactsBook.Models
{
    public class ContactModel
    {
        public string UserId { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(30, ErrorMessage = "Name can be no larger than 30 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Group")]
        public int ContactGroupId { get; set; }
        public string AwailableGroups { get; set; }
        public String Address { get; set; }

        [RegularExpression("^([0-9_ -\\+])+$", ErrorMessage = "Phone number is not valid")]
        public String Number { get; set; }

        [EmailAddress(ErrorMessage = "Email address is not valid")]
        public String Email { get; set; }

    }
}