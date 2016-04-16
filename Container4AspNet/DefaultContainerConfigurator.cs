namespace Container4AspNet
{
    using System;

    /// <summary>
    /// A simple generic implementation of IContainerConfigurator
    /// </summary>
    /// <typeparam name="TContainer"></typeparam>
    public class DefaultContainerConfigurator<TContainer> : IContainerConfigurator<TContainer>
    {
        /// <summary>
        /// Gets and sets the container implementation
        /// </summary>
        public TContainer Container { get; set; }

        /// <summary>
        /// Gets and sets the single-instance resolution method
        /// </summary>
        public Func<TContainer, Type, object> Resolve { get; set; }

        /// <summary>
        /// Gets and sets the multi-instance resolution method
        /// </summary>
        public Func<TContainer, Type, object[]> ResolveAll { get; set; }

        /// <summary>
        /// Gets and sets the IScopeResolver factory method.
        /// If scope is not allowed for the implementing container this should be null
        /// </summary>
        public Func<TContainer, IScopeResolver> ScopeFactory { get; set; }
    }
}
