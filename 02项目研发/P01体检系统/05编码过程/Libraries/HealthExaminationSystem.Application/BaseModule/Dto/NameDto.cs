using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.BaseModule.Dto
{
    public class NameDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
    }
}