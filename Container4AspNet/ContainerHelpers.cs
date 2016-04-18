namespace Container4AspNet
{
    using System;
    using Owin;

    /// <summary>
    /// Helper methods for interacting with Container4AspNet core elements
    /// </summary>
    public static class ContainerHelpers
    {
        /// <summary>
        /// Get an <see cref="ITypeResolver"/> stored in a specific <see cref="IAppBuilder"/> instance.
        /// </summary>
        /// <param name="builder">IAppBuilder</param>
        /// <returns>ITypeResolver</returns>
        public static ITypeResolver GetTypeResolver(IAppBuilder builder)
        {
            return builder.Properties[Constants.DependencyInjectionProperty] as ITypeResolver;
        }

        /// <summary>
        /// Get an <see cref="IScopeResolverFactory"/> stored in a specific <see cref="IAppBuilder"/> instance.
        /// </summary>
        /// <param name="builder">IAppBuilder</param>
        /// <returns>IScopeResolverFactory</returns>
        public static IScopeResolverFactory GetScopeResolverFactory(IAppBuilder builder)
        {
            return builder.Properties[Constants.DependencyInjectionProperty] as IScopeResolverFactory;
        }

        /// <summary>
        /// Get an un-typed <see cref="IContainerWrapper"/> stored in a specific <see cref="IAppBuilder"/> instance.
        /// </summary>
        /// <param name="builder">IAppBuilder</param>
        /// <returns>IContainerWrapper</returns>
        public static IContainerWrapper GetContainerWrapper(IAppBuilder builder)
        {
            return builder.Properties[Constants.DependencyInjectionProperty] as IContainerWrapper;
        }

        /// <summary>
        /// Get a strong-typed <see cref="IContainerWrapper{TContainer}"/> stored in a specific <see cref="IAppBuilder"/> instance.
        /// </summary>
        /// <typeparam name="TContainer">Expected container type</typeparam>
        /// <param name="builder">IAppBuilder</param>
        /// <returns>IContainerWrapper{TContainer}</returns>
        public static IContainerWrapper<TContainer> GetContainerWrapper<TContainer>(IAppBuilder builder)
        {
            return builder.Properties[Constants.DependencyInjectionProperty] as IContainerWrapper<TContainer>;
        }

        /// <summary>
        /// Retrieves the currently registered container
        /// </summary>
        /// <param name="builder">IAppBuilder</param>
        /// <returns></returns>
        public static object GetContainer(IAppBuilder builder)
        {
            var wrapper = GetContainerWrapper<object>(builder);

            return wrapper != null ? wrapper.Container : null;
        }

        /// <summary>
        /// Retrieves the currently registered container
        /// </summary>
        /// <typeparam name="TContainer">The container type</typeparam>
        /// <param name="builder">IAppBuilder</param>
        /// <returns></returns>
        public static TContainer GetContainer<TContainer>(IAppBuilder builder)
            where TContainer : class, IDisposable
        {
            var wrapper = GetContainerWrapper<TContainer>(builder);

            return wrapper != null ? wrapper.Container : default(TContainer);
        }

        /// <summary>
        /// Check validation rules against a specific <see cref="IContainerConfigurator{TContainer}"/> instance.
        /// </summary>
        /// <typeparam name="TContainer">Container type</typeparam>
        /// <param name="configurator">IContainerConfigurator{TContainer} instance</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <see cref="IContainerConfigurator{TContainer}.Container"/> property is empty.
        /// Thrown if <see cref="IContainerConfigurator{TContainer}.Resolve"/> property is empty.
        /// Thrown if <see cref="IContainerConfigurator{TContainer}.ResolveAll"/> property is empty.
        /// </exception>
        public static void ValidateConfigurator<TContainer>(IContainerConfigurator<TContainer> configurator)
        {
            if (configurator.Container == null)
            {
                throw new ArgumentException("Container cannot be null", "configurator");
            }
            if (configurator.Resolve == null)
            {
                throw new ArgumentException("Resolve delegate cannot be null", "configurator");
            }
            if (configurator.ResolveAll == null)
            {
                throw new ArgumentException("ResolveAll delegate cannot be null", "configurator");
            }
        }
    }
}
