using Fias.Api.Models.Job;
using Fias.Api.ViewModels.Jobs;

using Microsoft.AspNetCore.Mvc;

namespace Fias.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly ILogger<JobsController> _logger;

        public JobsController(ILogger<JobsController> logger)
        {
            _logger = logger;
        }

        [HttpPost()]
        [Route("addNewJob")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JobResponseModel))]
        public async Task<IActionResult> Add([FromBody] JobSheduleModel vm)
        {
            throw new NotImplementedException();
        }

        //[HttpPut()]
        //[Route("updateJob")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JobResponseModel))]
        //public async Task<IActionResult> Update([FromBody] JobResponseModel job)
        //{
        //}

        [HttpDelete()]
        [Route("deleteJob")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(JobDeleteVm vm)
        {
            throw new NotImplementedException();
        }

        //[HttpGet()]
        //[Route("getJob")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JobResponseModel))]
        //public async Task<IActionResult> Get(string jobKey)
        //{
        //}

        [HttpGet()]
        [Route("getJobs")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<JobResponseModel>))]
        public async Task<IActionResult> GetJobs(string? groupName)
        {
            throw new NotImplementedException();
        }
    }
}