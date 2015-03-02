namespace Container4AspNet.Windsor.Mvc
{
	using Castle.MicroKernel;
	using Castle.Windsor;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class WindsorMvcDependencyResolver : System.Web.Mvc.IDependencyResolver
	{
		private IWindsorContainer _container;
		private System.Web.Mvc.IDependencyResolver _delegateResolver;

		public static readonly System.Web.Mvc.IDependencyResolver NoDependencyResolver = new NullMvcDependencyResolver();

		public WindsorMvcDependencyResolver(IWindsorContainer container)
			: this(container, null)
		{
		}

		public WindsorMvcDependencyResolver(IWindsorContainer container, System.Web.Mvc.IDependencyResolver delegateResolver)
		{
			this._container = container;
			this._delegateResolver = delegateResolver ?? NoDependencyResolver;
		}

		public object GetService(Type serviceType)
		{
			return this._container.Kernel.HasComponent(serviceType) ? this._container.Resolve(serviceType) : this._delegateResolver.GetService(serviceType);
		}

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
