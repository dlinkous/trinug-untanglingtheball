using System;
using System.IO;
using System.Text;
using UntanglingTheBall.Business.Common;
using UntanglingTheBall.Business.UseCases;

namespace UntanglingTheBall.Files
{
	public class FileReportWriter : IReportWriter
	{
		private readonly IFileSettings fileSettings;

		public FileReportWriter(IFileSettings fileSettings) => this.fileSettings = fileSettings ?? throw new ArgumentNullException(nameof(fileSettings));

		public void Write(string username, ReportChunk chunk)
		{
			var builder = new StringBuilder();
			builder.AppendLine($"Report for {username}");
			foreach (var item in chunk.Items)
			{
				builder.AppendLine($"{item.Id},{item.Name},{item.Amount.ToString("######0.00")},{item.Fee.ToString("######0.00")}");
			}
			builder.AppendLine($"Total Items: {chunk.TotalItems}");
			builder.AppendLine($"Total Amounts: {chunk.TotalAmounts.ToString("######0.00")}");
			builder.AppendLine($"Total Fees: {chunk.TotalFees.ToString("######0.00")}");
			builder.Append($"Grand Total: {chunk.GrandTotal.ToString("######0.00")}");
			File.WriteAllText(fileSettings.GetRoot() + "\\report.txt", builder.ToString());
		}
	}
}
