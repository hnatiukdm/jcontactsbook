using System.Collections.Generic;

namespace ContactsBook.Models
{
    public class UserContactsModel
    {
        private readonly string _id;

        public UserContactsModel(string id)
        {
            _id = id;
        }

        public List<ContactModel> Contacts { get; set; }

        public IEnumerable<ContactGroupModel> Groups { get; set; } 

        public string Id
        {
            get { return _id; }
        }
    }
}