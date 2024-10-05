using Quartz;
using Quartz.Impl.Matchers;

using QuartzService.Exceptions;
using QuartzService.Interfaces.Services;
using QuartzService.Jobs;
using QuartzService.Models;

namespace QuartzService.Services;

public class QuartzService : IQuartzService
{
    private readonly ISchedulerFactory _factory;

    public QuartzService(ISchedulerFactory factory)
    {
        _factory = factory;
    }

    public async Task<JobSheduleModel> AddUpdateSheduleJobAsync(JobSheduleModel job)
    {
        if (job?.Triggers is null || !job.Triggers.Any())
        {
            throw new UserException("triggers are empty.");
        }

        var isReplace = !string.IsNullOrWhiteSpace(job.JobKey);

        var jobDataMap = new JobDataMap();
        jobDataMap.Put("data", job.Data);

        var jobDetails = JobBuilder.Create<StartingJob>()
            .WithIdentity(
                isReplace
                    ? job.JobKey
                    : Guid.NewGuid().ToString(),
                job.GroupName)
            .WithDescription(job.Description)
            .UsingJobData(jobDataMap)
            .StoreDurably()
            .Build();
        var newJob = new JobSheduleModel()
        {
            Data = job.Data,
            Description = job.Description,
            GroupName = job.GroupName,
            JobKey = jobDetails.Key.Name
        };

        var triggers = new List<ITrigger>();
        foreach (var item in job.Triggers)
        {
            var trigger = TriggerBuilder.Create()
            .WithIdentity(
                !string.IsNullOrWhiteSpace(item.TriggerKey)
                    ? item.TriggerKey
                    : $"{Guid.NewGuid()}-trigger",
                job.GroupName)
            .WithDescription(item.Description)
            .WithCronSchedule(item.CronExpression, x => x.WithMisfireHandlingInstructionFireAndProceed())
            .ForJob(jobDetails)
            .Build();
            triggers.Add(trigger);

            newJob.Triggers.Add(new TriggerModel()
            {
                CronExpression = item.CronExpression,
                Description = item.Description,
                GroupName = item.GroupName,
                TriggerKey = item.TriggerKey
            });
        }

        var scheduler = await _factory.GetScheduler();
        await scheduler.ScheduleJob(jobDetails, triggers, isReplace);

        return newJob;
    }

    public async Task DeleteSheduleJobAsync(string jobKey, string groupName)
    {
        var scheduler = await _factory.GetScheduler();
        var isDelete = await scheduler.DeleteJob(new JobKey(jobKey, groupName));
        if (!isDelete)
            throw new UserException("The job wasn't found or deleted.");
    }

    private async Task<IEnumerable<JobSheduleModel>> GetGroupSheduleJobAsync(string groupName, IScheduler scheduler)
    {
        if (string.IsNullOrWhiteSpace(groupName))
        {
            throw new UserException($"Could not find {groupName}");
        }

        var groupMatcher = GroupMatcher<JobKey>.GroupContains(groupName);
        var jobKeys = await scheduler.GetJobKeys(groupMatcher);
        if (jobKeys is null || !jobKeys.Any())
        {
            throw new UserException($"Could not find {groupName}");
        }

        var result = new List<JobSheduleModel>();
        foreach (var jobKey in jobKeys)
        {
            if (jobKey is null)
                continue;

            var job = await GetJobAsync(jobKey, scheduler);

            result.Add(job);
        }

        return result;
    }

    public async Task<IEnumerable<JobSheduleModel>> GetSheduleJobsAsync(string? groupName)
    {
        var scheduler = await _factory.GetScheduler();
        return string.IsNullOrWhiteSpace(groupName)
            ? await GetAllSheduleJobAsync(scheduler)
            : await GetGroupSheduleJobAsync(groupName, scheduler);
    }

    private async Task<IEnumerable<JobSheduleModel>> GetAllSheduleJobAsync(IScheduler scheduler)
    {

        var jobGroups = await scheduler.GetJobGroupNames();

        if (jobGroups is null || !jobGroups.Any())
        {
            throw new UserException($"Could not find jobs");
        }

        var result = new List<JobSheduleModel>();
        foreach (var group in jobGroups)
        {
            var jobs = await GetGroupSheduleJobAsync(group, scheduler);
            result.AddRange(jobs);
        }

        return result;
    }

    public async Task<JobSheduleModel> GetSheduleJobAsync(string jobKey, string groupName)
    {
        var scheduler = await _factory.GetScheduler();
        var jobK = new JobKey(jobKey, groupName);
        return await GetJobAsync(jobK, scheduler);
    }

    private async Task<JobSheduleModel> GetJobAsync(JobKey key, IScheduler scheduler)
    {
        var detail = await scheduler.GetJobDetail(key);
        var triggers = await scheduler.GetTriggersOfJob(key);
        var job = new JobSheduleModel()
        {
            JobKey = key.Name,
            Description = detail?.Description,
            Data = detail?.JobDataMap?.GetString("data"),
            GroupName = key.Group,
        };

        foreach (var trigger in triggers)
        {
            if (trigger is null)
                continue;

            job.Triggers.Add(new TriggerModel()
            {
                TriggerKey = trigger.Key.Name,
                GroupName = trigger.JobKey.Group,
                Description = trigger?.Description,
                CronExpression = trigger is ICronTrigger cronTrigger
                ? cronTrigger.CronExpressionString ?? string.Empty
                : string.Empty
            });
        }

        return job;
    }
}
