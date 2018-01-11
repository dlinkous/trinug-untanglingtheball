using System;
using UntanglingTheBall.Business.Entities;

namespace UntanglingTheBall.Business.UseCases
{
	public class ReportCreator
	{
		private readonly IItemRetriever itemRetriever;
		private readonly IReportWriter reportWriter;
		private readonly IUserContext userContext;

		public ReportCreator(IItemRetriever itemRetriever, IReportWriter reportWriter, IUserContext userContext)
		{
			this.itemRetriever = itemRetriever ?? throw new ArgumentNullException(nameof(itemRetriever));
			this.reportWriter = reportWriter ?? throw new ArgumentNullException(nameof(reportWriter));
			this.userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
		}

		public void Create(int quantity, decimal feeMultiplier)
		{
			var generator = new ReportChunkGenerator();
			foreach (var item in itemRetriever.GetUnreportedItems(quantity))
				generator.Add(item.Id, item.Name, item.Type, item.Amount);
			reportWriter.Write(userContext.GetUsername(), generator.Generate(feeMultiplier));
		}
	}
}
