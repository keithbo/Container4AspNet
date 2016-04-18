namespace Container4AspNet.Windsor
{
    using System;
    using Castle.Windsor;
    using Owin;

    /// <summary>
    /// Extension methods for Castle Windsor integration
    /// </summary>
    public static class WindsorExtensions
    {
        /// <summary>
        /// Configure IAppBuilder pipeline to use Castle Windsor IoC container.
        /// This method creates a default <see cref="IWindsorContainer"/> instance to be registered.
        /// </summary>
        /// <param name="builder">IAppBuilder pipeline instance</param>
        /// <param name="configure">Configuration delegate to be called with appropriate <see cref="IContainerConfigurator{IWindsorContainer}"/></param>
        /// <returns>IAppBuilder pipeline</returns>
        public static IAppBuilder UseWindsor(this IAppBuilder builder, Action<IContainerConfigurator<IWindsorContainer>> configure)
        {
            return UseWindsor(builder, new WindsorContainer(), configure);
        }

        /// <summary>
        /// Configure <see cref="IAppBuilder"/> pipeline to use Castle Windsor IoC container.
        /// </summary>
        /// <param name="builder"><see cref="IAppBuilder"/> pipeline instance</param>
        /// <param name="container"><see cref="IWindsorContainer"/> instance to be registered</param>
        /// <param name="configure">Configuration delegate to be called with appropriate <see cref="IContainerConfigurator{IWindsorContainer}"/></param>
        /// <returns>IAppBuilder pipeline</returns>
        public static IAppBuilder UseWindsor(this IAppBuilder builder, IWindsorContainer container, Action<IContainerConfigurator<IWindsorContainer>> configure)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            return builder.UseContainer(container, new WindsorContainerConfigurator(), configure);
        }
    }
}
