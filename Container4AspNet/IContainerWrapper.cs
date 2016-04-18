namespace Container4AspNet
{
    /// <summary>
    /// Wrapper abstraction around a container instance to provide necessary access functionality without knowing the
    /// actual container type.
    /// </summary>
    public interface IContainerWrapper : ITypeResolver, IScopeResolverFactory
    {

    }

    /// <summary>
    /// IContainerWrapper with strong typing access to the underlying container instance.
    /// </summary>
    /// <typeparam name="TContainer">Container type</typeparam>
    public interface IContainerWrapper<out TContainer> : IContainerWrapper
    {
        /// <summary>
        /// Container accessor
        /// </summary>
        TContainer Container { get; }
    }
}
