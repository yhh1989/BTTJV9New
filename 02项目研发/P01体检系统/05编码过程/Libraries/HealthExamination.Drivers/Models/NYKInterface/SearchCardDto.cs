using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers.Models.NYKInterface
{
  public  class SearchCardDto
    {
        /// <summary>
        ///医院编号
        /// </summary>       
        public virtual string hospitalno { get; set; }

        /// <summary>
        ///会员卡号
        /// </summary>       
        public virtual string cardno { get; set; }
    }
}
