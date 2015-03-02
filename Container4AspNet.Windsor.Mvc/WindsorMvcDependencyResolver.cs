namespace Container4AspNet.Windsor.Mvc
{
	using Castle.MicroKernel;
	using Castle.Windsor;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Basic delegating IDependencyResolver for Asp.Net MVC.
	/// This resolver will use IWindsorContainer and if no type can be resolved will
	/// delegate down the chain to the IDependencyResolver provided if any.
	/// </summary>
	public class WindsorMvcDependencyResolver : System.Web.Mvc.IDependencyResolver
	{
		private IWindsorContainer _container;
		private System.Web.Mvc.IDependencyResolver _delegateResolver;

		/// <summary>
		/// Default no-op IDependencyResolver implementation used if no chaining resolver is
		/// provided.
		/// </summary>
		public static readonly System.Web.Mvc.IDependencyResolver NoDependencyResolver = new NullMvcDependencyResolver();

		/// <summary>
		/// Constructs a new WindsorMvcDependencyResolver with the given IWindsorContainer
		/// and using NoDependencyResolver as the chaining delegate.
		/// </summary>
		/// <param name="container">IWindsorContainer</param>
		public WindsorMvcDependencyResolver(IWindsorContainer container)
			: this(container, null)
		{
		}

		/// <summary>
		/// Constructs a new WindsorMvcDependencyResolver with the given IWindsorContainer
		/// and the IDependencyResolver passed in as the delegate.
		/// </summary>
		/// <param name="container">IWindsorContainer</param>
		/// <param name="delegateResolver">IDependencyResolver, if null then NoDependencyResolver will be used instead</param>
		public WindsorMvcDependencyResolver(IWindsorContainer container, System.Web.Mvc.IDependencyResolver delegateResolver)
		{
			this._container = container;
			this._delegateResolver = delegateResolver ?? NoDependencyResolver;
		}

		/// <summary>
		/// Resolves the serviceType
		/// </summary>
		/// <param name="serviceType">Type to be resolved</param>
		/// <returns>implementation of serviceType</returns>
		public object GetService(Type serviceType)
		{
			return this._container.Kernel.HasComponent(serviceType) ? this._container.Resolve(serviceType) : this._delegateResolver.GetService(serviceType);
		}

		/// <summary>
		/// Resolves all instances of the serviceType
		/// </summary>
		/// <param name="serviceType">Type to be resolved</param>
		/// <returns>IEnumerable of implementations of serviceType</returns>
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
	}

	internal class NullMvcDependencyResolver : System.Web.Mvc.IDependencyResolver
	{
		public object GetService(Type serviceType)
		{
			return null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return new object[0];
		}
	}
}
