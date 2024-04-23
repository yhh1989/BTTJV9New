using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public class ReportGroupItems
    {       
        /// <summary>
        /// 项目编码
        /// </summary>    
        [StringLength(64)]
        public virtual string ItemBM { get; set; }       

    }
}
