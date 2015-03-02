namespace Container4AspNet.Windsor.WebApi
{
	using Castle.MicroKernel;
	using Castle.MicroKernel.Lifestyle;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// IDependencyScope implementation that wraps an IKernel and a single scope
	/// until it is disposed.
	/// </summary>
	public class WindsorWebApiDependencyScope : System.Web.Http.Dependencies.IDependencyScope
	{
		private readonly IKernel container;
		private readonly IDisposable scope;

		/// <summary>
		/// Constructs a new WindsorWebApiDependencyScope from an IKernel and
		/// takes out a new scope on construction.
		/// </summary>
		/// <param name="container">IKernel</param>
		public WindsorWebApiDependencyScope(IKernel container)
		{
			this.container = container;
			this.scope = this.container.BeginScope();
		}

		/// <summary>
		/// Resolves a serviceType from the container.
		/// </summary>
		/// <param name="serviceType">Type to be resolved</param>
		/// <returns>Instance of the type resolved from the container</returns>
		public object GetService(Type serviceType)
		{
			return this.container.HasComponent(serviceType) ? this.container.Resolve(serviceType) : null;
		}

		/// <summary>
		/// Resolves all instanes of the serviceType from the container.
		/// </summary>
		/// <param name="serviceType">Type to be resolved</param>
		/// <returns>IEnumerable of the service instances</returns>
		public IEnumerable<object> GetServices(Type serviceType)
		{
			return this.container.ResolveAll(serviceType).Cast<object>();
		}

		/// <summary>
		/// Disposes this WindsorWebApiDependencyScope and the underlying kernel scope
		/// taken out at creation.
		/// </summary>
		public void Dispose()
		{
			this.scope.Dispose();
		}
	}
}