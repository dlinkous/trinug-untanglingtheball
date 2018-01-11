using System;
using System.IO;
using UntanglingTheBall.Files;

namespace WebApp.Implementations
{
	public class WebFileSettings : IFileSettings
	{
		public string GetRoot() => Directory.GetCurrentDirectory();
	}
}
