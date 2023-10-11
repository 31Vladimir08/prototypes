﻿using System.ComponentModel.DataAnnotations;

namespace QuartzApi.Models
{
    public class JobResponseModel
    {
        public JobResponseModel() 
        {
            Triggers = new List<TriggerModel>();
        }

        [Required]
        public string JobKey { get; set; }

        [Required]
        public string GroupName { get; set; }

        [Required]
        public List<TriggerModel> Triggers { get; set; }

        public string? Description { get; set; }

        public string? Data { get; set; }
    }
}
