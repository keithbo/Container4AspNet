namespace Container4AspNet.Mvc
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// IFilterProvider implementation that adapts all IActionFilter instances in the container
    /// out as Filter instances.
    /// </summary>
    public class MvcFilterProvider : System.Web.Mvc.IFilterProvider
    {
        private readonly IContainerWrapper _containerWrapper;

        /// <summary>
        /// Constructs a new MvcFilterProvider
        /// </summary>
        /// <param name="containerWrapper">IContainerWrapper</param>
        public MvcFilterProvider(IContainerWrapper containerWrapper)
        {
            _containerWrapper = containerWrapper;
        }

        /// <summary>
        /// Gets all Filter instances adapted from IActionFilter instances in IWindsorContainer
        /// </summary>
        /// <param name="controllerContext">ControllerContext</param>
        /// <param name="actionDescriptor">ActionDescriptor</param>
        /// <returns>IEnumerable of Filter instances</returns>
        public IEnumerable<System.Web.Mvc.Filter> GetFilters(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ActionDescriptor actionDescriptor)
        {
            return _containerWrapper.ResolveAll(typeof(System.Web.Mvc.IActionFilter))
                                    .Select(af => new System.Web.Mvc.Filter(af, System.Web.Mvc.FilterScope.First, null));
        }
    }
}
