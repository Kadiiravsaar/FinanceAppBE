using Autofac;
using Finance.Core.Repositories;
using Finance.Core.Services;
using Finance.Core.UnitOfWorks;
using Finance.Repository;
using Finance.Repository.Repositories;
using Finance.Repository.UnitOfWorks;
using Finance.Service.Mapping;
using Finance.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace Finance.API.Modules
{
	public class RepoServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{

			builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
			builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

			builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

			var apiAssembly = Assembly.GetExecutingAssembly();				// API'nin  yani üzerinde çalıştığın Assembly al
			var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));  // Repo Assembly aldık
			var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile)); // Service Assembly aldık

			builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly) // bunlarda ara
				.Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope(); // sonu Repository bitenleri al ve
																					// interfacelerini de implement et

			builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
				.Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
		}
	}
}
