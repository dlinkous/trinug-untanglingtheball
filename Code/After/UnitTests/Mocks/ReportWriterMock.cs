using System;
using System.Collections.Generic;
using UntanglingTheBall.Business.Common;
using UntanglingTheBall.Business.UseCases;

namespace UntanglingTheBall.UnitTests.Mocks
{
	internal class ReportWriterMock : IReportWriter
	{
		internal class WriteCall
		{
			internal string Username;
			internal ReportChunk Chunk;
		}

		internal List<WriteCall> WriteCalls = new List<WriteCall>();

		public void Write(string username, ReportChunk chunk) => WriteCalls.Add(new WriteCall() { Username = username, Chunk = chunk });
	}
}
