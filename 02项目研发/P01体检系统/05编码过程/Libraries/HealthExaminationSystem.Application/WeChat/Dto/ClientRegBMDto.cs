using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class ClientRegBMDto
    {
        /// <summary>
        /// 体检号（用于对方存储失败后再次返回给接口）
        /// </summary>
        public virtual string ClientRegBM { get; set; }

        /// <summary>
        /// 是否上传 1上传，2不上传
        /// </summary>
        public virtual int ControlDate { get; set; }

        /// <summary>
        /// 体检号（用于对方存储失败后再次返回给接口）
        /// </summary>
        public virtual DateTime? LastTime { get; set; }

        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }

    }
}
