using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading;

namespace BLL.Concrete
{
  public class Repository<TEntity> where TEntity : class, new()
  {
    private readonly DbContext _context;
    private static readonly ReaderWriterLockSlim _lockSlim = new ReaderWriterLockSlim();

    public Repository(DbContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IEnumerable<TEntity> GetAll()
    {
      _lockSlim.EnterReadLock();
      try
      {
        return _context.Set<TEntity>();
      }
      finally
      {
        _lockSlim.ExitReadLock();
      }
    }

    public TEntity Get(int id)
    {
      if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));

      _lockSlim.EnterWriteLock();
      try
      {
        return _context.Set<TEntity>().Find(id);
      }
      finally
      {
        _lockSlim.ExitWriteLock();
      }   
    }

    public DbSet<TEntity> GetEntitySet()
    {
      _lockSlim.EnterWriteLock();
      try
      {
        return _context.Set<TEntity>();
      }
      finally
      {
        _lockSlim.ExitWriteLock();
      }
    }

    public TEntity Create(TEntity entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException(nameof(entity));
      }

      _lockSlim.EnterWriteLock();
      try
      {
        var c = _context.Set<TEntity>().Add(entity);
        
        _context.SaveChanges();

        return c;
      }
      finally
      {
        _lockSlim.ExitWriteLock();
      }
    }

    public void Update(TEntity entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException(nameof(entity));
      }

      _lockSlim.EnterWriteLock();
      try
      {
        _context.Set<TEntity>().AddOrUpdate(entity);

        _context.SaveChanges();
      }
      finally
      {
        _lockSlim.ExitWriteLock();
      }
    }

    public void Delete(int id)
    {
      if (id <= 0)
      {
        throw new ArgumentOutOfRangeException(nameof(id));
      }

      _lockSlim.EnterWriteLock();
      try
      {
        var result = _context.Set<TEntity>().Find(id);
        if (result == null)
        {
          return;
        }

        _context.Set<TEntity>().Remove(result);

        _context.SaveChanges();
      }
      finally
      {
        _lockSlim.ExitWriteLock();
      }
    }
  }
}