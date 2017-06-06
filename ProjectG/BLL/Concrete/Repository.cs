using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace BLL.Concrete
{
  public class Repository<TEntity> where TEntity : class, new()
  {
    private readonly DbContext _context;

    public Repository(DbContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IEnumerable<TEntity> GetAll()
    {
      return _context.Set<TEntity>();
    }

    public TEntity Get(int id)
    {
      if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));

      var result = _context.Set<TEntity>().Find(id);

      return result;
    }

    public DbSet<TEntity> GetEntitySet()
    {
      return _context.Set<TEntity>();
    }

    public TEntity Create(TEntity entity)
    {
      if (entity == null)
        throw new ArgumentNullException(nameof(entity));

      var c = _context.Set<TEntity>().Add(entity);

      var errors = _context.GetValidationErrors();

      _context.SaveChanges();

      return c;
    }

    public void Update(TEntity entity)
    {
      if (entity == null) throw new ArgumentNullException(nameof(entity));

      _context.Set<TEntity>().AddOrUpdate(entity);

      _context.SaveChanges();
    }

    public void Delete(int id)
    {
      if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

      var result = _context.Set<TEntity>().Find(id);
      if (result == null)
      {
        return;
      }        

      _context.Set<TEntity>().Remove(result);

      _context.SaveChanges();
    }
  }
}