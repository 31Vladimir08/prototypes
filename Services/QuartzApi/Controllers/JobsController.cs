using Microsoft.AspNetCore.Mvc;

using Quartz;

using QuartzApi.Exceptions;
using QuartzApi.Interfaces.Services;
using QuartzApi.Models;

namespace QuartzApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly ILogger<JobsController> _logger;
        private readonly IQuartzService _quartzService;

        public JobsController(ILogger<JobsController> logger, IQuartzService quartzService)
        {
            _logger = logger;
            _quartzService = quartzService;
        }

        [HttpPost(Name = "AddNewJob")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] JobSheduleModel vm)
        {
            if (!CronExpression.IsValidExpression(vm.CronExpression))
            {
                throw new UserException("Unvalid cron expression.");
            }

            await _quartzService.AddSheduleJobAsync(vm);

            return Ok();
        }
    }
}