using Microsoft.AspNetCore.Mvc;

using Quartz;

using QuartzApi.Exceptions;
using QuartzApi.Interfaces.Services;
using QuartzApi.Models;
using QuartzApi.ViewModel;

namespace QuartzApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly ILogger<JobsController> _logger;
        private readonly IQuartzService _quartzService;

        public JobsController(ILogger<JobsController> logger, IQuartzService quartzService)
        {
            _logger = logger;
            _quartzService = quartzService;
        }

        [HttpPost()]
        [Route("addNewJob")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JobAddResponseViewModel))]
        public async Task<IActionResult> Add([FromBody] JobSheduleModel vm)
        {
            if (vm.Triggers.Any(x => !CronExpression.IsValidExpression(x.CronExpression)))
            {
                throw new UserException("Unvalid cron expression.");
            }

            var newJob = await _quartzService.AddUpdateSheduleJobAsync(vm);

            return Ok(new JobAddResponseViewModel() { GroupName = newJob.GroupName, JobKey = newJob.JobKey });
        }

        [HttpPut()]
        [Route("updateJob")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] JobSheduleModel vm)
        {
            if (vm.Triggers.Any(x => !CronExpression.IsValidExpression(x.CronExpression)))
            {
                throw new UserException("Unvalid cron expression.");
            }

            await _quartzService.AddUpdateSheduleJobAsync(vm);
            return Ok();
        }

        [HttpDelete()]
        [Route("deleteJob")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(JobDeleteVm vm)
        {
            await _quartzService.DeleteSheduleJobAsync(vm.JobKey, vm.GroupName);
            return Ok();
        }

        [HttpGet()]
        [Route("getJob")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JobSheduleModel))]
        public async Task<IActionResult> Get(string jobKey, string groupName)
        {
            var job = await _quartzService.GetSheduleJobAsync(jobKey, groupName);
            return Ok(job);
        }

        [HttpGet()]
        [Route("getJobs")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<JobSheduleModel>))]
        public async Task<IActionResult> GetJobs(string? groupName)
        {
            var jobs = await _quartzService.GetSheduleJobsAsync(groupName);
            return Ok(jobs);
        }
    }
}