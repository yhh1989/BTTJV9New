using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto
{
   public class SerachOccdictionary
    {
        /// <summary>
        /// 字典类别，来自程序的枚举
        /// </summary>
        public virtual string Type { get; set; }
        /// <summary>
        /// 是否启用1启用0停用
        /// </summary>
        public virtual int IsActive { get; set; } 
    }
}
