namespace Container4AspNet
{
    /// <summary>
    /// Contract for retrieving <see cref="IScopeResolver"/>
    /// </summary>
    public interface IScopeResolverFactory
    {
        /// <summary>
        /// Get an IScopeResolver instance.
        /// </summary>
        /// <returns>IScopeResolver</returns>
        IScopeResolver GetScopeResolver();
    }
}
