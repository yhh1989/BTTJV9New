using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class NCusGroupsDto
    {
        /// <summary>
        /// 项目组合编码
        /// </summary>
     
        public virtual string ItemGroupBM { get; set; }      
        /// <summary>
        /// 套餐/或个性化编码
        /// </summary>  
        public virtual string ItemSuitBM { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal DiscountRate { get; set; }

        /// <summary>
        /// 折后价格
        /// </summary>
        public virtual decimal PriceAfterDis { get; set; }
    }
}
