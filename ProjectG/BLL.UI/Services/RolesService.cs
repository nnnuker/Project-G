using BLL.Concrete;
using BLL.UI.Models;
using BLL.UI.Mappers;
using System.Collections.Generic;
using System.Linq;

namespace BLL.UI.Services
{
  public class RolesService
  {
    private readonly Repository<Role> _repository;

    public RolesService(Repository<Role> repository)
    {
      _repository = repository;
    }

    public BllRole Get(int id)
    {
      return _repository.Get(id)?.ToBll();
    }

    public IEnumerable<BllRole> GetAll()
    {
      return _repository.GetAll()?.ToList().Select(role => role.ToBll());
    }

    public void Create(BllRole entity)
    {
      _repository.Create(entity.ToDB());
    }

    public void Update(BllRole entity)
    {
      _repository.Update(entity.ToDB());
    }

    public void Delete(int id)
    {
      _repository.Delete(id);
    }
  }
}