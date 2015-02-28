namespace Container4AspNet.DependencyInjection
{
	using Castle.Windsor;
	using Owin;
	using System;

	public static class WindsorExtensions
	{
		/// <summary>
		/// Applies a default IWindsorContainer implementation to the target IAppBuilder instance. Then returns a wrapper
		/// implementation of IAppBuilder that will attempt to resolve middleware implementations against the IWindsorContainer
		/// before passing on functionality to the input IAppBuilder.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns>An IAppBuilder implementation that will attempt to resolve types against the IWindsorContainer first.</returns>
		public static IAppBuilder UseCastleWindsor(this IAppBuilder builder)
		{
			return builder.UseCastleWindsor(new WindsorContainer());
		}

		public static IAppBuilder UseCastleWindsor(this IAppBuilder builder, Action<IWindsorContainer> configure)
		{
			return builder.UseCastleWindsor(new WindsorContainer(), configure);
		}

		public static IAppBuilder UseCastleWindsor(this IAppBuilder builder, IWindsorContainer container)
		{
			return builder
				.UseDependencyInjectionContainer(container, (c, type) => c.Resolve(type))
				.UseScopeResolution(container.AsScopeResolver());
		}

		public static IAppBuilder UseCastleWindsor(this IAppBuilder builder, IWindsorContainer container, Action<IWindsorContainer> configure)
		{
			return builder
				.UseDependencyInjectionContainer(container, (c, type) => c.Resolve(type))
				.UseScopeResolution(container.AsScopeResolver())
				.ConfigureDependencyInjectionContainer<IWindsorContainer>(configure);
		}

		public static IWindsorContainer GetCastleWindsor(this IAppBuilder builder)
		{
			return builder.GetDependencyInjectionContainer<IWindsorContainer>();
		}

		public static IAppBuilder ConfigureCastleWindsor(this IAppBuilder builder, Action<IWindsorContainer> configure)
		{
			return builder.ConfigureDependencyInjectionContainer<IWindsorContainer>(configure);
		}

		public static IScopeResolver AsScopeResolver(this IWindsorContainer container)
		{
			return new WindsorKernelScopeResolver(container.Kernel);
		}
	}
}
