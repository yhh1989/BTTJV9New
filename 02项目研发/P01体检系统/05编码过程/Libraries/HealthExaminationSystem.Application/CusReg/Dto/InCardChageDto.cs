using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
  public   class InCardChageDto
    {
        /// <summary>
        /// 卡号
        /// </summary>
        [StringLength(64)]
        public virtual string CardNo { get; set; }
        /// <summary>
        /// 预约Id
        /// </summary>  
        public virtual Guid CusRegId { get; set; }
        /// <summary>
        /// 套餐Id
        /// </summary>  
        public virtual Guid? SuitId { get; set; }
    }
}
