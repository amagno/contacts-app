using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsApp.Domain
{
  public interface IContactRepository
  {
    Task Insert(Contact contact);
    Task Update(Contact contact);
    Task Delete(Contact contact);
    Task<Contact> GetById(int id);
    Task<Contact> GetByEmail(string email);
    Task<List<Contact>> GetAll(int? limit = null, int? skip = null);
  }
}