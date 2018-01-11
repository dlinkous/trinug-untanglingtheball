using System;
using UntanglingTheBall.Business.Common;

namespace UntanglingTheBall.Business.UseCases
{
	public interface IReportWriter
	{
		void Write(string username, ReportChunk chunk);
	}
}
