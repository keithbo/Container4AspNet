namespace Container4AspNet
{
    using System;

    /// <summary>
    /// This interface defines the contract required by container implementations for container
    /// registration process and resolution.
    /// </summary>
    /// <typeparam name="TContainer">container implementation type</typeparam>
    public interface IContainerConfigurator<TContainer>
    {
        /// <summary>
        /// Gets and sets the container implementation
        /// </summary>
        /// <remarks>Required. This property cannot be left null</remarks>
        TContainer Container { get; set; }
        /// <summary>
        /// Gets or sets the resolution test method.
        /// </summary>
        /// <remarks>Optional. If left null, resolution test will default to true and always fall through to Resolve or ResolveAll.</remarks>
        Func<TContainer, Type, bool> CanResolve { get; set; }
        /// <summary>
        /// Gets and sets the single-instance resolution method
        /// </summary>
        /// <remarks>Required. This property cannot be left null</remarks>
        Func<TContainer, Type, object> Resolve { get; set; }
        /// <summary>
        /// Gets and sets the multi-instance resolution method
        /// </summary>
        /// <remarks>Required. This property cannot be left null</remarks>
        Func<TContainer, Type, object[]> ResolveAll { get; set; }
        /// <summary>
        /// Gets and sets the instance cleanup method
        /// </summary>
        /// <remarks>Optional. If release capability is not available leave this null</remarks>
        Action<TContainer, object> Release { get; set; }
        /// <summary>
        /// Gets and sets the IScopeResolver factory method.
        /// </summary>
        /// <remarks>Optional. If scope is not allowed for the implementing container this should be null</remarks>
        Func<TContainer, IScopeResolver> ScopeFactory { get; set; }
    }
}
