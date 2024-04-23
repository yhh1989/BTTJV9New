using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
   /// <summary>
   /// 修改登记信息状态
   /// </summary>
    public class EditCustomerRegStatesDto
    {
       /// <summary>
       /// 人员登记表id
       /// </summary>
        public Guid CustomerRegId { get; set; }
        /// <summary>
        /// 是否导引单
        /// </summary>
        public bool GuidanceSate { get; set; }
        ///// <summary>
        ///// 是否条码
        ///// </summary>
        //public bool BarState { get; set; }
        ///// <summary>
        ///// 是否申请单
        ///// </summary>
        //public bool RequestState { get; set; }
        /// <summary>
        /// 状态值
        /// </summary>
        public int StateValue { get; set; }
    }
}
