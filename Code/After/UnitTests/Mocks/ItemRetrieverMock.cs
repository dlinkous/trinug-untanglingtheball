using System;
using System.Collections.Generic;
using System.Linq;
using UntanglingTheBall.Business.Common;
using UntanglingTheBall.Business.UseCases;

namespace UntanglingTheBall.UnitTests.Mocks
{
	internal class ItemRetrieverMock : IItemRetriever
	{
		internal List<ReportItem> UnreportedItems = new List<ReportItem>();

		public IEnumerable<ReportItem> GetUnreportedItems(int quantity) => UnreportedItems.Take(quantity);

		internal void AddUnreportedItem(int id, string name, string type, decimal amount) =>
			UnreportedItems.Add(new ReportItem()
			{
				Id = id,
				Name = name,
				Type = type,
				Amount = amount
			});
	}
}
