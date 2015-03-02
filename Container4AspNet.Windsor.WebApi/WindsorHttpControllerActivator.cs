namespace Container4AspNet.Windsor.WebApi
{
	using Castle.Windsor;
	using System;
	using System.Net.Http;
	using System.Web.Http.Controllers;
	using System.Web.Http.Dispatcher;

	/// <summary>
	/// IHttpControllerActivator implementation that delegates IHttpController creation to
	/// an IWindsorContainer instance.
	/// </summary>
	public class WindsorHttpControllerActivator : IHttpControllerActivator
	{
		private readonly IWindsorContainer _container;

		/// <summary>
		/// Construct a new WindsorHttpControllerActivator
		/// </summary>
		/// <param name="container">IWindsorContainer</param>
		public WindsorHttpControllerActivator(IWindsorContainer container)
		{
			this._container = container;
		}

		/// <summary>
		/// Creates a new IHttpController instance from IWindsorContainer and registers the instance for
		/// disposal upon release of the controller.
		/// </summary>
		/// <param name="request">HttpRequestMessage</param>
		/// <param name="controllerDescriptor">HttpControllerDescriptor</param>
		/// <param name="controllerType">Type to be resolved</param>
		/// <returns>IHttpController instance</returns>
		public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
		{
			var controller = (IHttpController)this._container.Resolve(controllerType);

			request.RegisterForDispose(new Release(() => _container.Release(controller)));

			return controller;
		}

		private sealed class Release : IDisposable
		{
			private readonly Action _release;

			public Release(Action release)
			{
				this._release = release;
			}

			public void Dispose()
			{
				this._release();
			}
		}
	}
}