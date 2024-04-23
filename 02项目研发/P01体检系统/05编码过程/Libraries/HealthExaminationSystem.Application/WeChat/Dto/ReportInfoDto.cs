using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class ReportInfoDto
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        public virtual ReportCusDto cusInfo { get; set; }

        /// <summary>
        /// 检查结果
        /// </summary>
        public virtual List<ReportCusITemDto> cusTem { get; set; }

        /// <summary>
        /// 总检建议
        /// </summary>
        public virtual CustomerSummarizeDto cusSummarize{ get; set; }

        /// <summary>
        /// 返回状态
        /// </summary>
        public virtual int ReState { get; set; }

        /// <summary>
        /// 状态说明  0未总检  1已总检
        /// </summary>
        public virtual string StateMa { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public virtual  List<PictureDto>  pictureDtos{ get; set; }

    }
}
