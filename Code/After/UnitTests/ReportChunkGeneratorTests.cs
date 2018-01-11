using System;
using Xunit;
using UntanglingTheBall.Business.Common;
using UntanglingTheBall.Business.Entities;

namespace UntanglingTheBall.UnitTests
{
	public class ReportChunkGeneratorTests
	{
		[Fact]
		public void GeneratesReportCorrectly()
		{
			var generator = new ReportChunkGenerator();
			generator.Add(1, "Name1", "Red", 100);
			generator.Add(2, "Name2", "Orange", 200);
			generator.Add(3, "Name3", "Yellow", 300);
			generator.Add(4, "Name4", "Green", 400);
			generator.Add(5, "Name5", "Blue", 500);
			generator.Add(6, "Name6", "Violet", 600);
			var chunk = generator.Generate(0.1m);
			Assert.Equal(6, chunk.TotalItems);
			Assert.Equal(2100, chunk.TotalAmounts);
			Assert.Equal(50, chunk.TotalFees);
			Assert.Equal(2150, chunk.GrandTotal);
			var item1 = chunk.Items[0];
			var item2 = chunk.Items[1];
			var item3 = chunk.Items[2];
			var item4 = chunk.Items[3];
			var item5 = chunk.Items[4];
			var item6 = chunk.Items[5];
			void AssertItem(ReportChunk.ReportChunkItem item, int id, string name, decimal amount, decimal fee)
			{
				Assert.Equal(id, item.Id);
				Assert.Equal(name, item.Name);
				Assert.Equal(amount, item.Amount);
				Assert.Equal(fee, item.Fee);
			}
			AssertItem(item1, 1, "Name1", 100, 10);
			AssertItem(item2, 2, "Name2", 200, 0);
			AssertItem(item3, 3, "Name3", 300, 0);
			AssertItem(item4, 4, "Name4", 400, 40);
			AssertItem(item5, 5, "Name5", 500, 0);
			AssertItem(item6, 6, "Name6", 600, 0);
		}
	}
}
