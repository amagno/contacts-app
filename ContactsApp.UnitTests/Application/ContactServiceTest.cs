using System.Threading.Tasks;
using Bogus;
using ContactsApp.Application.Exceptions;
using ContactsApp.Application.Services;
using ContactsApp.Domain;
using ContactsApp.UnitTests.Domain;
using Moq;
using Xunit;

namespace ContactsApp.UnitTests.Application
{
  public class ContactServiceTest
  {
    [Fact]
    public async Task TestCreateContact()
    {
      var contactRepositoryMock = new Mock<IContactRepository>();

      contactRepositoryMock
        .Setup(repo => repo.GetByEmail(It.IsAny<string>()))
        .ReturnsAsync(null as Contact);

      var service = new ContactService(contactRepositoryMock.Object);
      var faker = new Faker();

      await service.CreateNewContact(
        faker.Name.FirstName(),
        faker.Name.LastName(),
        faker.Internet.Email(),
        faker.Date.Past(faker.Random.Int(8, 80)),
        faker.PickRandom(new [] { "f", "m"}),
        false,
        faker.Company.CompanyName(),
        faker.Internet.Avatar(),
        faker.Address.FullAddress(),
        faker.Phone.PhoneNumber("(##) #####-####"),
        faker.Random.Words(faker.Random.Int(0, 100))
      );

      contactRepositoryMock
        .Verify(repo => repo.Insert(It.IsAny<Contact>()), Times.Once());
    }
    [Fact]
    public async Task TestCreateContactAlreadyEmailExists()
    {
      var contactRepositoryMock = new Mock<IContactRepository>();

      contactRepositoryMock
        .Setup(repo => repo.GetByEmail(It.IsAny<string>()))
        .ReturnsAsync(ContactTest.CreateValidContact());

      var service = new ContactService(contactRepositoryMock.Object);
      var faker = new Faker();

      await Assert.ThrowsAsync<EmailAlreadyExistsException>(async () => {
        await service.CreateNewContact(
          faker.Name.FirstName(),
          faker.Name.LastName(),
          faker.Internet.Email(),
          faker.Date.Past(faker.Random.Int(8, 80)),
          faker.PickRandom(new [] { "f", "m"}),
          false,
          faker.Company.CompanyName(),
          faker.Internet.Avatar(),
          faker.Address.FullAddress(),
          faker.Phone.PhoneNumber("(##) #####-####"),
          faker.Random.Words(faker.Random.Int(0, 100))
        );
      });

      contactRepositoryMock
        .Verify(repo => repo.Insert(It.IsAny<Contact>()), Times.Never());
    }
  }
}