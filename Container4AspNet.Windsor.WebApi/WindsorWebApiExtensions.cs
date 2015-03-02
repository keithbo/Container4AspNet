namespace Container4AspNet.Windsor.WebApi
{
	using Castle.MicroKernel.Resolvers.SpecializedResolvers;
	using Castle.Windsor;
	using Owin;
	using System.Web.Http;
	using System.Web.Http.Dispatcher;

	/// <summary>
	/// Extension methods specific to WebApi integration into Container4AspNet
	/// </summary>
	public static class WindsorWebApiExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static WindsorContainerConfigurator ToWebApi(this WindsorContainerConfigurator configurator)
		{
			return ToWebApi(configurator, configurator.Container.Resolve<HttpConfiguration>());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static WindsorContainerConfigurator ToWebApi(this WindsorContainerConfigurator configurator, HttpConfiguration configuration)
		{
			RegisterContainer(configurator.Container, configuration);
			return configurator;
		}

		/// <summary>
		/// Extension overload to UseWebApi that forces use of the dependency container to retrieve the active HttpConfiguration to then
		/// pass along the normal UseWebApi(HttpConfiguration) execution path.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IAppBuilder UseWebApi(this IAppBuilder builder)
		{
			return builder.UseWebApi(builder.Resolve<HttpConfiguration>());
		}

		private static void RegisterContainer(IWindsorContainer container, HttpConfiguration configuration)
		{
			configuration.Properties[Container4AspNet.Constants.DependencyInjectionProperty] = container;

			// shim the container instantiated IDependencyResolver to enable DI during ASP.NET dependency resolution
			container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
			configuration.DependencyResolver = new WindsorWebApiDependencyResolver(container);

			// shim the container instantiated IHttpControllerActivator to enable DI during controller creation
			configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorHttpControllerActivator(container));
		}
	}
}
