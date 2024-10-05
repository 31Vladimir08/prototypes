using System.ComponentModel.DataAnnotations;

namespace Fias.Api.ViewModels.Jobs
{
    public class JobDeleteVm
    {
        [Required]
        public string JobKey { get; set; }

        [Required]
        public string GroupName { get; set; }
    }
}
