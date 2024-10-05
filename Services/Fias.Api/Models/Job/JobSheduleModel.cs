using System.ComponentModel.DataAnnotations;

namespace Fias.Api.Models.Job
{
    public class JobSheduleModel
    {
        [Required]
        public string GroupName { get; set; }

        [Required]
        public string CronExpression { get; set; }
        public string? Description { get; set; }
        public string? Data { get; set; }
    }
}
