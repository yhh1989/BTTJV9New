using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 查询个人登记信息 
    /// </summary>
    public class SearchCustomerDto
    {
        /// <summary>
        /// 个人身份证号
        /// </summary>
        public string IDCardNo { get; set; }
        /// <summary>
        /// 个人名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 个人档案号
        /// </summary>
        public string ArchivesNum { get; set; }
        /// <summary>
        /// 登记体检号
        /// </summary>
        public string CustomerBM { get; set; }

        /// <summary>
        /// 就诊卡
        /// </summary>      
        public virtual string VisitCard { get; set; }
        /// <summary>
        /// 非某种体检状态（为了查询不是完成状态的登记信息
        /// </summary>
        public int? NotCheckState { get; set; }

        /// <summary>
        /// 订单号
        /// </summary> 
        public virtual string OrderNum { get; set; }
        /// <summary>
        /// 工号
        /// </summary>    
        public virtual string WorkNumber { get; set; }
    }
}
