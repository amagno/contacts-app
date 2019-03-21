using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ContactsApp.FunctionalTests.API
{
  public class GenerateContactsTests : IClassFixture<ApplicationFactory>, IDisposable
  {
      private readonly ApplicationFactory _factory;
      public GenerateContactsTests(ApplicationFactory factory)
      {
        _factory = factory;
      }

      [Fact]
      public async Task TestGenerateContacts()
      {
        int count = 10;

        var client = _factory.CreateClient();
        
        var result = await client.GetAsync($"/api/contacts/generate/{count}");
        result.EnsureSuccessStatusCode();

        var contacts = await _factory.DbContext.Contacts.ToListAsync();
        Assert.Equal(count, contacts.Count);
      }

      // deleta e cria database para cada teste executado
      public void Dispose()
      {
        _factory.DbContext.Database.EnsureDeleted();
        _factory.DbContext.Database.EnsureCreated();
      }
  }
}
