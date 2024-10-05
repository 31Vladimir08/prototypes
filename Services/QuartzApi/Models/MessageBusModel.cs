using System.ComponentModel.DataAnnotations;

namespace QuartzService.Models;

public class MessageBusModel
{
    [Required]
    public string JobKey { get; set; }

    [Required]
    public string GroupName { get; set; }

    [Required]
    public TriggerModel Trigger { get; set; }

    public string? Description { get; set; }

    public string? Data { get; set; }
}
