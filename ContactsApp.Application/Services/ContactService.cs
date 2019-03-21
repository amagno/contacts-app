using System;
using System.Linq;
using System.Threading.Tasks;
using ContactsApp.Application.Exceptions;
using ContactsApp.Domain;

namespace ContactsApp.Application.Services
{
  public class ContactService : IContactService
  {
    private readonly IContactRepository _contactRepository;
    public ContactService(IContactRepository contactRepository)
    {
      _contactRepository = contactRepository;
    }
    public async Task CreateNewContact(string firstName, string lastName, string email, DateTime birthday, string gender, bool isFavorite, string company = null, string avatar = null, string address = null, string phone = null, string comments = null)
    {
      var exists = await _contactRepository.GetByEmail(email);
      if (exists != null)
      {
        throw new EmailAlreadyExistsException(email);
      }

      try
      {
        var contact = new Contact(
          firstName,
          lastName,
          email,
          birthday,
          gender,
          isFavorite,
          new ContactInfo(company, avatar, address, phone, comments)
        );
        await _contactRepository.Insert(contact);
      }
      catch (InvalidFavoriteContactException e)
      {
        throw new BadRequestException(e.Message);
      }
    }
    public async Task SetIsFavorite(int id, bool isFavorite)
    {
      var contact = await _contactRepository.GetById(id);
      if (contact == null)
      {
        throw new ContactNotExistsException(id);
      }

      try
      {
        contact.SetIsFavorite(isFavorite);
        await _contactRepository.Update(contact);
      }
      catch(InvalidFavoriteContactException e)
      {
        throw new BadRequestException(e.Message);
      }
    }
    public async Task EditContact(int id, string firstName = null, string lastName = null, string email = null, DateTime? birthday = null, string gender = null, bool? isFavorite = null, string company = null, string avatar = null, string address = null, string phone = null, string comments = null)
    {
      var contact = await _contactRepository.GetById(id);
      if (contact == null)
      {
        throw new ContactNotExistsException(id);
      }

      var exists = await _contactRepository.GetByEmail(email);
      if (exists != null && exists.Id != contact.Id)
      {
        throw new EmailAlreadyExistsException(email);
      }

      try
      {
        var info = new ContactInfo(
          company ?? contact.Info.Company,
          avatar ?? contact.Info.Avatar,
          address ?? contact.Info.Address,
          phone ?? contact.Info.Phone
        );
        contact.EditContact(
          firstName ?? contact.FirstName,
          lastName ?? contact.LastName,
          email ?? contact.Email,
          birthday ?? contact.Birthday,
          gender ?? contact.Gender,
          isFavorite ?? contact.IsFavorite,
          info
        );
        await _contactRepository.Update(contact);
      }
      catch (InvalidFavoriteContactException e)
      {
        throw new BadRequestException(e.Message);
      }
    }

    public async Task DeleteContact(int id)
    {
      var contact = await _contactRepository.GetById(id);

      if (contact == null)
      {
        throw new ContactNotExistsException(id);
      }

      await _contactRepository.Delete(contact);
    }
  }
}