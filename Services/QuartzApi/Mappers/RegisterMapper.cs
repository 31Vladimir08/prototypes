using Mapster;

using QuartzApi.Models;
using QuartzApi.Protos;

namespace QuartzApi.Mappers
{
    public class RegisterMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<JobResponseModel, JobSheduleModel>()
                .Map(x => x.JobKey, y => y.JobKey)
                .Map(x => x.GroupName, y => y.GroupName)
                .Map(x => x.Description, y => y.Description)
                .Map(x => x.Data, y => y.Data)
                .Map(x => x.Triggers, y => y.Triggers)
                .RequireDestinationMemberSource(true);
            config.NewConfig<JobSheduleModel, JobResponseModel>()
                .Map(x => x.JobKey, y => y.JobKey)
                .Map(x => x.GroupName, y => y.GroupName)
                .Map(x => x.Description, y => y.Description)
                .Map(x => x.Data, y => y.Data)
                .Map(x => x.Triggers, y => y.Triggers)
                .RequireDestinationMemberSource(true);

            config.NewConfig<Protos.TriggerModel, Models.TriggerModel>()
                .Map(x => x.TriggerKey, y => y.TriggerKey)
                .Map(x => x.GroupName, y => y.GroupName)
                .Map(x => x.Description, y => y.Description)
                .Map(x => x.CronExpression, y => y.CronExpression)
                .RequireDestinationMemberSource(true);
            config.NewConfig<Models.TriggerModel, Protos.TriggerModel>()
                .Map(x => x.TriggerKey, y => y.TriggerKey)
                .Map(x => x.GroupName, y => y.GroupName)
                .Map(x => x.Description, y => y.Description)
                .Map(x => x.CronExpression, y => y.CronExpression)
                .RequireDestinationMemberSource(true);
        }
    }
}
