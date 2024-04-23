using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto
{
   public class ShowResultSetDto
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public Guid? ClientrRegID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDataTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDataTime { get; set; }
        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }
        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }
        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }
        /// <summary>
        /// 时间类别
        /// </summary>
        public virtual int? TimeType { get; set; }


        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }
    }
}
