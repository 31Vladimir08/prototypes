using QuartzService.Models;

namespace QuartzService.Interfaces.Services;

public interface IQuartzService
{
    Task<JobSheduleModel> AddUpdateSheduleJobAsync(JobSheduleModel job);
    Task DeleteSheduleJobAsync(string jobKey, string groupName);
    Task<JobSheduleModel> GetSheduleJobAsync(string jobKey, string groupName);
    Task<IEnumerable<JobSheduleModel>> GetSheduleJobsAsync(string? groupName);
}
