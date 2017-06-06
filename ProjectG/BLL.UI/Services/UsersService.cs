using BLL.Concrete;
using BLL.UI.Mappers;
using BLL.UI.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL.UI.Services
{
  public class UsersService
  {
    private readonly Repository<User> _repository;

    public UsersService(Repository<User> repository)
    {
      _repository = repository;
    }

    public BllUser Get(int id)
    {
      return _repository.Get(id)?.ToBll();
    }

    public IEnumerable<BllUser> GetAll()
    {
      return _repository.GetAll()?.Select(user => user.ToBll());
    }

    public void Create(BllUser entity)
    {
      _repository.Create(entity.ToDB());
    }

    public void Update(BllUser entity)
    {
      _repository.Update(entity.ToDB());
    }

    public void Delete(int id)
    {
      _repository.Delete(id);
    }

    public BllUser Get(string email)
    {
      return _repository.GetAll().ToList().FirstOrDefault(user => user.Email.Equals(email))?.ToBll();
    }
  }
}