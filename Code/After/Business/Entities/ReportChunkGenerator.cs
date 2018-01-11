using System;
using System.Collections.Generic;
using System.Linq;
using UntanglingTheBall.Business.Common;

namespace UntanglingTheBall.Business.Entities
{
	public class ReportChunkGenerator
	{
		private List<ReportItem> Items { get; set; } = new List<ReportItem>();

		public void Add(int id, string name, string type, decimal amount) =>
			Items.Add(new ReportItem()
			{
				Id = id,
				Name = name,
				Type = type,
				Amount = amount
			});

		public ReportChunk Generate(decimal feeMultiplier)
		{
			var chunkItems = Items
				.Select(i => new ReportChunk.ReportChunkItem()
				{
					Id = i.Id,
					Name = i.Name,
					Amount = i.Amount,
					Fee = CalculateFee(i.Type, i.Amount, feeMultiplier)
				})
				.ToArray();
			var chunk = new ReportChunk()
			{
				Items = chunkItems,
				TotalItems = chunkItems.Length,
				TotalAmounts = chunkItems.Sum(i => i.Amount),
				TotalFees = chunkItems.Sum(i => i.Fee)
			};
			chunk.GrandTotal = chunk.TotalAmounts + chunk.TotalFees;
			return chunk;
		}

		private decimal CalculateFee(string type, decimal amount, decimal feeMultiplier) => IsTypeFeeable(type) ? (amount * feeMultiplier) : 0;

		private bool IsTypeFeeable(string type) => type == "Red" || type == "Green";
	}
}
