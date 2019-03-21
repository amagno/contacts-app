using System;
using Bogus;
using ContactsApp.Domain;
using Xunit;


namespace ContactsApp.UnitTests.Domain
{
  public class ContactTest
  {
    public static Contact CreateValidContact()
    {
      var faker = new Faker();
      var info =  new ContactInfo(
        faker.Company.CompanyName(),
        faker.Internet.Avatar(),
        faker.Address.FullAddress(),
        faker.Phone.PhoneNumber("(##) #####-####"),
        faker.Random.Words(faker.Random.Int(0, 100))
      );
      return new Contact(
        faker.Name.FirstName(),
        faker.Name.LastName(),
        faker.Internet.Email(),
        faker.Date.Past(faker.Random.Int(8, 80)),
        faker.PickRandom(new [] { "f", "m"}),
        false,
        info
      );
    }
    [Fact]
    public void TestNewContact()
    {
      var contact = CreateValidContact();
      Assert.Null(contact.Updated);
    }
    [Fact]
    public void TestCreateFavoriteInvalidAgeContact()
    {
      var faker = new Faker();
      var info =  new ContactInfo(
        faker.Company.CompanyName(),
        faker.Internet.Avatar(),
        faker.Address.FullAddress(),
        faker.Phone.PhoneNumber("(##) #####-####"),
        faker.Random.Words(faker.Random.Int(0, 100))
      );
      
      Assert.Throws<InvalidFavoriteContactException>(() => {
        var contact = new Contact(
          faker.Name.FirstName(),
          faker.Name.LastName(),
          faker.Internet.Email(),
          // invalid age
          faker.Date.Past(8),
          faker.PickRandom(new [] { "f", "m"}),
          true,
          info
        );
      });
    }
    [Fact]
    public void TestFavoriteValidContact()
    {
      var contact = CreateValidContact();
      contact.SetIsFavorite(true);
      Assert.True(contact.IsFavorite);
      Assert.NotNull(contact.Updated);
    }

    
  }
}