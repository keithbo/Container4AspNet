namespace Container4AspNet.Windsor.WebApi
{
	using Castle.Windsor;
	using System;
	using System.Net.Http;
	using System.Web.Http.Controllers;
	using System.Web.Http.Dispatcher;

	public class WindsorHttpControllerActivator : IHttpControllerActivator
	{
		private readonly IWindsorContainer _container;

		public WindsorHttpControllerActivator(IWindsorContainer container)
		{
			this._container = container;
		}

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