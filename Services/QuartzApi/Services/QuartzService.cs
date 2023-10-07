using Quartz;

using QuartzApi.Interfaces.Services;
using QuartzApi.Jobs;
using QuartzApi.Models;

namespace QuartzApi.Services
{
    public class QuartzService : IQuartzService
    {
        private readonly ISchedulerFactory _factory;

        public QuartzService(ISchedulerFactory factory) 
        {
            _factory = factory;
        }

        public async Task AddSheduleJobAsync(JobSheduleModel job)
        {
            var jobDataMap = new JobDataMap();
            jobDataMap.Put("data", job.Data);

            var jobDetails = JobBuilder.Create<StartingJob>()
                .WithIdentity(Guid.NewGuid().ToString(), job.GroupName)
                .WithDescription(job.Description)
                .UsingJobData(jobDataMap)
                .StoreDurably()
                .Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobDetails.Key.Name}-trigger", job.GroupName)
                //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(01, 05).WithMisfireHandlingInstructionFireAndProceed())
                .WithCronSchedule(job.CronExpression, x => x.WithMisfireHandlingInstructionFireAndProceed())
                .ForJob(jobDetails)
                .Build();

            var scheduler = await _factory.GetScheduler();
            await scheduler.ScheduleJob(jobDetails, trigger);
        }
    }
}
