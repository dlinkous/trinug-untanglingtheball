using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

		public IActionResult Reports()
		{
			ViewData["Message"] = "Reports";

			return View();
		}

		[HttpPost]
		public IActionResult Reports(string quantity, string feeMultiplier)
		{
			ViewData["Message"] = "Reports";

			SqlConnection con = new SqlConnection();
			con.ConnectionString = "Server=POSEIDON\\EXPRESS1;Database=UntanglingTheBall;Trusted_Connection=true;";
			con.Open();
			SqlCommand com = new SqlCommand();
			com.Connection = con;
			com.CommandText = "SELECT TOP (" + quantity +  ") *, CASE [Type] WHEN 'Red' THEN 'True' WHEN 'Green' THEN 'True' ELSE 'False' END AS [RedOrGreen] FROM [dbo].[Items] WHERE Category = 'DEV' AND IsReported = 0";
			StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\report.txt");
			writer.WriteLine("Report for " + Environment.GetEnvironmentVariable("Username"));
			SqlDataReader reader = com.ExecuteReader();
			decimal items = 0;
			decimal amounts = 0;
			decimal fees = 0;
			decimal total = 0;
			while (reader.Read())
			{
				var fee = (Boolean.Parse(reader["RedOrGreen"].ToString()) ? Decimal.Parse(reader["Amount"].ToString()) * Decimal.Parse(feeMultiplier) : 0);
				writer.WriteLine(
					reader["Id"].ToString() + "," +
					reader["Name"].ToString() + "," +
					Decimal.Parse(reader["Amount"].ToString()).ToString("######0.00") + "," +
					fee.ToString("######0.00"));
				items = items + 1;
				amounts = amounts + Decimal.Parse(reader["Amount"].ToString());
				fees = fees + fee;
				total = total + Decimal.Parse(reader["Amount"].ToString()) + fee;
			}
			writer.WriteLine("Total Items: " + items.ToString());
			writer.WriteLine("Total Amounts: " + amounts.ToString("######0.00"));
			writer.WriteLine("Total Fees: " + fees.ToString("######0.00"));
			writer.Write("Grand Total: " + total.ToString("######0.00"));
			writer.Flush();

			return View();
		}

		public IActionResult Error()
        {
            return View();
        }
    }
}
