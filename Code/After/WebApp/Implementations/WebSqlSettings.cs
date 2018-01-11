using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using UntanglingTheBall.SQL;

namespace WebApp.Implementations
{
	public class WebSqlSettings : ISqlSettings
	{
		private readonly IConfigurationRoot configurationRoot = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

		public string GetConnectionString() => configurationRoot.GetConnectionString("PrimaryConnection");

		public string GetCurrentCategory() => configurationRoot.GetSection("AppConfiguration")["Category"];
	}
}
