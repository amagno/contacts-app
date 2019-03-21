using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ContactsApp.Infrastructure
{
  public class ContactsAppDesignTimeDbContextFactory: IDesignTimeDbContextFactory<ContactsAppDbContext>
  {
    public ContactsAppDbContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<ContactsAppDbContext>()
        .UseSqlServer("Server=localhost; Database=contacts-app; Integrated Security=false; User Id=sa; Password=abc123##;");

      return new ContactsAppDbContext(optionsBuilder.Options);
    }
  }
}