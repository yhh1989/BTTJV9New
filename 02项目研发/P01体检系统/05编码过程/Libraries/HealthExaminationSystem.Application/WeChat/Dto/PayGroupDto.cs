using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class PayGroupDto
    {

        /// <summary>
        /// 项目组合编码
        /// </summary>
        public virtual string ItemGroupBM { get; set; }           

        /// <summary>
        /// 折后价格
        /// </summary>
        public virtual decimal PriceAfterDis { get; set; }
    }
}
