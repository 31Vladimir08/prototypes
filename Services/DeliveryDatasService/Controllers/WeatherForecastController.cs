using System.Data;

using DeliveryDatasService.Extensions;
using DeliveryDatasService.Interfaces.Services;
using DeliveryDatasService.Models.Options;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Expressions;

using Npgsql;

namespace DeliveryDatasService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IContextServiceFactory _factory;
        private readonly IDeliveryService _service;
        private readonly TablesOption _tableOption;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IContextServiceFactory factory, IDeliveryService service, IOptions<TablesOption> tableOption)
        {
            _logger = logger;
            _factory = factory;
            _service = service;
            _tableOption = tableOption.Value;
        }
        [HttpGet()]
        [Route("getJobs")]
        public async Task<IActionResult> GetJobs()
        {
            var d = await _service.GetDatasFormTableAsync(_tableOption.Tables[0]);
            return Ok(d);
        }
    }
}