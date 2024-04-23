using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class WXClientCusListDto
    {
        /// <summary>
        /// 单位编码
        /// </summary>
        [StringLength(24)]
        public virtual string ClientBM { get; set; }
        /// <summary>
        /// 预约编码
        /// </summary>
        public virtual string ClientRegBM { get; set; }     


        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime EndCheckDate { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public virtual DateTime? LastTime { get; set; }
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual List<WXclientCusDto> clientcuslist { get; set; }
    }
}
