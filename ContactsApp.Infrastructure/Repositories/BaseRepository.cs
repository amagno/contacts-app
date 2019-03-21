using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Infrastructure.Repositories
{
  public abstract class BaseRepository<TEntity> where TEntity : class
  {
    protected DbSet<TEntity> DbSet;

    private ContactsAppDbContext _db;
    public BaseRepository(ContactsAppDbContext db)
    {
      _db = db;
      DbSet = db.Set<TEntity>();
    }
    protected Task SaveAsync()
    {
      return _db.SaveChangesAsync();
    }
  }
}