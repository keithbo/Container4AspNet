namespace Container4AspNet.Configuration
{
	using Microsoft.Owin.Security.OAuth;
	using System.Web.Http;

	public class OAuthWebApiConfigurer : IWebApiConfigurer
	{
		public void Configure(System.Web.Http.HttpConfiguration configuration)
		{
			// Configure Web API to use only bearer token authentication.
			configuration.SuppressDefaultHostAuthentication();
			configuration.Filters.Add(
				new AuthenticationFilterAdapter(
					new HostAuthenticationFilter(OAuthDefaults.AuthenticationType)));
		}
	}
}
