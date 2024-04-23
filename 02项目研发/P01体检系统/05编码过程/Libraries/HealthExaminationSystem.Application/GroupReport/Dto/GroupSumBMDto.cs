using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto
{
   public class GroupSumBMDto : EntityDto<Guid>
    {


        /// <summary>
        /// 建议名称
        /// </summary>
        [StringLength(128)]
        public virtual string SummarizeName { get; set; }
        /// <summary>
        /// 人数
        /// </summary>

        public virtual int? cout { get; set; }
   
    }
}
