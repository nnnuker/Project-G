using BLL.Concrete;
using Ninject;
using Ninject.Web.Common;
using System.Data.Entity;

namespace BLL.Activation
{
  public static class ResolverModule
  {
    public static void Configure(this IKernel kernel)
    {
      kernel.Bind<DbContext>().To<DatabaseContext>().InSingletonScope();

      kernel.Bind<Repository<Role>>().ToSelf().InRequestScope();
      kernel.Bind<Repository<User>>().ToSelf().InRequestScope();
    }
  }
}