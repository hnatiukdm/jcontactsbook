using System.ComponentModel.DataAnnotations;

namespace ContactsBook.Models
{
    public class ContactGroupModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Group name can be no larger than 30 characters")]
        [Display(Name = "Group name")]

        public string GroupName { get; set; }
    }
}