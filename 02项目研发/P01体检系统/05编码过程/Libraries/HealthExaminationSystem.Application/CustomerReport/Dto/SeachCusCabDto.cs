using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
  public   class SeachCusCabDto
    {
        /// <summary>
        /// 体检号
        /// </summary>      
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string CustomerName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }

        /// <summary>
        /// 柜子号
        /// </summary>
        public string CabitName { get; set; }
        /// <summary>
        /// 取出状态1存入2取出
        /// </summary>
        public int? GetState { get; set; }
        /// <summary>
        /// 登记开始时间
        /// </summary>
        public virtual DateTime? StarLoginTime { get; set; }

        /// <summary>
        /// 登记结束时间
        /// </summary>
        public virtual DateTime? EndLoginTime { get; set; }

        /// <summary>
        /// 领取开始时间
        /// </summary>
        public virtual DateTime? StarSendTime { get; set; }

        /// <summary>
        /// 领取结束时间
        /// </summary>
        public virtual DateTime? EndSendTime { get; set; }

        
    }
}
