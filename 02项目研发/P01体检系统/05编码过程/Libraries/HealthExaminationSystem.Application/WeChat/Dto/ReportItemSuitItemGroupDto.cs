using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
    public class ReportItemSuitItemGroupDto
    {

       

        /// <summary>
        /// 组合编码
        /// </summary>    
        public virtual string ItemGroupBM { get; set; }


        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal? Suitgrouprate { get; set; }

        /// <summary>
        /// 项目原价
        /// </summary>
        public virtual decimal? ItemPrice { get; set; }

        /// <summary>
        /// 折扣后价格
        /// </summary>
        public virtual decimal? PriceAfterDis { get; set; }
      
    }
}
