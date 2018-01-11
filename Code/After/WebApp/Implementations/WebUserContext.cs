using System;
using UntanglingTheBall.Business.UseCases;

namespace WebApp.Implementations
{
	public class WebUserContext : IUserContext
	{
		public string GetUsername() => Environment.GetEnvironmentVariable("Username");
	}
}
