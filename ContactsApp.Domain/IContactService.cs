using System;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsApp.Domain
{
  public interface IContactService
  {
    Task CreateNewContact(
      string firstName, 
      string lastName, 
      string email, 
      DateTime birthDay,
      string gender, 
      bool isFavorite,
      string company = null,
      string avatar = null,
      string address = null,
      string phone = null,
      string comments = null
    );
    Task EditContact(
      int id,
      string firstName = null, 
      string lastName = null, 
      string email = null, 
      DateTime? birthDay = null,
      string gender = null, 
      bool? isFavorite = null,
      string company = null,
      string avatar = null,
      string address = null,
      string phone = null,
      string comments = null
    );
    Task SetIsFavorite(int id, bool isFavorite);

    Task DeleteContact(int id);

  }
}