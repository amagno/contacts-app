using ContactsAPI.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInfo> ContactsInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Contact>()
                .HasIndex(p => p.Email)
                .IsUnique();
            modelBuilder
                .Entity<ContactInfo>()
                .HasOne<Contact>()
                .WithOne(c => c.Info)
                .HasForeignKey<ContactInfo>(c => c.Id)
                .IsRequired();
        }
    }
}