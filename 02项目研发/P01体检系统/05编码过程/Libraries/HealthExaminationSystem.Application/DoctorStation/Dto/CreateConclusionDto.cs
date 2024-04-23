using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 生成科室小结
    /// </summary>
    public class CreateConclusionDto
    {
        /// <summary>
        /// 科室id
        /// </summary>
        [Required]
        public List<Guid> Department { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        [Required]
        public string CustomerBM { get; set; }

        /// <summary>
        /// 服务传入
        /// </summary>
        public bool ServiceIntroduction { get; set; }
    }
}
