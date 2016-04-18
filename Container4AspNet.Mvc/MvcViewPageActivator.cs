namespace Container4AspNet.Mvc
{
    using System;

    /// <summary>
    /// IViewPageActivator implementation for sourcing MVC page construction through IWindsorContainer
    /// </summary>
    public class MvcViewPageActivator : System.Web.Mvc.IViewPageActivator
    {
        private readonly IContainerWrapper _containerWrapper;

        /// <summary>
        /// Constructs a new MvcViewPageActivator
        /// </summary>
        /// <param name="containerWrapper">IContainerWrapper</param>
        public MvcViewPageActivator(IContainerWrapper containerWrapper)
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
