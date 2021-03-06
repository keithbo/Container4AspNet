﻿namespace Container4AspNet
{
    using Owin;
    using System;

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
        /// <param name="configure">delegate targeted at an explicit IContainerConfigurator implementation that receives a TConfigurator instance</param>
        /// <returns>IAppBuilder pipeline</returns>
        public static IAppBuilder UseContainer<TContainer, TConfigurator>(this IAppBuilder builder, TContainer container, Action<TConfigurator> configure)
            where TConfigurator : IContainerConfigurator<TContainer>, new()
        {
            return UseContainer(builder, container, new TConfigurator(), c => configure((TConfigurator)c));
        }

        /// <summary>
        /// Main entry point for configuring the target IAppBuilder pipeline with the provided dependency container.
        /// This method forces an implementation of IContainerConfigurator to be used as the basis for specifying downline
        /// container specific configuration methods.
        /// </summary>
        /// <typeparam name="TContainer">IoC container type</typeparam>
        /// <param name="builder">IAppBuilder pipeline instance</param>
        /// <param name="container">IoC container implementation to be used</param>
        /// <param name="configure">delegate targeted at IContainerConfigurator implementation of type TConfigurator</param>
        /// <returns>IAppBuilder pipeline</returns>
        public static IAppBuilder UseContainer<TContainer>(this IAppBuilder builder, TContainer container, Action<IContainerConfigurator<TContainer>> configure)
        {
            return UseContainer(builder, container, null, configure);
        }

        /// <summary>
        /// Main entry point for configuring the target IAppBuilder pipeline with the provided dependency container.
        /// This method forces an implementation of IContainerConfigurator to be used as the basis for specifying downline
        /// container specific configuration methods.
        /// </summary>
        /// <typeparam name="TContainer">IoC container type</typeparam>
        /// <param name="builder">IAppBuilder pipeline instance</param>
        /// <param name="container">IoC container implementation to be used</param>
        /// <param name="configurator">IContainerConfigurator implementation to be used in configuration of container</param>
        /// <param name="configure">delegate targeted at IContainerConfigurator implementation for container type TContainer</param>
        /// <returns>IAppBuilder pipeline</returns>
        public static IAppBuilder UseContainer<TContainer>(this IAppBuilder builder, TContainer container, IContainerConfigurator<TContainer> configurator, Action<IContainerConfigurator<TContainer>> configure)
        {
            configurator = configurator ?? new DefaultContainerConfigurator<TContainer>();
            configurator.Container = container;
            if (configure != null)
            {
                configure(configurator);
            }

            ContainerHelpers.ValidateConfigurator(configurator);

            IContainerWrapper<TContainer> wrapper = new ContainerWrapper<TContainer>(configurator);
            IAppBuilder dependencyBuilder = new ContainerAppBuilderAdapter<TContainer>(builder, wrapper);
            dependencyBuilder.Properties[Constants.DependencyInjectionProperty] = wrapper;

            if (configurator.ScopeFactory != null)
            {
                dependencyBuilder = dependencyBuilder.UseScopeResolution(configurator.ScopeFactory(container));
            }

            return dependencyBuilder;
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

        /// <summary>
        /// Resolves a target type from the registered dependency container
        /// </summary>
        /// <typeparam name="TDependency">Type of the dependency to resolve</typeparam>
        /// <param name="builder">IAppBuilder pipeline</param>
        /// <returns>Instance of the dependency type or null</returns>
        public static TDependency Resolve<TDependency>(this IAppBuilder builder)
            where TDependency : class
        {
            ITypeResolver resolver = ContainerHelpers.GetTypeResolver(builder);

            return resolver.Resolve(typeof(TDependency)) as TDependency;
        }

        /// <summary>
        /// Executes a configuration delegate for the target dependency instance
        /// </summary>
        /// <typeparam name="TDependency">Type of the dependency to resolve</typeparam>
        /// <param name="builder">IAppBuilder pipeline</param>
        /// <param name="configure">delegate for resolving the instance of type TDependency</param>
        /// <returns>IAppBuilder pipeline</returns>
        public static IAppBuilder ConfigureDependency<TDependency>(this IAppBuilder builder, Action<TDependency> configure)
            where TDependency : class
        {
            configure.Invoke(builder.Resolve<TDependency>());

            return builder;
        }

        /// <summary>
        /// Executes a configuration delegate for the target dependency instance
        /// </summary>
        /// <typeparam name="TDependency">Type of the dependency to resolve</typeparam>
        /// <param name="builder">IAppBuilder pipeline</param>
        /// <param name="configure">delegate for resolving the instance of type TDependency with the passed in IAppBuilder instance</param>
        /// <returns>IAppBuilder pipeline</returns>
        public static IAppBuilder ConfigureDependency<TDependency>(this IAppBuilder builder, Action<IAppBuilder, TDependency> configure)
            where TDependency : class
        {
            configure.Invoke(builder, builder.Resolve<TDependency>());

            return builder;
        }

        /// <summary>
        /// Executes a configuration delegate for the dependency container
        /// </summary>
        /// <typeparam name="TContainer">Type of the dependency container previously registered</typeparam>
        /// <param name="builder">IAppBuilder pipeline</param>
        /// <param name="configure">delegate for configuring the dependency container</param>
        /// <returns>IAppBuilder pipeline</returns>
        public static IAppBuilder ConfigureContainer<TContainer>(this IAppBuilder builder, Action<TContainer> configure)
            where TContainer : class, IDisposable
        {
            configure.Invoke(ContainerHelpers.GetContainer<TContainer>(builder));
            return builder;
        }

        /// <summary>
        /// Registers an IScopeResolver of type TScopeResolver into the IAppBuilder pipeline.
        /// </summary>
        /// <typeparam name="TScopeResolver">Implementation of type IScopeResolver</typeparam>
        /// <param name="builder">IAppBuilder pipeline</param>
        /// <param name="resolver">TScopeResolver instance</param>
        /// <returns>IAppBuilder pipeline</returns>
        public static IAppBuilder UseScopeResolution<TScopeResolver>(this IAppBuilder builder, TScopeResolver resolver)
            where TScopeResolver : IScopeResolver
        {
            return builder.Use<ScopeMiddleware>(resolver);
        }
    }
}
