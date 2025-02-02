using Grpc.Core;

using MapsterMapper;

using QuartzService.Protos;

using QuartzService.Interfaces.Services;
using QuartzService.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using static Quartz.Logging.OperationName;

namespace QuartzService.GrpcServices;

public class JobService : QuartzService.Protos.JobService.JobServiceBase
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

    public override async Task<JobKeyViewModelProto> AddJob(JobResponseModelProto request, ServerCallContext context)
    {
        var job = _mapper.Map<JobSheduleModel>(request);
        var newJob = await _quartzService.AddUpdateSheduleJobAsync(job);
        return new JobKeyViewModelProto
        {
            JobKey = newJob.JobKey,
            GroupName = newJob.GroupName
        };
    }

    //public async Task<IActionResult> Update([FromBody] JobResponseModel job)
    //{
    //}

    public override async Task<JobDeleteResponseMessageProto> DeleteJob(JobKeyViewModelProto request, ServerCallContext context)
    {
        await _quartzService.DeleteSheduleJobAsync(request.JobKey, request.GroupName);
        return new JobDeleteResponseMessageProto();
    }

    public override async Task<JobResponseModelProto> GetJob(JobKeyViewModelProto request, ServerCallContext context)
    {
        var job = await _quartzService.GetSheduleJobAsync(request.JobKey, request.GroupName);
        var result = _mapper.Map<JobResponseModelProto>(job);
        return result;
    }

    public override async Task<JobsResponseModelProto> GetJobs(JobGroupNameRequestProto request, ServerCallContext context)
    {
        var jobs = await _quartzService.GetSheduleJobsAsync(request.GroupName);
        var result = new JobsResponseModelProto();
        foreach (var job in jobs)
        {
            var jobResult = _mapper.Map<JobResponseModelProto>(job);
            result.Jobs.Add(jobResult);
        }
        return result;
    }
}
