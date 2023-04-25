using ClosedXML.Excel;
using ExportExcelDemo.Entities;
using ExportExcelDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace ExportExcelDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<FileResult> ExportPeopleInExcel()
        {
            var people = await context.People.ToListAsync();
            var fileName = "people.xlsx";
            return GenerateExcel(fileName, people);
        }

        private FileResult GenerateExcel(string fileName, IEnumerable<Person> people)
        {
            DataTable dataTable = new DataTable("People");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id"),
                new DataColumn("Name")
            });

            foreach (var person in people)
            {
                dataTable.Rows.Add(person.Id, person.Name);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);

                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
                }
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}