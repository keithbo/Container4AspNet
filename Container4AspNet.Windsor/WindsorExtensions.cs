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
        public static IAppBuilder UseWindsor(this IAppBuilder builder, Action<IContainerConfigurator<IWindsorContainer>> configure)
        {
            return UseWindsor(builder, new WindsorContainer(), configure);
        }

        public static IAppBuilder UseWindsor(this IAppBuilder builder, IWindsorContainer container, Action<IContainerConfigurator<IWindsorContainer>> configure)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            return builder.UseContainer(container, new WindsorContainerConfigurator(), configure);
        }

		/// <summary>
		/// Constructs a new IScopeResolver for a given IWindsorContainer.
		/// </summary>
		/// <see cref="WindsorKernelScopeResolver"/>
		/// <param name="container">IWindsorContainer</param>
		/// <returns>WindsorKernelScopeResolver</returns>
		public static WindsorKernelScopeResolver AsScopeResolver(this IWindsorContainer container)
		{
			return new WindsorKernelScopeResolver(container.Kernel);
		}
	}
}
