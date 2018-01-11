using System;

namespace UntanglingTheBall.SQL
{
	public interface ISqlSettings
	{
		string GetConnectionString();
		string GetCurrentCategory();
	}
}
