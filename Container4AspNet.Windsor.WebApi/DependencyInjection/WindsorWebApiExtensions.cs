namespace Container4AspNet.DependencyInjection
{
	using Castle.MicroKernel.Resolvers.SpecializedResolvers;
	using Castle.Windsor;
	using Owin;
	using System;
	using System.Web.Http;
	using System.Web.Http.Dispatcher;

	public static class WindsorWebApiExtensions
	{

		public static IAppBuilder DelegateCastleWindsorToWebApi(this IAppBuilder builder, HttpConfiguration configuration)
		{
			builder.ConfigureCastleWindsor(
				container =>
				{
					configuration.UseCastleWindsor(container);
				});

			return builder;
		}

		public static HttpConfiguration UseCastleWindsor(this HttpConfiguration configuration)
		{
			object diFound = null;
			if (!configuration.Properties.TryGetValue(Container4AspNet.DependencyInjection.Constants.DependencyInjectionProperty, out diFound))
			{
				configuration.UseCastleWindsor(new WindsorContainer());
			}

			return configuration;
		}

		public static HttpConfiguration UseCastleWindsor(this HttpConfiguration configuration, IWindsorContainer container)
		{
			configuration.Properties[Container4AspNet.DependencyInjection.Constants.DependencyInjectionProperty] = container;

			// shim the container instantiated IDependencyResolver to enable DI during ASP.NET dependency resolution
			container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
			configuration.DependencyResolver = new WindsorWebApiDependencyResolver(container.Kernel);

			// shim the container instantiated IHttpControllerActivator to enable DI during controller creation
			configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorHttpControllerActivator(container));

			return configuration;
		}

		public static HttpConfiguration ConfigureCastleWindsor(this HttpConfiguration configuration, Action<IWindsorContainer> configure)
		{
			IWindsorContainer container = configuration.Properties[Container4AspNet.DependencyInjection.Constants.DependencyInjectionProperty] as IWindsorContainer;
			configure.Invoke(container);

			return configuration;
		}
	}
}
