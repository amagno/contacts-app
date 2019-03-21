using ContactsApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactsApp.Infrastructure.Mappings
{
  public class ContactInfoMapping : IEntityTypeConfiguration<ContactInfo>
  {
    public void Configure(EntityTypeBuilder<ContactInfo> builder)
    {

      builder
        .Property(c => c.Address)
        .HasMaxLength(500)
        .IsRequired(false);

      builder
        .Property(c => c.Avatar)
        .HasMaxLength(1000)
        .IsRequired(false);

      builder
        .Property(c => c.Company)
        .HasMaxLength(200)
        .IsRequired(false);
      
      builder
        .Property(c => c.Comments)
        .HasMaxLength(1000)
        .IsRequired(false);

      builder
        .Property(c => c.Phone)
        .HasMaxLength(100)
        .IsRequired(false);

      builder
        .HasOne<Contact>()
        .WithOne(c => c.Info)
        .HasForeignKey<ContactInfo>(c => c.Id)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

    }
  }
}