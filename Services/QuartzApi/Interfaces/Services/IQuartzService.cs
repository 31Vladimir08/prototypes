using QuartzApi.Models;

namespace QuartzApi.Interfaces.Services
{
    public interface IQuartzService
    {
        Task<JobResponseModel> AddSheduleJobAsync(JobSheduleModel job);
        Task UpdateSheduleJobAsync(JobResponseModel job);
        Task DeleteSheduleJobAsync(string jobKey, string groupName);
        Task<JobResponseModel> GetSheduleJobAsync(string jobKey, string groupName);
        Task<IEnumerable<JobResponseModel>> GetSheduleJobsAsync(string? groupName);
    }
}
