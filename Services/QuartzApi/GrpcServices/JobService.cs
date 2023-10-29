using Grpc.Core;

using MapsterMapper;

using QuartzApi.Interfaces.Services;
using QuartzApi.Models;
using QuartzApi.Protos;

namespace QuartzApi.GrpcServices
{
    public class JobService : Protos.JobService.JobServiceBase
    {
        private readonly ILogger<JobService> _logger;
        private readonly IQuartzService _quartzService;
        private readonly IMapper _mapper;

        public JobService(ILogger<JobService> logger, IQuartzService quartzService, IMapper mapper) 
        {
            _logger = logger;
            _quartzService = quartzService;
            _mapper = mapper;
        }

        public override async Task<JobKeyViewModel> AddJob(JobResponseModel request, ServerCallContext context)
        {
            try
            {
                var job = _mapper.Map<JobSheduleModel>(request);
                var newJob = await _quartzService.AddSheduleJobAsync(job);
                return new JobKeyViewModel
                {
                    JobKey = newJob.JobKey,
                    GroupName = newJob.GroupName
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Permission denied"));
            }
        }

        //public async Task<IActionResult> Update([FromBody] JobResponseModel job)
        //{
        //}

        public async Task<JobDeleteResponseMessage> DeleteJob(JobKeyViewModel request, ServerCallContext context)
        {
            try
            {
                await _quartzService.DeleteSheduleJobAsync(request.JobKey, request.GroupName);
                return new JobDeleteResponseMessage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Permission denied"));
            }
        }

        public async Task<JobResponseModel> GetJob(JobKeyViewModel request, ServerCallContext context)
        {
            try
            {
                var job = await _quartzService.GetSheduleJobAsync(request.JobKey, request.GroupName);
                var result = _mapper.Map<JobResponseModel>(request);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Permission denied"));
            }
        }

        public async Task<JobsResponseModel> GetJobs(JobGroupNameRequest request, ServerCallContext context)
        {
            try
            {
                var job = await _quartzService.GetSheduleJobsAsync(request.GroupName);
                var jobsResult = _mapper.Map<IEnumerable<JobResponseModel>>(request);
                var result = new JobsResponseModel();
                result.Jobs.AddRange(jobsResult);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Permission denied"));
            }
        }
    }
}
