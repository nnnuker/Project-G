using BLL.Concrete;
using BLL.UI.Mappers;
using BLL.UI.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL.UI.Services
{
  public class CategoriesService
  {
    private readonly Repository<Category> _categoriesRepository;
    private readonly Repository<Page> _pagesRepository;

    public CategoriesService(Repository<Category> categoriesRepository, Repository<Page> pagesRepository)
    {
      _categoriesRepository = categoriesRepository;
      _pagesRepository = pagesRepository;
    }

    public BllCategory Get(int id)
    {
      var result = _categoriesRepository.Get(id != 1 ? id : 2);

      return AddAdditionalData(result);
    }

    public IEnumerable<BllCategory> GetAll()
    {
      return _categoriesRepository.GetAll()?.Where(u => u.ParentId != 1).Select(user => AddAdditionalData(user));
    }

    public BllCategory Create(BllCategory entity)
    {
      entity.ParentId = entity.ParentId < 2 ? 2 : entity.ParentId;

      return _categoriesRepository.Create(entity.ToDB())?.ToBll();
    }

    public void Update(BllCategory entity)
    {
      entity.ParentId = entity.ParentId < 2 ? 2 : entity.ParentId;

      _categoriesRepository.Update(entity.ToDB());
    }

    public void Delete(int id)
    {
      _categoriesRepository.Delete(id);
    }

    private BllCategory AddAdditionalData(Category category)
    {
      var bll = category?.ToBll();

      if (bll != null)
      {
        bll.HasChildCategory = _categoriesRepository.GetEntitySet().FirstOrDefault(c => c.ParentId == bll.Id) != null;
        bll.PagesCount = category.Pages.Count;
      }

      return bll;
    }
  }
}