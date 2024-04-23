using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.OConDictionary.Dto
{
  public   class ZYBTypeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>     
        public virtual string Name { get; set; }
        /// <summary>
        /// 职业健康类别
        /// </summary>
        public virtual string TypeName { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>    
        public virtual int? Order { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>     
        public virtual string HelpChar { get; set; }
    }
}
