using System.ComponentModel.DataAnnotations;

namespace QuartzApi.Models
{
    public class TriggerModel
    {
        public string TriggerKey { get; set; }

        [Required]
        public string GroupName { get; set; }

        [Required]
        public string CronExpression { get; set; }

        public string? Description { get; set; }
    }
}
