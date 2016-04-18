namespace Container4AspNet.Windsor.Mvc
{
    using System.Web.Mvc;
    using Owin;

    /// <summary>
    /// Container4AspNet registration extension methods for Asp.Net MVC
    /// </summary>
    public static class WindsorMvcExtensions
    {
        /// <summary>
        /// Maps the current container as the dependency resolver for Asp.Net MVC.
        /// </summary>
        /// <param name="builder">IAppBuilder</param>
        /// <returns>IAppBuilder for chaining</returns>
        public static IAppBuilder ToMvc(this IAppBuilder builder)
        {
            RegisterContainer(builder);
            return builder;
        }

        private static void RegisterContainer(IAppBuilder builder)
        {
            var wrapper = ContainerHelpers.GetContainerWrapper(builder);

            //// Generally it is frowned upon to let components inject the container itself.
            //// These two components are direct extension points for MVC to resolve types and so we
            //// need to bootstrap requests through to the container
            //container.Register(Component.For<IFilterProvider>()
            //    .ImplementedBy<WindsorMvcFilterProvider>().LifestylePerWebRequest()
            //    .DependsOn(Dependency.OnValue<IContainerWrapper>(wrapper)));

            DependencyResolver.SetResolver(new WindsorMvcDependencyResolver(wrapper, DependencyResolver.Current));
        }
    }
}
