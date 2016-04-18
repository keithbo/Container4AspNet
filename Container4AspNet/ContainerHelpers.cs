using System;

namespace Container4AspNet
{
    using Owin;

    public static class ContainerHelpers
    {
        public static ITypeResolver GetTypeResolver(IAppBuilder builder)
        {
            return builder.Properties[Constants.DependencyInjectionProperty] as ITypeResolver;
        }

        public static IScopeResolverFactory GetScopeResolverFactory(IAppBuilder builder)
        {
            return builder.Properties[Constants.DependencyInjectionProperty] as IScopeResolverFactory;
        }

        public static IContainerWrapper GetContainerWrapper(IAppBuilder builder)
        {
            return builder.Properties[Constants.DependencyInjectionProperty] as IContainerWrapper;
        }

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
