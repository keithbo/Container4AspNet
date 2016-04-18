namespace Container4AspNet.Windsor.Mvc
{
    using System;

    /// <summary>
    /// IViewPageActivator implementation for sourcing MVC page construction through IWindsorContainer
    /// </summary>
    public class WindsorMvcViewPageActivator : System.Web.Mvc.IViewPageActivator
    {
        private readonly IContainerWrapper _containerWrapper;

        /// <summary>
        /// Constructs a new WindsorMvcViewPageActivator
        /// </summary>
        /// <param name="containerWrapper">IContainerWrapper</param>
        public WindsorMvcViewPageActivator(IContainerWrapper containerWrapper)
        {
            _containerWrapper = containerWrapper;
        }

        /// <summary>
        /// Creates a new page instance from the given type
        /// </summary>
        /// <param name="controllerContext">ControllerContext</param>
        /// <param name="type">Type of the page to construct</param>
        /// <returns>new instance of the page type</returns>
        public object Create(System.Web.Mvc.ControllerContext controllerContext, Type type)
        {
            return _containerWrapper.Resolve(type);
        }
    }
}
