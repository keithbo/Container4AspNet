namespace Container4AspNet.DependencyInjection
{
	using Castle.MicroKernel;
	using Castle.MicroKernel.Lifestyle;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class WindsorWebApiDependencyScope : System.Web.Http.Dependencies.IDependencyScope
	{
		private readonly IKernel container;
		private readonly IDisposable scope;

		public WindsorWebApiDependencyScope(IKernel container)
		{
			this.container = container;
			this.scope = this.container.BeginScope();
		}

		public object GetService(Type serviceType)
		{
			return this.container.HasComponent(serviceType) ? this.container.Resolve(serviceType) : null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return this.container.ResolveAll(serviceType).Cast<object>();
		}

		public void Dispose()
		{
			this.scope.Dispose();
		}
	}
}