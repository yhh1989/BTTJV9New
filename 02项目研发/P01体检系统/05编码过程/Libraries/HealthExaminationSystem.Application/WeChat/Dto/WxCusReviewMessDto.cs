using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class WxCusReviewMessDto
    {

        /// <summary>
        /// 档案号
        /// </summary> 
        public virtual string archivesnum { get; set; }
        /// <summary>
        /// 体检号
        /// </summary> 
        public virtual string CustomerBM { get; set; }
        
        /// <summary>
        /// 订单号
        /// </summary>    
        public virtual string OrderNum { get; set; }



        /// <summary>
        /// keyword1复查信息
        /// </summary> 
        public virtual string keyword1 { get; set; }
        /// <summary>
        /// keyword2复查时间
        /// </summary> 
        public virtual string keyword2 { get; set; }


        /// <summary>
        /// 最后修改时间
        /// </summary>       
        public virtual DateTime? LastDate { get; set; }

    }
}
