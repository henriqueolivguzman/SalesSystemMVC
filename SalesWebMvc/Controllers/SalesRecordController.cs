using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SimpleSearch(DateTime? minimum, DateTime? maximum)
        {
            if (!minimum.HasValue)
            {
                minimum = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maximum.HasValue)
            {
                maximum = DateTime.Now;
            }
            var result = await _salesRecordService.FindByDateAsync(minimum, maximum);
            //View ira basicamente aceitar os seu model e o dicionario ViewData
            ViewData["minimum"] = minimum.Value.ToString("yyyy-MM-dd"); //value usado sempre nesse metodo pois os argumentos sao opcionais
            ViewData["maximum"] = maximum.Value.ToString("yyyy-MM-dd");


            return  View(result);
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
