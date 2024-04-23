using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.IDNumber.Dto
{
    public class NameDto
    {
        /// <summary>
        /// Id名称
        /// </summary>
        [Required]
        [StringLength(64)]
        public virtual string IDName { get; set; }
    }
}