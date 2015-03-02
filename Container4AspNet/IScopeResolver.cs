namespace Container4AspNet
{
	using System;

	/// <summary>
	/// Contract for scope creation.
	/// </summary>
	public interface IScopeResolver : IDisposable
	{
		/// <summary>
		/// Construct a new scope state.
		/// </summary>
		/// <returns></returns>
		IDisposable NewScope();
	}
}
