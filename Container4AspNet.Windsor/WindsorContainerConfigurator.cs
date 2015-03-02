namespace Container4AspNet.Windsor
{
	using Castle.Windsor;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class WindsorContainerConfigurator : IContainerConfigurator<WindsorContainer>
	{
		public WindsorContainerConfigurator()
		{
			Resolve = (container, type) => container.Resolve(type);
			ResolveAll = (container, type) => container.ResolveAll(type).Cast<object>().ToArray();
			ScopeFactory = (container) => container.AsScopeResolver();
		}

		public WindsorContainer Container { get; set; }

		public Func<WindsorContainer, Type, object> Resolve { get; set; }

		public Func<WindsorContainer, Type, object[]> ResolveAll { get; set; }

		public Func<WindsorContainer, IScopeResolver> ScopeFactory { get; set; }
	}
}
