namespace Container4AspNet.DependencyInjection
{
	using System;

	public interface ITypeResolver
	{
		object ResolveType(Type type);
	}
}
