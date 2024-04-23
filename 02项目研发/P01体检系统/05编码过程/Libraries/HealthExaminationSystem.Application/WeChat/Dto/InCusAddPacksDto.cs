using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class InCusAddPacksDto
    {
        /// <summary>
        /// 加项包表ID
        /// </summary>      
        public virtual Guid? ItemSuitID { get; set; }
        /// <summary>
        /// 加项包表名称
        /// </summary>      
        public virtual string ItemSuitName { get; set; }

    }
}
