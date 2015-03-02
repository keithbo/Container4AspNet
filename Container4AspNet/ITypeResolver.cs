namespace Container4AspNet
{
	using System;

	public interface ITypeResolver
	{
		object ResolveType(Type type);
	}
}
