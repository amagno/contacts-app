using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Bogus;
using ContactsApp.API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ContactsApp.FunctionalTests.API
{
  public class CreateContactTests : IClassFixture<ApplicationFactory>, IDisposable
  {
    private readonly ApplicationFactory _factory;
    public CreateContactTests(ApplicationFactory factory)
    {
      _factory = factory;
    }
    [Fact]
    public async Task CreateValidContact()
    {
      var faker =  new Faker();
      var viewModel = new CreateContactViewModel
      {
        FirstName = faker.Name.FirstName(),
        LastName = faker.Name.LastName(),
        Email = faker.Internet.Email(),
        Birthday = faker.Date.Past(18),
        IsFavorite = false,
        Gender = "m",
        Address = faker.Address.FullAddress(),
        Avatar = faker.Internet.Avatar(),
        Company = faker.Company.CompanyName(),
        Phone = faker.Phone.PhoneNumber("(##) #####-####"),
        Comments =  faker.Random.Words(faker.Random.Int(0, 100))
      };
  
      var client = _factory.CreateClient();
      var result = await client.PostAsJsonAsync("/api/contacts", viewModel);

      result.EnsureSuccessStatusCode();

      var contact = await _factory.DbContext.Contacts.SingleOrDefaultAsync();

      Assert.Equal(viewModel.FirstName, contact.FirstName);
      Assert.Equal(viewModel.LastName, contact.LastName);
      Assert.Equal(viewModel.Email, contact.Email);
    }
    [Fact]
    public async Task CreateInvalidEmailContact()
    {
      var faker =  new Faker();
      var viewModel = new CreateContactViewModel
      {
        FirstName = faker.Name.FirstName(),
        LastName = faker.Name.LastName(),
        Email = "INVALID_EMAIL",
        Birthday = faker.Date.Past(18),
        IsFavorite = false,
        Gender = "m",
        Address = faker.Address.FullAddress(),
        Avatar = faker.Internet.Avatar(),
        Company = faker.Company.CompanyName(),
        Phone = faker.Phone.PhoneNumber("(##) #####-####"),
        Comments =  faker.Random.Words(faker.Random.Int(0, 100))
      };
  
      var client = _factory.CreateClient();
      var result = await client.PostAsJsonAsync("/api/contacts", viewModel);

      Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
    // deleta e cria database para cada teste executado
    public void Dispose()
    {
      _factory.DbContext.Database.EnsureDeleted();
      _factory.DbContext.Database.EnsureCreated();
    }
  }
}