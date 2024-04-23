using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers.Models.YKYInterface
{
   public class InYKCarNum
    {
        /// <summary>
        ///人员类别[0:非会员.1：会员]
        /// </summary>       
        public virtual int? CustomerType { get; set; }
        /// <summary>
        ///档案号
        /// </summary>       
        public virtual string ArchivesNum { get; set; }

        /// <summary>
        ///标识[1:his.2:体检]
        /// </summary>       
        public virtual int? Mark { get; set; }
        /// <summary>
        ///消费金额
        /// </summary>       
        public virtual decimal? UseMoney { get; set; }
        /// <summary>
        ///收款类型[1:现金.2：银行卡.3：会员卡]
        /// </summary>       
        public virtual int? PaymentType { get; set; }
        /// <summary>
        ///会计科目
        /// </summary>       
        public virtual string AccountSubject { get; set; }
        /// <summary>
        ///操作人姓名
        /// </summary>       
        public virtual string OperatorName { get; set; }

        /// <summary>
        ///备注
        /// </summary>       
        public virtual string Remark { get; set; }
    }
}
