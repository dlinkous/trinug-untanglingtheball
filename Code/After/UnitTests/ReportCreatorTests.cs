using System;
using System.Linq;
using Xunit;
using UntanglingTheBall.Business.Common;
using UntanglingTheBall.Business.UseCases;
using UntanglingTheBall.UnitTests.Mocks;

namespace UntanglingTheBall.UnitTests
{
    public class ReportCreatorTests
    {
		[Fact]
		public void CreatesReportCorrectly()
		{
			var itemRetrieverMock = new ItemRetrieverMock();
			var reportWriterMock = new ReportWriterMock();
			var userContextMock = new UserContextMock();
			var creator = new ReportCreator(itemRetrieverMock, reportWriterMock, userContextMock);
			itemRetrieverMock.AddUnreportedItem(1, "Name1", "Red", 100);
			itemRetrieverMock.AddUnreportedItem(2, "Name2", "Orange", 200);
			itemRetrieverMock.AddUnreportedItem(3, "Name3", "Yellow", 300);
			itemRetrieverMock.AddUnreportedItem(4, "Name4", "Green", 400);
			itemRetrieverMock.AddUnreportedItem(5, "Name5", "Blue", 500);
			itemRetrieverMock.AddUnreportedItem(6, "Name6", "Violet", 600);
			userContextMock.Username = "UnitTestUsername";
			creator.Create(5, 0.2m);
			var writeCall = reportWriterMock.WriteCalls.Single();
			Assert.Equal("UnitTestUsername", writeCall.Username);
			var chunk = writeCall.Chunk;
			Assert.Equal(5, chunk.Items.Length);
			Assert.Equal(5, chunk.TotalItems);
			Assert.Equal(1500, chunk.TotalAmounts);
			Assert.Equal(100, chunk.TotalFees);
			Assert.Equal(1600, chunk.GrandTotal);
			var item1 = chunk.Items[0];
			var item2 = chunk.Items[1];
			var item3 = chunk.Items[2];
			var item4 = chunk.Items[3];
			var item5 = chunk.Items[4];
			void AssertItem(ReportChunk.ReportChunkItem item, int id, string name, decimal amount, decimal fee)
			{
				Assert.Equal(id, item.Id);
				Assert.Equal(name, item.Name);
				Assert.Equal(amount, item.Amount);
				Assert.Equal(fee, item.Fee);
			}
			AssertItem(item1, 1, "Name1", 100, 20);
			AssertItem(item2, 2, "Name2", 200, 0);
			AssertItem(item3, 3, "Name3", 300, 0);
			AssertItem(item4, 4, "Name4", 400, 80);
			AssertItem(item5, 5, "Name5", 500, 0);
		}
	}
}
