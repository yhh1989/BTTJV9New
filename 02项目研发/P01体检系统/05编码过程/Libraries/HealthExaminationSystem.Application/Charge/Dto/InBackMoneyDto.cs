using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
   public  class InBackMoneyDto
    {
        /// <summary>
        /// ID结合
        /// </summary>
        public virtual List<MReceiptInfoDto> MReceiptInfos { get; set; }
        /// <summary>
        /// 退项目集合
        /// </summary>
        public virtual List<ChargeGroupsDto> ChargeGroupsDtos { get; set; }
        /// <summary>
        /// 退费金额
        /// </summary>
        public virtual decimal BackMoney { get; set; }

        /// <summary>
        /// 退费原价
        /// </summary>
        public virtual decimal BackAllMoney { get; set; }

    }
}
