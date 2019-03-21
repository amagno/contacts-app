using ContactsApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactsApp.Infrastructure.Mappings
{
  public class ContactMapping : IEntityTypeConfiguration<Contact>
  {
    public void Configure(EntityTypeBuilder<Contact> builder)
    {

      builder
        .Property(c => c.FirstName)
        .HasMaxLength(200)
        .IsRequired();

      builder
        .Property(c => c.LastName)
        .HasMaxLength(200)
        .IsRequired();

      builder
        .Property(c => c.Email)
        .HasMaxLength(500)
        .IsRequired();

      builder.HasIndex(c => c.Email).IsUnique();

      builder
        .Property(c => c.Birthday)
        .IsRequired();

      builder
        .Property(c => c.Created)
        .IsRequired();

      builder
        .Property(c => c.Updated)
        .IsRequired(false);
    
      builder
        .Property(c => c.Gender)
        .HasMaxLength(1)
        .IsRequired();
      
      builder
        .Property(c => c.IsFavorite)
        .HasField("_isFavorite");
    }
  }
}