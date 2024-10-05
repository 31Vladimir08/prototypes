using System.ComponentModel.DataAnnotations;

namespace QuartzService.Models;

public class JobSheduleModel
{
    public JobSheduleModel()
    {
        Triggers = new List<TriggerModel>();
    }

    public string JobKey { get; set; }

    [Required]
    public string GroupName { get; set; }

    [Required]
    public List<TriggerModel> Triggers { get; set; }

    public string? Description { get; set; }

    public string? Data { get; set; }
}
