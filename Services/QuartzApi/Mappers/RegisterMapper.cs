using Google.Protobuf.Collections;

using Mapster;

using QuartzService.Models;
using QuartzService.Protos;

namespace QuartzService.Mappers;

public class RegisterMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.Default.UseDestinationValue(member => member.SetterModifier == AccessModifier.None
            && member.Type.IsGenericType
            && member.Type.GetGenericTypeDefinition() == typeof(RepeatedField<>));

        config.NewConfig<JobResponseModelProto, JobSheduleModel>()
            .Map(x => x.JobKey, y => y.JobKey)
            .Map(x => x.GroupName, y => y.GroupName)
            .Map(x => x.Description, y => y.Description)
            .Map(x => x.Data, y => y.Data)
            .Map(x => x.Triggers, y => y.Triggers)
            .RequireDestinationMemberSource(true);
        config.NewConfig<JobSheduleModel, JobResponseModelProto>()
            .Map(x => x.JobKey, y => y.JobKey)
            .Map(x => x.GroupName, y => y.GroupName)
            .Map(x => x.Description, y => y.Description)
            .Map(x => x.Data, y => y.Data)
            .Map(x => x.Triggers, y => y.Triggers)
            .RequireDestinationMemberSource(true);

        config.NewConfig<TriggerModelProto, TriggerModel>()
            .Map(x => x.TriggerKey, y => y.TriggerKey)
            .Map(x => x.GroupName, y => y.GroupName)
            .Map(x => x.Description, y => y.Description)
            .Map(x => x.CronExpression, y => y.CronExpression)
            .RequireDestinationMemberSource(true);
        config.NewConfig<TriggerModel, TriggerModelProto>()
            .Map(x => x.TriggerKey, y => y.TriggerKey)
            .Map(x => x.GroupName, y => y.GroupName)
            .Map(x => x.Description, y => y.Description)
            .Map(x => x.CronExpression, y => y.CronExpression)
            .RequireDestinationMemberSource(true);
    }
}
