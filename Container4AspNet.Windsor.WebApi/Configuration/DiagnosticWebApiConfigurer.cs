namespace Container4AspNet.Configuration
{
	using System.Web.Http;

	public class DiagnosticWebApiConfigurer : IWebApiConfigurer
	{
		public bool EnableTrace { get; set; }

		public bool IncludeErrorPolicy { get; set; }

		public bool LocalErrorPolicyOnly { get; set; }

		public DiagnosticWebApiConfigurer()
		{
			this.EnableTrace = false;
			this.IncludeErrorPolicy = false;
			this.LocalErrorPolicyOnly = false;
		}

		public void Configure(HttpConfiguration configuration)
		{
			// To disable tracing in your application, please comment out or remove the following line of code
			// For more information, refer to: http://www.asp.net/web-api
			if (this.EnableTrace)
			{
				var traceWriter = configuration.EnableSystemDiagnosticsTracing();
				traceWriter.IsVerbose = true;
				traceWriter.MinimumLevel = System.Web.Http.Tracing.TraceLevel.Debug;
			}

			IncludeErrorDetailPolicy errorPolicy = IncludeErrorDetailPolicy.Never;
			if (this.IncludeErrorPolicy)
			{
				if (this.LocalErrorPolicyOnly)
				{
					errorPolicy = IncludeErrorDetailPolicy.LocalOnly;
				}
				else
				{
					errorPolicy = IncludeErrorDetailPolicy.Always;
				}
			}
			configuration.IncludeErrorDetailPolicy = errorPolicy;
		}
	}
}
