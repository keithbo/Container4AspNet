namespace Container4AspNet
{
	using System;

	/// <summary>
	/// Generic type resolver contract
	/// </summary>
	public interface ITypeResolver
	{
		/// <summary>
		/// Retrieve a type instance.
		/// </summary>
		/// <param name="type">Type to retrieve</param>
		/// <returns>object instance of the specified type</returns>
		object ResolveType(Type type);
	}
}
