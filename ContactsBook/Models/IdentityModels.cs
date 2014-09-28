using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Validation;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ContactsBook.Models
{

    public class ContactsDb : IdentityDbContext<User>
    {
        public ContactsDb()
            : base("ContactsDb")
        {
        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            EntityTypeConfiguration<User> userConfigurator = modelBuilder.Entity<User>().ToTable("Users").
                HasKey(u => u.Id);

            userConfigurator.HasMany<Contact>(c => c.Contacts).WithRequired(c => c.User).HasForeignKey(c => c.UserId);
            userConfigurator.HasMany<ContactGroup>(c => c.ContactGroups).WithRequired(c => c.User).HasForeignKey(c => c.UserId);


            modelBuilder.Entity<ContactGroup>().ToTable("ContactGroups").HasMany(g => g.Contacts).WithRequired(c => c.ContactGroup).HasForeignKey(c => c.GroupId);
            modelBuilder.Entity<Contact>().ToTable("Contacts");
        }
    }
}


