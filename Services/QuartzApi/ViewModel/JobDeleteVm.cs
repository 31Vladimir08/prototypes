using System.ComponentModel.DataAnnotations;

namespace QuartzApi.ViewModel
{
    public class JobDeleteVm
    {
        [Required]
        public string JobKey { get; set; }

        [Required]
        public string GroupName { get; set; }
    }
}
