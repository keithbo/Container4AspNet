namespace Container4AspNet.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Web.Http.Filters;

	public class AuthenticationFilterAdapter : IAuthenticationFilter, IFilter
	{
		private readonly IAuthenticationFilter _source;

		public AuthenticationFilterAdapter(IAuthenticationFilter source)
		{
			this._source = source;
		}

		public Task AuthenticateAsync(HttpAuthenticationContext context, System.Threading.CancellationToken cancellationToken)
		{
			return this._source.AuthenticateAsync(context, cancellationToken);
		}

		public Task ChallengeAsync(HttpAuthenticationChallengeContext context, System.Threading.CancellationToken cancellationToken)
		{
			return this._source.ChallengeAsync(context, cancellationToken);
		}

		public bool AllowMultiple
		{
			get
			{
				return this._source.AllowMultiple;
			}
		}
	}
}
