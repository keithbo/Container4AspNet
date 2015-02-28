using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Container4AspNet.DependencyInjection
{
	public static class WindsorMvcExtensions
	{
		public static IAppBuilder DelegateCastleWindsorToMvc(this IAppBuilder builder)
		{
			return builder.ConfigureCastleWindsor(
				container =>
				{
					// Generally it is frowned upon to let components inject the container itself.
					// These two components are direct extension points for MVC to resolve types and so we
					// need to bootstrap requests through to the IWindsorContainer
					container.Register(Component.For<IFilterProvider>()
						.ImplementedBy<WindsorMvcFilterProvider>().LifestylePerWebRequest()
						.DependsOn(Dependency.OnValue<IWindsorContainer>(container)));
					//container.Register(Component.For<IViewPageActivator>()
					//	.ImplementedBy<WindsorMvcViewPageActivator>().LifestylePerWebRequest()
					//	.DependsOn(Dependency.OnValue<IWindsorContainer>(container)));

					DependencyResolver.SetResolver(new WindsorMvcDependencyResolver(container, DependencyResolver.Current));
				});
		}
	}
}
