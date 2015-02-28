namespace Container4AspNet.DependencyInjection
{
	using Castle.MicroKernel;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class WindsorWebApiDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
	{
		private readonly IKernel container;

		public WindsorWebApiDependencyResolver(IKernel container)
		{
			this.container = container;
		}

		public System.Web.Http.Dependencies.IDependencyScope BeginScope()
		{
			return new WindsorWebApiDependencyScope(this.container);
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
		}
	}
}