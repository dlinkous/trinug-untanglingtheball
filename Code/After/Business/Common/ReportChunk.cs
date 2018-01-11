using System;

namespace UntanglingTheBall.Business.Common
{
	public class ReportChunk
	{
		public class ReportChunkItem
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public decimal Amount { get; set; }
			public decimal Fee { get; set; }
		}

		public ReportChunkItem[] Items { get; set; }
		public int TotalItems { get; set; }
		public decimal TotalAmounts { get; set; }
		public decimal TotalFees { get; set; }
		public decimal GrandTotal { get; set; }
	}
}
