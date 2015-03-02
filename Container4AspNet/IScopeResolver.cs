namespace Container4AspNet
{
	using System;

	public interface IScopeResolver : IDisposable
	{
		IDisposable NewScope();
	}
}
