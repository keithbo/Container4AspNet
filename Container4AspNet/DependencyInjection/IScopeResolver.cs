namespace Container4AspNet.DependencyInjection
{
	using System;

	public interface IScopeResolver : IDisposable
	{
		IDisposable NewScope();
	}
}
