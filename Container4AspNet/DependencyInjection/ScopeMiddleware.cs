namespace Container4AspNet.DependencyInjection
{
	using Microsoft.Owin;
	using System.Threading.Tasks;

	public class ScopeMiddleware : OwinMiddleware
	{
		private IScopeResolver _resolver;

		public ScopeMiddleware(OwinMiddleware next, IScopeResolver resolver)
			: base(next)
		{
			this._resolver = resolver;
		}

		public override async Task Invoke(IOwinContext context)
		{
			using (this._resolver.NewScope())
			{
				await this.Next.Invoke(context);
			}
		}
	}
}
