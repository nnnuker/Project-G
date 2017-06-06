using BLL.Concrete;
using BLL.UI.Mappers;
using BLL.UI.Models;
using BLL.UI.Providers;
using System.Collections.Generic;
using System.Linq;

namespace BLL.UI.Services
{
  public class PagesService
  {
    private readonly Repository<Page> _repository;
    private readonly PageTitleProvider _titleProvider;

    public PagesService(Repository<Page> repository, PageTitleProvider titleProvider)
    {
      _repository = repository;
      _titleProvider = titleProvider;
    }

    public BllPage Get(int id)
    {
      return _repository.Get(id)?.ToBll();
    }

    public IEnumerable<BllPage> GetAll()
    {
      return _repository.GetAll()?.Select(user => user.ToBll());
    }

    public void Create(BllPage entity)
    {
      if (entity.CategoryId < 2)
      {
        return;
      }

      entity.Title = _titleProvider.GetValue(entity.Content, "h2");
      _repository.Create(entity.ToDB());
    }

    public void Update(BllPage entity)
    {
      entity.Title = _titleProvider.GetValue(entity.Content, "h2");
      _repository.Update(entity.ToDB());
    }

    public void Delete(int id)
    {
      _repository.Delete(id);
    }

    public BllPage Get(string seoUrl)
    {
      return _repository
        .GetEntitySet()
        .FirstOrDefault(c => c.Seo.Name.Equals(seoUrl, System.StringComparison.OrdinalIgnoreCase))?
        .ToBll();
    }

    public IEnumerable<BllPage> GetByCategory(int id)
    {
      return _repository
        .GetAll()?
        .Where(p => p.CategoryId == id)
        .Select(p => p.ToBll());
    }
  }
}