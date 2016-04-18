namespace Container4AspNet.Windsor.Mvc
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Basic delegating IDependencyResolver for Asp.Net MVC.
    /// This resolver will use IWindsorContainer and if no type can be resolved will
    /// delegate down the chain to the IDependencyResolver provided if any.
    /// </summary>
    public class WindsorMvcDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        private readonly IContainerWrapper _containerWrapper;
        private readonly System.Web.Mvc.IDependencyResolver _delegateResolver;

        /// <summary>
        /// Default no-op IDependencyResolver implementation used if no chaining resolver is
        /// provided.
        /// </summary>
        public static readonly System.Web.Mvc.IDependencyResolver NoDependencyResolver = new NullMvcDependencyResolver();

        /// <summary>
        /// Constructs a new WindsorMvcDependencyResolver with the given IWindsorContainer
        /// and using NoDependencyResolver as the chaining delegate.
        /// </summary>
        /// <param name="containerWrapper">IContainerWrapper</param>
        public WindsorMvcDependencyResolver(IContainerWrapper containerWrapper)
            : this(containerWrapper, null)
        {
        }

        /// <summary>
        /// Constructs a new WindsorMvcDependencyResolver with the given IWindsorContainer
        /// and the IDependencyResolver passed in as the delegate.
        /// </summary>
        /// <param name="containerWrapper">IContainerWrapper</param>
        /// <param name="delegateResolver">IDependencyResolver, if null then NoDependencyResolver will be used instead</param>
        public WindsorMvcDependencyResolver(IContainerWrapper containerWrapper, System.Web.Mvc.IDependencyResolver delegateResolver)
        {
            _containerWrapper = containerWrapper;
            _delegateResolver = delegateResolver ?? NoDependencyResolver;
        }

        /// <summary>
        /// Resolves the serviceType
        /// </summary>
        /// <param name="serviceType">Type to be resolved</param>
        /// <returns>implementation of serviceType</returns>
        public object GetService(Type serviceType)
        {
            return _containerWrapper.CanResolve(serviceType) ? _containerWrapper.Resolve(serviceType) : _delegateResolver.GetService(serviceType);
        }

        /// <summary>
        /// Resolves all instances of the serviceType
        /// </summary>
        /// <param name="serviceType">Type to be resolved</param>
        /// <returns>IEnumerable of implementations of serviceType</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _containerWrapper.CanResolve(serviceType) ? _containerWrapper.ResolveAll(serviceType) : _delegateResolver.GetServices(serviceType);
        }
    }
}
