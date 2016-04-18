namespace Container4AspNet.Windsor
{
    using Castle.Windsor;
    using System.Linq;

    /// <summary>
    /// IContainerConfigurator implementation for registering Castle Windsor
    /// </summary>
    public class WindsorContainerConfigurator : DefaultContainerConfigurator<IWindsorContainer>
    {
        /// <summary>
        /// Constructs a new WindsorContainerConfigurator
        /// </summary>
        public WindsorContainerConfigurator()
        {
            CanResolve = (container, type) => container.Kernel.HasComponent(type);
            Resolve = (container, type) => container.Resolve(type);
            ResolveAll = (container, type) => container.ResolveAll(type).Cast<object>().ToArray();
            Release = (container, instance) => container.Release(instance);
            ScopeFactory = (container) => new WindsorKernelScopeResolver(container.Kernel);
        }
    }
}
