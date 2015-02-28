namespace Container4AspNet.Configuration
{
	using Castle.MicroKernel.Registration;

	public abstract class WindsorConfigurerInstaller<TConfigurer> : IWindsorInstaller
		where TConfigurer : IConfigurer
	{
		protected abstract void RegisterContainerTypes(Castle.Windsor.IWindsorContainer container);

		public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
		{
			this.RegisterContainerTypes(container);

			// delegate Mvc settings to dependency injected configurations
			foreach (var settingsConfiguration in container.ResolveAll<TConfigurer>())
			{
				settingsConfiguration.Configure();
			}
		}
	}

	public abstract class ConfigurerWindsorInstaller<TConfigurer, TConfigurerContext> : IWindsorInstaller
		where TConfigurer : IConfigurer<TConfigurerContext>
		where TConfigurerContext : class
	{
		protected abstract void RegisterContainerTypes(Castle.Windsor.IWindsorContainer container);

		protected abstract TConfigurerContext GetConfigurerContext(Castle.Windsor.IWindsorContainer container);

		public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
		{
			this.RegisterContainerTypes(container);

			TConfigurerContext context = this.GetConfigurerContext(container);

			// delegate Mvc settings to dependency injected configurations
			foreach (var settingsConfiguration in container.ResolveAll<TConfigurer>())
			{
				settingsConfiguration.Configure(context);
			}
		}
	}
}
