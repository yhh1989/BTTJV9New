using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto
{
  public   class SeachOccTargetDiseaseDto
    {
        /// <summary>
        /// 危害因素Id
        /// </summary>
        public virtual Guid? OccHazardFactorsId { get; set; }
        /// <summary>
        /// 检查类型
        /// </summary>
        [StringLength(50)]
        public virtual string CheckType { get; set; }
        /// <summary>
        /// 职业健康名称
        /// </summary>
        [StringLength(50)]
        public virtual string OccDisName { get; set; }
        /// <summary>
        /// 禁忌证名称
        /// </summary>
        [StringLength(50)]
        public virtual string ConTrName { get; set; }

        public virtual int? IsOk { get; set; }

    }
}
