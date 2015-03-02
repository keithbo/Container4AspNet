namespace Container4AspNet.Windsor.Mvc
{
	using Castle.MicroKernel.Registration;
	using Castle.Windsor;
	using System.Web.Mvc;

	/// <summary>
	/// Castle Windsor container registration extension methods for Asp.Net MVC
	/// </summary>
	public static class WindsorMvcExtensions
	{
		/// <summary>
		/// Maps Windsor to be the dependency resolver for Asp.Net MVC.
		/// </summary>
		/// <param name="configurator">WindSorContainerConfigurator</param>
		/// <returns>WindsorContainerConfigurator for chaining</returns>
		public static WindsorContainerConfigurator ToMvc(this WindsorContainerConfigurator configurator)
		{
			RegisterContainer(configurator.Container);
			return configurator;
		}

		private static void RegisterContainer(IWindsorContainer container)
		{
			// Generally it is frowned upon to let components inject the container itself.
			// These two components are direct extension points for MVC to resolve types and so we
			// need to bootstrap requests through to the IWindsorContainer
			container.Register(Component.For<IFilterProvider>()
				.ImplementedBy<WindsorMvcFilterProvider>().LifestylePerWebRequest()
				.DependsOn(Dependency.OnValue<IWindsorContainer>(container)));
			//container.Register(Component.For<IViewPageActivator>()
			//	.ImplementedBy<WindsorMvcViewPageActivator>().LifestylePerWebRequest()
			//	.DependsOn(Dependency.OnValue<IWindsorContainer>(container)));

			DependencyResolver.SetResolver(new WindsorMvcDependencyResolver(container, DependencyResolver.Current));
		}
	}
}
