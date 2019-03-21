using ContactsApp.Domain;
using ContactsApp.Infrastructure.Mappings;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Infrastructure
{
    public class ContactsAppDbContext : DbContext
    {
        public ContactsAppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInfo> ContactsInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactMapping());
            modelBuilder.ApplyConfiguration(new ContactInfoMapping());
        }
    }
}