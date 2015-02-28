namespace Container4AspNet.Configuration
{
	using Newtonsoft.Json.Converters;
	using System.Web.Http;

	public class JsonWebApiConfigurer : IWebApiConfigurer
	{
		public void Configure(HttpConfiguration configuration)
		{
			configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
		}
	}
}
