using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using ContactsBook.Models;
using WebGrease.Css.Extensions;

namespace ContactsBook.Controllers
{
    public class HomeController : Controller
    {

        [Authorize]
        public ActionResult Index()
        {
            var user = GetCurrentUser();


            UserContactsModel userEditModel = new UserContactsModel("0")
            {
                Contacts = new List<ContactModel>(),
                Groups = new List<ContactGroupModel>()
            };

            if (user != null)
            {
                userEditModel = new UserContactsModel(user.Id)
                {
                    Contacts = user.Contacts.Select(c => new ContactModel()
                    {
                        Id = c.Id,
                        FirstName = c.Name,
                        LastName = c.Surname,
                        Address = c.Address,
                        Number = c.Number,
                        Email = c.Email,
                        ContactGroupId = c.ContactGroup != null ? c.ContactGroup.Id : user.ContactGroups.First().Id
                    }).ToList(),
                    Groups = user.ContactGroups.Select(cg => new ContactGroupModel()
                    {
                        GroupName = cg.GroupName,
                        Id = cg.Id
                    })
                };
            }

            return View(userEditModel);
        }

        private User GetCurrentUser()
        {
            var identity = User.Identity;

            var user =
                UsersRepository.Instance.ContactsDb.Users.Select(u => u).FirstOrDefault(id => id.UserName == identity.Name);

            return user;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContact(ContactModel contact)
        {
            var user = GetCurrentUser();

            if (ModelState.IsValid)
            {
                user.Contacts.Add(new Contact()
                {
                    GroupId = contact.ContactGroupId,
                    Name = contact.FirstName,
                    Surname = contact.LastName,
                    Address = contact.Address,
                    Number = contact.Number,
                    Email = contact.Email,
                });
                UsersRepository.Instance.SubmitChanges();

                return RedirectToAction("Index");
            }

            return View(contact);
        }

        public ActionResult CreateContact()
        {
            var user = GetCurrentUser();
            var groups = user.ContactGroups.Select(g =>
                new ContactGroupModel()
                {
                    Id = g.Id,
                    GroupName = g.GroupName
                }).ToArray();

            return PartialView(new ContactModel() {AwailableGroups = System.Web.Helpers.Json.Encode(groups)});
        }

        [HttpPost]
        [Authorize]
        public ActionResult Save(ContactModel contactModel)
        {
            var user = UsersRepository.Instance.GetUserById(contactModel.UserId);

            var contact = UsersRepository.Instance.GetContactById(contactModel.Id, user);

            contact.ChangeName(contactModel.FirstName, contactModel.LastName);
            contact.ContactGroup = user.ContactGroups.First(c => c.Id == contactModel.ContactGroupId);
            contact.Email = contactModel.Email;
            contact.Number = contactModel.Number;
            contact.Address = contactModel.Address;

            UsersRepository.Instance.SubmitChanges();

            return RedirectToAction("Index");
        }


        [Authorize]
        public ActionResult EditContact(ContactModel contactModel)
        {
            return PartialView(contactModel);
        }

        [Authorize]
        public ActionResult Remove(ContactModel contactModel)
        {
            var user = UsersRepository.Instance.GetUserById(contactModel.UserId);
            
            var contact = UsersRepository.Instance.GetContactById(contactModel.Id, user);

            user.Contacts.Remove(contact);

            UsersRepository.Instance.SubmitChanges();

            return RedirectToAction("Index");
        }

        public ActionResult CreateGroup()
        {
            return PartialView(new ContactGroupModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(ContactGroupModel contactGroup)
        {
            var user = GetCurrentUser();

            user.ContactGroups.Add(new ContactGroup()
            {
                GroupName = contactGroup.GroupName

            });
            
            UsersRepository.Instance.SubmitChanges();

            return RedirectToAction("Index");
        }

        public ActionResult RemoveGroup(int id)
        {
            var user = GetCurrentUser();

            var commonGroup =
                user.ContactGroups.Where(cg => cg.GroupName == UsersRepository.Instance.CommonGroupName).OrderBy(cg => cg.Id).FirstOrDefault();

            if (commonGroup != null)
            {
                user.Contacts.Where(c => c.ContactGroup.Id == id).ForEach(c => c.ContactGroup = commonGroup);

                var groupToRemove = user.ContactGroups.FirstOrDefault(cg => cg.Id == id);

                if (groupToRemove != null)
                {
                    user.ContactGroups.Remove(groupToRemove);
                }

                UsersRepository.Instance.SubmitChanges();
            }

            return RedirectToAction("Index");
        }
    }
}