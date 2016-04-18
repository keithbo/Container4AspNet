namespace Container4AspNet
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Generic type resolver contract
    /// </summary>
    public interface ITypeResolver
    {
        /// <summary>
        /// Check to see if a type can be resolved with Resolve or ResolveAll.
        /// </summary>
        /// <param name="type">Type to test resolution for</param>
        /// <returns>True if resolveable</returns>
        bool CanResolve(Type type);

        /// <summary>
        /// Retrieve an instance of a type.
        /// </summary>
        /// <param name="type">Type to retrieve</param>
        /// <returns>object instance of the specified type</returns>
        object Resolve(Type type);

        /// <summary>
        /// Retrieve all instances of a type.
        /// </summary>
        /// <param name="type">Type to retrieve</param>
        /// <returns>IEnumerable of instances of the specified type</returns>
        IEnumerable<object> ResolveAll(Type type);

        /// <summary>
        /// Release an instance created by this ITypeResolver
        /// </summary>
        /// <param name="instance">instance to release</param>
        void Release(object instance);
    }
}
