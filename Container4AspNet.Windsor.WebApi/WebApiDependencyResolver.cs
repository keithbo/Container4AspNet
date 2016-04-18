namespace Container4AspNet.Windsor.WebApi
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// IDependencyResolver implementation that wraps an <see cref="IContainerWrapper"/> and an optional
    /// IDependencyResolver delegate instance for fallback.
    /// </summary>
    public class WebApiDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IContainerWrapper _containerWrapper;
        private readonly System.Web.Http.Dependencies.IDependencyResolver _delegateResolver;

        /// <summary>
        /// Default no-op IDependencyResolver instance
        /// </summary>
        public static readonly System.Web.Http.Dependencies.IDependencyResolver NoDependencyResolver = new NullWebApiDependencyResolver();

        /// <summary>
        /// Constructs a new WebApiDependencyResolver with the NoDependencyResolver as fallback.
        /// </summary>
        /// <param name="containerWrapper">IContainerWrapper</param>
        public WebApiDependencyResolver(IContainerWrapper containerWrapper)
            : this(containerWrapper, null)
        {
        }

        /// <summary>
        /// Constructs a new WebApiDependencyResolver with a delegate IDependencyResolver instance for fallback.
        /// </summary>
        /// <param name="containerWrapper">IContainerWrapper</param>
        /// <param name="delegateResolver">IDependencyResolver</param>
        public WebApiDependencyResolver(IContainerWrapper containerWrapper, System.Web.Http.Dependencies.IDependencyResolver delegateResolver)
        {
            _containerWrapper = containerWrapper;
            _delegateResolver = delegateResolver ?? NoDependencyResolver;
        }

        /// <summary>
        /// Creates a new IDependencyScope sourced from IWindsorContainer
        /// </summary>
        /// <returns>IDependencyScope</returns>
        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return new WebApiDependencyScope(_containerWrapper);
        }

        /// <summary>
        /// Resolves a type instance from IWindsorContainer or the fallback resolver if no component is registered
        /// </summary>
        /// <param name="serviceType">Type to be resolved</param>
        /// <returns>Type instance</returns>
        public object GetService(Type serviceType)
        {
            return _containerWrapper.CanResolve(serviceType) ? _containerWrapper.Resolve(serviceType) : _delegateResolver.GetService(serviceType);
        }

        /// <summary>
        /// Resolves all instances of the specified type from IWindsorContainer or the fallback
        /// resolver if no component is found
        /// </summary>
        /// <param name="serviceType">type to be resolved</param>
        /// <returns>IEnumerable of resolved instances</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _containerWrapper.CanResolve(serviceType) ? _containerWrapper.ResolveAll(serviceType) : _delegateResolver.GetServices(serviceType);
        }

        /// <summary>
        /// Disposes this WebApiDependencyResolver
        /// </summary>
        public void Dispose()
        {
        }
    }
}