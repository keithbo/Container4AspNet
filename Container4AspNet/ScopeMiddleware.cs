namespace Container4AspNet
{
	using Microsoft.Owin;
	using System.Threading.Tasks;

	/// <summary>
	/// Middleware implementation that manages a scope instance within the middleware pipeline
	/// </summary>
	public class ScopeMiddleware : OwinMiddleware
	{
		private IScopeResolver _resolver;

		/// <summary>
		/// Constructs a new ScopeMiddleware
		/// </summary>
		/// <param name="next">OwinMiddleware</param>
		/// <param name="resolver">IScopeResolver</param>
		public ScopeMiddleware(OwinMiddleware next, IScopeResolver resolver)
			: base(next)
		{
			this._resolver = resolver;
		}

		/// <summary>
		/// Invokes a new scope into the middleware pipeline
		/// </summary>
		/// <param name="context">IOwinContext</param>
		/// <returns>async Task</returns>
		public override async Task Invoke(IOwinContext context)
		{
			using (this._resolver.NewScope())
			{
				await this.Next.Invoke(context);
			}
		}
	}
}
