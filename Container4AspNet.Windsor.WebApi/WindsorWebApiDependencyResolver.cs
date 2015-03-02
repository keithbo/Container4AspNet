namespace Container4AspNet.Windsor.WebApi
{
	using Castle.MicroKernel;
	using Castle.Windsor;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class WindsorWebApiDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
	{
		private readonly IWindsorContainer _container;
		private readonly System.Web.Http.Dependencies.IDependencyResolver _delegateResolver;

		public static readonly System.Web.Http.Dependencies.IDependencyResolver NoDependencyResolver = new NullMvcDependencyResolver();

		public WindsorWebApiDependencyResolver(IWindsorContainer container)
			: this(container, null)
		{
		}

		public WindsorWebApiDependencyResolver(IWindsorContainer container, System.Web.Http.Dependencies.IDependencyResolver delegateResolver)
		{
			this._container = container;
			this._delegateResolver = delegateResolver ?? NoDependencyResolver;
		}

		public System.Web.Http.Dependencies.IDependencyScope BeginScope()
		{
			return new WindsorWebApiDependencyScope(this._container.Kernel);
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