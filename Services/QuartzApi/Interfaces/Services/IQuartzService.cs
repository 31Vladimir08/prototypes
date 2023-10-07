using QuartzApi.Models;

namespace QuartzApi.Interfaces.Services
{
    public interface IQuartzService
    {
        public Task AddSheduleJobAsync(JobSheduleModel job);
    }
}
