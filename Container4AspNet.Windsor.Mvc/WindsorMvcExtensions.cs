namespace Container4AspNet.Windsor.Mvc
{
	using Castle.MicroKernel.Registration;
	using Castle.Windsor;
	using System.Web.Mvc;

	public static class WindsorMvcExtensions
	{
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
