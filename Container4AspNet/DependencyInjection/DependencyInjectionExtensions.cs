namespace Container4AspNet.DependencyInjection
{
	using Owin;
	using System;

	public static class DependencyInjectionExtensions
	{
		public static IAppBuilder UseDependencyInjectionContainer<TContainer>(this IAppBuilder builder, TContainer container, Func<TContainer, Type, object> resolver)
		{
			IDependencyContainerWrapper<TContainer> wrapper = new DependencyContainerWrapper<TContainer>(container, resolver);
			IAppBuilder dependencyBuilder = new DependencyContainerAppBuilderAdapter<TContainer>(builder, wrapper);
			dependencyBuilder.Properties[Constants.DependencyInjectionProperty] = wrapper;

			return dependencyBuilder;
		}

		public static TContainer GetDependencyInjectionContainer<TContainer>(this IAppBuilder builder)
			where TContainer : class, IDisposable
		{
			return (builder.Properties[Constants.DependencyInjectionProperty] as IDependencyContainerWrapper<TContainer>).Container;
		}

		private static ITypeResolver GetTypeResolver(this IAppBuilder builder)
		{
			return builder.Properties[Constants.DependencyInjectionProperty] as ITypeResolver;
		}

		/// <summary>
		/// Basic configurer delegate extension method for the IAppBuilder itself. The primary purpose for this utility method
		/// is to allow fluent configuration of the IAppBuilder with components and methods that are not themselves fluent in
		/// nature.
		/// </summary>
		/// <param name="builder">The IAppBuilder instance to configure against</param>
		/// <param name="configure">The IAppBuilder configuration delegate to invoke against the provided IAppBuilder</param>
		/// <returns>The IAppBuilder instance</returns>
		public static IAppBuilder Configure(this IAppBuilder builder, Action<IAppBuilder> configure)
		{
			configure.Invoke(builder);

			return builder;
		}

		public static TDependency Resolve<TDependency>(this IAppBuilder builder)
			where TDependency : class
		{
			ITypeResolver resolver = builder.GetTypeResolver();

			return resolver.ResolveType(typeof(TDependency)) as TDependency;
		}

		public static IAppBuilder ConfigureDependency<TDependency>(this IAppBuilder builder, Action<TDependency> configure)
			where TDependency : class
		{
			configure.Invoke(builder.Resolve<TDependency>());

			return builder;
		}

		public static IAppBuilder ConfigureDependency<TDependency>(this IAppBuilder builder, Action<IAppBuilder, TDependency> configure)
			where TDependency : class
		{
			configure.Invoke(builder, builder.Resolve<TDependency>());

			return builder;
		}

		public static IAppBuilder ConfigureDependencyInjectionContainer<TContainer>(this IAppBuilder builder, Action<TContainer> configure)
			where TContainer : class, IDisposable
		{
			configure.Invoke(builder.GetDependencyInjectionContainer<TContainer>());

			return builder;
		}

		public static IAppBuilder UseScopeResolution<TScopeResolver>(this IAppBuilder builder, TScopeResolver resolver)
			where TScopeResolver : IScopeResolver
		{
			return builder.Use<ScopeMiddleware>(resolver);
		}
	}
}
