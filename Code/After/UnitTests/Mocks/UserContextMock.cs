using System;
using UntanglingTheBall.Business.UseCases;

namespace UntanglingTheBall.UnitTests.Mocks
{
	internal class UserContextMock : IUserContext
	{
		internal string Username = String.Empty;

		public string GetUsername() => Username;
	}
}
