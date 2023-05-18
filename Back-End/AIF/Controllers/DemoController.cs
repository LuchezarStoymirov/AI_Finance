using AIF.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace AIF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetDemoInfo")]
        public IEnumerable<DemoModel> Get()
        {
            var demoInfo = new List<DemoModel>();
            demoInfo.Add(new DemoModel("S & P 500", (decimal)4161.00));
            demoInfo.Add(new DemoModel("Dow 30", (decimal)33340.71));
            demoInfo.Add(new DemoModel("Nasdaq", (decimal)12570.71));


            return demoInfo;
        }
    }
}