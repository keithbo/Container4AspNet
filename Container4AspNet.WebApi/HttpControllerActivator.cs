namespace Container4AspNet.WebApi
{
    using System;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;

    /// <summary>
    /// IHttpControllerActivator implementation that delegates IHttpController creation to
    /// an IWindsorContainer instance.
    /// </summary>
    public class HttpControllerActivator : IHttpControllerActivator
    {
        private readonly IContainerWrapper _containerWrapper;

        /// <summary>
        /// Construct a new HttpControllerActivator
        /// </summary>
        /// <param name="containerWrapper">IContainerWrapper</param>
        public HttpControllerActivator(IContainerWrapper containerWrapper)
        {
            _containerWrapper = containerWrapper;
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
            var controller = (IHttpController)_containerWrapper.Resolve(controllerType);

            request.RegisterForDispose(new Release(() => _containerWrapper.Release(controller)));

            return controller;
        }

        private sealed class Release : IDisposable
        {
            private readonly Action _release;

            public Release(Action release)
            {
                _release = release;
            }

            public void Dispose()
            {
                _release();
            }
        }
    }
}