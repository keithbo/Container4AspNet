namespace Container4AspNet.Windsor.WebApi
{
	using Castle.MicroKernel;
	using Castle.Windsor;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// IDependencyResolver implementation that wraps an IWindsorContainer and an optional
	/// IDependencyResolver delegate instance for fallback.
	/// </summary>
	public class WindsorWebApiDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
	{
		private readonly IWindsorContainer _container;
		private readonly System.Web.Http.Dependencies.IDependencyResolver _delegateResolver;

		/// <summary>
		/// Default no-op IDependencyResolver instance
		/// </summary>
		public static readonly System.Web.Http.Dependencies.IDependencyResolver NoDependencyResolver = new NullMvcDependencyResolver();

		/// <summary>
		/// Constructs a new WindsorWebApiDependencyResolver with the NoDependencyResolver as fallback.
		/// </summary>
		/// <param name="container">IWindsorContainer</param>
		public WindsorWebApiDependencyResolver(IWindsorContainer container)
			: this(container, null)
		{
		}

		/// <summary>
		/// Constructs a new WindsorWebApiDependencyResolver with a delegate IDependencyResolver instance
		/// for fallback.
		/// </summary>
		/// <param name="container">IWindsorContainer</param>
		/// <param name="delegateResolver">IDependencyResolver</param>
		public WindsorWebApiDependencyResolver(IWindsorContainer container, System.Web.Http.Dependencies.IDependencyResolver delegateResolver)
		{
			this._container = container;
			this._delegateResolver = delegateResolver ?? NoDependencyResolver;
		}

		/// <summary>
		/// Creates a new IDependencyScope sourced from IWindsorContainer
		/// </summary>
		/// <returns>IDependencyScope</returns>
		public System.Web.Http.Dependencies.IDependencyScope BeginScope()
		{
			return new WindsorWebApiDependencyScope(this._container.Kernel);
		}

		/// <summary>
		/// Resolves a type instance from IWindsorContainer or the fallback resolver if no component is registered
		/// </summary>
		/// <param name="serviceType">Type to be resolved</param>
		/// <returns>Type instance</returns>
		public object GetService(Type serviceType)
		{
			return this._container.Kernel.HasComponent(serviceType) ? this._container.Resolve(serviceType) : this._delegateResolver.GetService(serviceType);
		}

		/// <summary>
		/// Resolves all instances of the specified type from IWindsorContainer or the fallback
		/// resolver if no component is found
		/// </summary>
		/// <param name="serviceType">type to be resolved</param>
		/// <returns>IEnumerable of resolved instances</returns>
		public IEnumerable<object> GetServices(Type serviceType)
		{
			try
			{
				return this._container.ResolveAll(serviceType).Cast<object>();
			}
			catch (ComponentNotFoundException)
			{
				return this._delegateResolver.GetServices(serviceType);
			}
		}

		/// <summary>
		/// Disposes this WindsorWebApiDependencyResolver
		/// </summary>
		public void Dispose()
		{
		}
	}

	internal class NullMvcDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
	{
		public object GetService(Type serviceType)
		{
			return null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return new object[0];
		}

		public System.Web.Http.Dependencies.IDependencyScope BeginScope()
		{
			return null;
		}

		public void Dispose()
		{
		}
	}
}