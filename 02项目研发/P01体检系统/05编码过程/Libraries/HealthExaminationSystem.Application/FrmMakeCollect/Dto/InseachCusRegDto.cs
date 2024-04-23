using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect.Dto
{
  public   class InseachCusRegDto
    {
       
       /// <summary>
       /// 预约日期
       /// </summary>
        public virtual DateTime? BookingDate { get; set; }


        /// <summary>
        /// 时间段
        /// </summary>
        public virtual int? TimeSoft { get; set; }


       /// <summary>
       /// 预约类型
       /// </summary>
        public virtual int? RegType { get; set; }

        /// <summary>
        /// 预约状态
        /// </summary>
        public virtual int? RegState { get; set; }
        /// <summary>
        /// 单位预约Id
        /// </summary>
        public virtual Guid? ClienRegId { get; set; }
        /// <summary>
        /// 姓名体检号电话
        /// 
        /// </summary>
        public virtual string SearchName { get; set; }
    }
}
