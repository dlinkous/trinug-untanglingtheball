using System;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using Xunit;
using UntanglingTheBall.Business.UseCases;
using UntanglingTheBall.Files;
using UntanglingTheBall.SQL;
using WebApp.Implementations;

namespace UntanglingTheBall.IntegrationTests
{
	public class ReportCreatorTests : IDisposable
	{
		private IFileSettings fileSettings = new WebFileSettings();
		private ISqlSettings sqlSettings = new WebSqlSettings();
		private IUserContext userContext = new WebUserContext();

		public ReportCreatorTests()
		{
			using (var con = new SqlConnection(sqlSettings.GetConnectionString()))
			{
				con.Open();
				var category = sqlSettings.GetCurrentCategory();
				con.Execute($"DELETE dbo.Items WHERE Category = '{category}'");
				void InsertItem(int id, string type, decimal amount, bool reported)
				{
					con.Execute($"INSERT INTO dbo.Items VALUES ({id}, 'Name{id}', '{type}', {amount}, {(reported ? 1 : 0)}, '{category}')");
				}
				InsertItem(99901, "Red", 100, true);
				InsertItem(99902, "Blue", 100, true);
				InsertItem(99903, "Red", 100, true);
				InsertItem(99904, "Red", 100, false);
				InsertItem(99905, "Blue", 200, false);
				InsertItem(99906, "Red", 300, false);
				InsertItem(99907, "Green", 400, false);
				InsertItem(99908, "Violet", 500, false);
				InsertItem(99909, "Red", 600, false);
			}
			Environment.SetEnvironmentVariable("Username", "IntegrationTest");
		}

		[Fact]
		public void FullIntegrationTest()
		{
			var creator = new ReportCreator(new SqlItemRetriever(sqlSettings), new FileReportWriter(fileSettings), userContext);
			creator.Create(5, 0.3m);
			var file = File.ReadAllText(fileSettings.GetRoot() + "\\report.txt");
			Assert.Equal(
@"Report for IntegrationTest
99904,Name99904,100.00,30.00
99905,Name99905,200.00,0.00
99906,Name99906,300.00,90.00
99907,Name99907,400.00,120.00
99908,Name99908,500.00,0.00
Total Items: 5
Total Amounts: 1500.00
Total Fees: 240.00
Grand Total: 1740.00", file);
		}

		public void Dispose()
		{
			using (var con = new SqlConnection(sqlSettings.GetConnectionString()))
			{
				con.Open();
				var category = sqlSettings.GetCurrentCategory();
				con.Execute($"DELETE dbo.Items WHERE Category = '{category}'");
			}
			Environment.SetEnvironmentVariable("Username", "");
		}
	}
}
