namespace Container4AspNet.WebApi
{
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using Owin;

    /// <summary>
    /// Extension methods specific to WebApi integration into Container4AspNet
    /// </summary>
    public static class WebApiExtensions
    {
        /// <summary>
        /// Delegates the currently registered container as dependency resolver for WebApi
        /// </summary>
        /// <param name="builder">IAppBuilder</param>
        /// <returns>IAppBuilder for chaining</returns>
        public static IAppBuilder ToWebApi(this IAppBuilder builder)
        {
            return ToWebApi(builder, builder.Resolve<HttpConfiguration>());
        }

        /// <summary>
        /// Delegates the currently registered container as the dependency resolver for WebApi
        /// with an external HttpConfiguration
        /// </summary>
        /// <param name="builder">IAppBuilder</param>
        /// <param name="configuration">HttpConfiguration</param>
        /// <returns>WindsorContainerConfigurator for chaining</returns>
        public static IAppBuilder ToWebApi(this IAppBuilder builder, HttpConfiguration configuration)
        {
            RegisterContainer(builder, configuration);
            return builder;
        }

        /// <summary>
        /// Extension overload to UseWebApi that forces use of the dependency container to retrieve the active HttpConfiguration to then
        /// pass along the normal UseWebApi(HttpConfiguration) execution path.
        /// </summary>
        /// <param name="builder">IAppBuilder</param>
        /// <returns>IAppBuilder for chaining</returns>
        public static IAppBuilder UseWebApi(this IAppBuilder builder)
        {
            var config = builder.Resolve<HttpConfiguration>();
            RegisterContainer(builder, config);
            return builder.UseWebApi(config);
        }

        private static void RegisterContainer(IAppBuilder builder, HttpConfiguration configuration)
        {
            configuration.Properties[Container4AspNet.Constants.DependencyInjectionProperty] = ContainerHelpers.GetContainer(builder);
            var wrapper = ContainerHelpers.GetContainerWrapper(builder);

            // shim the container instantiated IDependencyResolver to enable DI during ASP.NET dependency resolution
            //container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            configuration.DependencyResolver = new WebApiDependencyResolver(wrapper);

            // shim the container instantiated IHttpControllerActivator to enable DI during controller creation
            configuration.Services.Replace(typeof(IHttpControllerActivator), new HttpControllerActivator(wrapper));
        }
    }
}
