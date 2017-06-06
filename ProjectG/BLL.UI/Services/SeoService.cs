using BLL.Concrete;
using BLL.UI.Mappers;
using BLL.UI.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL.UI.Services
{
  public class SeoService
  {
    private readonly Repository<Seo> _repository;

    public SeoService(Repository<Seo> repository)
    {
      _repository = repository;
    }

    public BllSeo Get(int id)
    {
      return _repository.Get(id)?.ToBll();
    }

    public IEnumerable<BllSeo> GetAll()
    {
      return _repository.GetAll()?.Select(user => user.ToBll());
    }

    public BllSeo Create(BllSeo entity)
    {
      return _repository.Create(entity.ToDB())?.ToBll();
    }

    public void Update(BllSeo entity)
    {
      _repository.Update(entity.ToDB());
    }

    public void Delete(int id)
    {
      _repository.Delete(id);
    }

    public BllSeo Get(string seo)
    {
      return _repository.GetEntitySet().FirstOrDefault(r => r.Name.Equals(seo))?.ToBll();
    }
  }
}