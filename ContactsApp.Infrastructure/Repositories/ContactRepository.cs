using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Infrastructure.Repositories
{
  public class ContactRepository : BaseRepository<Contact>, IContactRepository
  {
    public IQueryable<Contact> Query { get => DbSet.AsNoTracking().Include(c => c.Info); }
    public ContactRepository(ContactsAppDbContext db) : base(db)
    {
    }
    public Task Delete(Contact contact)
    {
      DbSet.Remove(contact);
      return SaveAsync();
    }
    public Task Insert(Contact contact)
    {
      DbSet.Add(contact);
      return SaveAsync();
    }
    public Task Update(Contact contact)
    {
      DbSet.Update(contact);
      return SaveAsync();
    }

    public Task<Contact> GetById(int id)
    {
      return Query.FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task<Contact> GetByEmail(string email)
    {
      return Query.FirstOrDefaultAsync(c => c.Email == email);
    }

    public Task<List<Contact>> GetAll(int? limit = null, int? skip = null)
    {
      if (limit != null && skip == null)
      {
        return Query
          .Take((int)limit)
          .ToListAsync();
      }
      if (limit != null && skip != null)
      {
        return Query
          .Skip((int)skip)
          .Take((int)limit)
          .ToListAsync();
      }
      return Query.ToListAsync();
    }
  }
}