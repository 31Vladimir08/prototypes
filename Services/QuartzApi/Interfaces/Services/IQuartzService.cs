using QuartzApi.Models;

namespace QuartzApi.Interfaces.Services
{
    public interface IQuartzService
    {
        Task<JobSheduleModel> AddSheduleJobAsync(JobSheduleModel job);
        Task UpdateSheduleJobAsync(JobSheduleModel job);
        Task DeleteSheduleJobAsync(string jobKey, string groupName);
        Task<JobSheduleModel> GetSheduleJobAsync(string jobKey, string groupName);
        Task<IEnumerable<JobSheduleModel>> GetSheduleJobsAsync(string? groupName);
    }
}
