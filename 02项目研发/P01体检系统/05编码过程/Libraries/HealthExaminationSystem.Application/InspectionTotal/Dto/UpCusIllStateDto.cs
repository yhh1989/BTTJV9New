using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
  public  class UpCusIllStateDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人项目Id
        /// </summary>
        public virtual Guid cusItemId { get; set; }
        /// <summary>
        /// 项目标示 H偏高 HH超高L偏低 LL 超低M正常P异常
        /// </summary>        
        public virtual string Symbol { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }
    }
}
