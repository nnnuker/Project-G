using BLL.UI.Models;

namespace BLL.UI.Mappers
{
  public static class Mappers
  {
    #region Roles
    public static BllRole ToBll(this Role role)
    {
      return new BllRole
      {
        Id = role.Id,
        Name = role.Name
      };
    }

    public static Role ToDB(this BllRole role)
    {
      return new Role
      {
        Id = role.Id,
        Name = role.Name
      };
    }
    #endregion

    #region Categories
    public static BllCategory ToBll(this Category entity)
    {
      return new BllCategory
      {
        Id = entity.Id,
        Name = entity.Name,
        ParentId = entity.ParentId
      };
    }

    public static Category ToDB(this BllCategory entity)
    {
      return new Category
      {
        Id = entity.Id,
        Name = entity.Name,
        ParentId = entity.ParentId
      };
    }
    #endregion

    #region Pages
    public static BllPage ToBll(this Page entity)
    {
      return new BllPage
      {
        Id = entity.Id,
        CategoryId = entity.CategoryId,
        UrlId = entity.UrlId,
        Content = entity.Content,
        Title = entity.Title,
        CreationDate = entity.CreationDate,
        Url = entity.Seo.Name
      };
    }

    public static Page ToDB(this BllPage entity)
    {
      return new Page
      {
        Id = entity.Id,
        CategoryId = entity.CategoryId,
        UrlId = entity.UrlId,
        Content = entity.Content,
        Title = entity.Title,
        CreationDate = entity.CreationDate
      };
    }
    #endregion

    #region Seos
    public static BllSeo ToBll(this Seo entity)
    {
      return new BllSeo
      {
        Id = entity.Id,
        Name = entity.Name
      };
    }

    public static Seo ToDB(this BllSeo entity)
    {
      return new Seo
      {
        Id = entity.Id,
        Name = entity.Name
      };
    }
    #endregion

    #region Users
    public static BllUser ToBll(this User entity)
    {
      return new BllUser
      {
        Id = entity.Id,
        Email = entity.Email,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Password = entity.Password,
        RoleId = entity.RoleId
      };
    }

    public static User ToDB(this BllUser entity)
    {
      return new User
      {
        Id = entity.Id,
        Email = entity.Email,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Password = entity.Password,
        RoleId = entity.RoleId
      };
    }
    #endregion
  }
}