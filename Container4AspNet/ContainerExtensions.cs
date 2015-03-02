namespace Container4AspNet
{
	using Owin;
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;

	/// <summary>
	/// Core extension methods for Container4AspNet.
	/// These methods are geared towards configuration and retrieval of IAppBuilder so Asp.Net can make use of the provided dependency container
	/// </summary>
	public static class ContainerExtensions
	{
		/// <summary>
		/// Main entry point for configuring the target IAppBuilder pipeline with the provided dependency container.
		/// This method forces an implementation of IContainerConfigurator to be used as the basis for specifying downline
		/// container specific configuration methods.
		/// </summary>
		/// <typeparam name="TContainer">IoC container type</typeparam>
		/// <typeparam name="TConfigurator">IContainerConfigurator implementing type</typeparam>
		/// <param name="builder">IAppBuilder pipeline instance</param>
		/// <param name="container">IoC container implementation to be used</param>
		/// <param name="configure">delegate targeted at IContainerConfigurator implementation of type TConfigurator</param>
		/// <returns>IAppBuilder pipeline</returns>
		public static IAppBuilder UseContainer<TContainer, TConfigurator>(this IAppBuilder builder, TContainer container, Action<TConfigurator> configure)
			where TConfigurator : IContainerConfigurator<TContainer>, new()
		{
			var configurator = new TConfigurator();
			configurator.Container = container;
			configure(configurator);

			ValidateConfigurator<TContainer, TConfigurator>(configurator);

			IContainerWrapper<TContainer> wrapper = new ContainerWrapper<TContainer>(configurator);
			IAppBuilder dependencyBuilder = new ContainerAppBuilderAdapter<TContainer>(builder, wrapper);
			dependencyBuilder.Properties[Constants.DependencyInjectionProperty] = wrapper;

			if (configurator.ScopeFactory != null)
			{
				dependencyBuilder = dependencyBuilder.UseScopeResolution(configurator.ScopeFactory(container));
			}

			return dependencyBuilder;
		}

		private static void ValidateConfigurator<TContainer, TConfigurator>(TConfigurator configurator)
			where TConfigurator : IContainerConfigurator<TContainer>
		{
			if (configurator.Container == null)
			{
				throw new ArgumentException("configurator");
			}
			if (configurator.Resolve == null)
			{
				throw new ArgumentException("configurator");
			}
			if (configurator.ResolveAll == null)
			{
				throw new ArgumentException("configurator");
			}
		}

		public static TContainer GetContainer<TContainer>(this IAppBuilder builder)
			where TContainer : class, IDisposable
		{
			return (builder.Properties[Constants.DependencyInjectionProperty] as IContainerWrapper<TContainer>).Container;
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

		public static IAppBuilder ConfigureContainer<TContainer>(this IAppBuilder builder, Action<TContainer> configure)
			where TContainer : class, IDisposable
		{
			configure.Invoke(builder.GetContainer<TContainer>());

			return builder;
		}

		public static IAppBuilder UseScopeResolution<TScopeResolver>(this IAppBuilder builder, TScopeResolver resolver)
			where TScopeResolver : IScopeResolver
		{
			return builder.Use<ScopeMiddleware>(resolver);
		}
	}
}
