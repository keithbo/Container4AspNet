namespace Container4AspNet.Windsor.Mvc
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// IFilterProvider implementation that adapts all IActionFilter instances in the container
    /// out as Filter instances.
    /// </summary>
    public class WindsorMvcFilterProvider : System.Web.Mvc.IFilterProvider
    {
        private readonly IContainerWrapper _containerWrapper;

        /// <summary>
        /// Constructs a new WindsorMvcFilterProvider
        /// </summary>
        /// <param name="containerWrapper">IContainerWrapper</param>
        public WindsorMvcFilterProvider(IContainerWrapper containerWrapper)
        {
            _containerWrapper = containerWrapper;
        }

        /// <summary>
        /// Gets all Filter instances adapted from IActionFilter instances in IWindsorContainer
        /// </summary>
        /// <param name="controllerContext">ControllerContext</param>
        /// <param name="actionDescriptor">ActionDescriptor</param>
        /// <returns>IEnumerable of Filter instances</returns>
        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            return _containerWrapper.ResolveAll(typeof(IActionFilter))
                                    .Select(af => new Filter(af, FilterScope.First, null));
        }
    }
}
