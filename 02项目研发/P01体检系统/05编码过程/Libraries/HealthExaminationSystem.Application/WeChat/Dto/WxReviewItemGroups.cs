using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
  public   class WxReviewItemGroup
    {
        /// <summary>
        /// 组合ID
        /// </summary>    
        [StringLength(64)]
        public virtual string ItemGroupBM { get; set; }
    }
}
