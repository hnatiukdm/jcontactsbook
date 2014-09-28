using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Web;

namespace ContactsBook.Models
{
    public class UsersRepository
    {
        private static readonly Lazy<UsersRepository> _lazyInstance = new Lazy<UsersRepository>(() => new UsersRepository(), true);

        private readonly ContactsDb _contactsDb;

        private UsersRepository()
        {
            _contactsDb = new ContactsDb();
        }

        public string CommonGroupName { get { return "All Contacts"; } }

        public static UsersRepository Instance
        {
            get { return _lazyInstance.Value; }
        }

        public Contact GetContactById(int contactId, User user)
        {
            var contact = user.Contacts.FirstOrDefault(c => c.Id == contactId);

            if (contact == null)
            {
                throw new InstanceNotFoundException("Cannot load contact from DB");
            }
            return contact;
        }

        public User GetUserById(string userId)
        {
            var user = ContactsDb.Users.Find(userId);

            if (user == null)
            {
                throw new InstanceNotFoundException("Cannot load user from DB");
            }
            return user;
        }

        public void SubmitChanges()
        {
            try
            {
                ContactsDb.SaveChanges();
            }
            catch {}
        }

        public ContactsDb ContactsDb
        {
            get { return _contactsDb; }
        }
    }
}