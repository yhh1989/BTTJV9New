using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
 public    class OccCusDiseaseDto : EntityDto<Guid>
    {
        public virtual Guid? OccHazardFactorsId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(500)]
        public virtual string HelpChar { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(3072)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 父级单位
        /// </summary>
        [StringLength(500)]
        public virtual string ParentName { get; set; }

        /// <summary>
        /// 标准
        /// </summary>
        [StringLength(3072)]
        public virtual string Standards { get; set; }
    }
}
