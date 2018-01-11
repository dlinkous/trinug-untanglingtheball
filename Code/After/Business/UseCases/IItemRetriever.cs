using System;
using System.Collections.Generic;
using UntanglingTheBall.Business.Common;

namespace UntanglingTheBall.Business.UseCases
{
	public interface IItemRetriever
	{
		IEnumerable<ReportItem> GetUnreportedItems(int quantity);
	}
}
