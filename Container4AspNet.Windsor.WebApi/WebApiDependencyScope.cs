namespace Container4AspNet.Windsor.WebApi
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// IDependencyScope implementation that wraps an IKernel and a single scope
    /// until it is disposed.
    /// </summary>
    public class WebApiDependencyScope : System.Web.Http.Dependencies.IDependencyScope
    {
        private readonly IContainerWrapper _containerWrapper;
        private readonly IDisposable _scope;

        /// <summary>
        /// Constructs a new WebApiDependencyScope from an IContainerWrapper and
        /// takes out a new scope on construction.
        /// </summary>
        /// <param name="containerWrapper">IContainerWrapper</param>
        public WebApiDependencyScope(IContainerWrapper containerWrapper)
        {
            _containerWrapper = containerWrapper;
            _scope = _containerWrapper.GetScopeResolver().NewScope();
        }

        /// <summary>
        /// Resolves a serviceType from the container.
        /// </summary>
        /// <param name="serviceType">Type to be resolved</param>
        /// <returns>Instance of the type resolved from the container</returns>
        public object GetService(Type serviceType)
        {
            return _containerWrapper.CanResolve(serviceType) ? _containerWrapper.Resolve(serviceType) : null;
        }

        /// <summary>
        /// Resolves all instanes of the serviceType from the container.
        /// </summary>
        /// <param name="serviceType">Type to be resolved</param>
        /// <returns>IEnumerable of the service instances</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _containerWrapper.ResolveAll(serviceType);
        }

        /// <summary>
        /// Disposes this WebApiDependencyScope and the underlying kernel scope
        /// taken out at creation.
        /// </summary>
        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}