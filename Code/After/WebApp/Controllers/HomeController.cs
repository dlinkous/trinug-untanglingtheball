using System;
using Microsoft.AspNetCore.Mvc;
using UntanglingTheBall.Business.UseCases;
using UntanglingTheBall.Files;
using UntanglingTheBall.SQL;
using WebApp.Implementations;

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

			var creator = new ReportCreator(new SqlItemRetriever(new WebSqlSettings()), new FileReportWriter(new WebFileSettings()), new WebUserContext());
			creator.Create(Int32.Parse(quantity), Decimal.Parse(feeMultiplier));

			return View();
		}

		public IActionResult Error()
        {
            return View();
        }
    }
}
