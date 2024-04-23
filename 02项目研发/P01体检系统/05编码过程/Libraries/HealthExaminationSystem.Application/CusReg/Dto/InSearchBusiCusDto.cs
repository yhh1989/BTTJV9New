using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
   public  class InSearchBusiCusDto
    {
        /// <summary>
        /// 单位预约ID
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }
        /// <summary>
        ///开始登记日期
        /// </summary>
        public virtual DateTime? StarDate { get; set; }
        /// <summary>
        ///结束登记日期
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public virtual string LinkName { get; set; }


        /// <summary>
        /// 销售人员
        /// </summary>
        public virtual long? UserId { get; set; }
    }
}
