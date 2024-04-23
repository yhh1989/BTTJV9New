using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto
{
   public class UpItemUnit
    {
        /// <summary>
        /// 标准编码
        /// </summary>  
        public virtual string StandardCode { get; set; }

        /// <summary>
        /// 单位
        /// </summary>     
        public virtual string Unit { get; set; }
        /// <summary>
        /// 单位编码
        /// </summary>
        public virtual string UnitBM { get; set; }
    }
}
