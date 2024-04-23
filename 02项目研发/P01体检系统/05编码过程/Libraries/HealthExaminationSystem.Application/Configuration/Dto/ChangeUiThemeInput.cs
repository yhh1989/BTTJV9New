using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.Configuration.Dto
{
    public class ChangeUiThemeInput
    {
        [Required]
        [MaxLength(32)]
        public string Theme { get; set; }
    }
}