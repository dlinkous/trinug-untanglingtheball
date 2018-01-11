using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using UntanglingTheBall.Business.Common;
using UntanglingTheBall.Business.UseCases;

namespace UntanglingTheBall.SQL
{
	public class SqlItemRetriever : IItemRetriever
	{
		private readonly ISqlSettings sqlSettings;

		public SqlItemRetriever(ISqlSettings sqlSettings) => this.sqlSettings = sqlSettings ?? throw new ArgumentNullException(nameof(sqlSettings));

		public IEnumerable<ReportItem> GetUnreportedItems(int quantity)
		{
			using (var con = new SqlConnection(sqlSettings.GetConnectionString()))
			{
				con.Open();
				const string sql = @"SELECT TOP (@Top) * FROM [dbo].[Items] WHERE Category = @Category AND IsReported = 0";
				return con.Query<ReportItem>(sql, new { Top = quantity, Category = sqlSettings.GetCurrentCategory() });
			}
		}
	}
}
