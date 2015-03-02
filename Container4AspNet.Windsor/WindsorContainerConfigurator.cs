namespace Container4AspNet.Windsor
{
	using Castle.Windsor;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// IContainerConfigurator implementation for registering Castle Windsor
	/// </summary>
	public class WindsorContainerConfigurator : IContainerConfigurator<WindsorContainer>
	{
		/// <summary>
		/// Constructs a new WindsorContainerConfigurator
		/// </summary>
		public WindsorContainerConfigurator()
		{
			Resolve = (container, type) => container.Resolve(type);
			ResolveAll = (container, type) => container.ResolveAll(type).Cast<object>().ToArray();
			ScopeFactory = (container) => container.AsScopeResolver();
		}

		/// <summary>
		/// Gets or sets the WindsorContainer instance.
		/// </summary>
		public WindsorContainer Container { get; set; }

		/// <summary>
		/// Gets or sets the single-instance resolution method.
		/// Default is IWindsorContainer.Resolve
		/// </summary>
		public Func<WindsorContainer, Type, object> Resolve { get; set; }

		/// <summary>
		/// Gets or sets the multi-instance resolution method.
		/// Default is IWindsorContainer.ResolveAll
		/// </summary>
		public Func<WindsorContainer, Type, object[]> ResolveAll { get; set; }

		/// <summary>
		/// Gets or sets the IScopeResolver factory method.
		/// Defaults to IKernel.BeginScope
		/// </summary>
		public Func<WindsorContainer, IScopeResolver> ScopeFactory { get; set; }
	}
}
