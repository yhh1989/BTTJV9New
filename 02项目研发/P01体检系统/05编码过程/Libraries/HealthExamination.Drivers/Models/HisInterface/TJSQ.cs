using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers.Models.HisInterface
{ 
    public class TJSQ
    {
        /// <summary>
        /// His厂家
        /// </summary>
        public string HISName { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        public string SQDH{ get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public int? SQSTATUS { get; set; }
        /// <summary>
        /// 登记工号
        /// </summary>
        public int? DJGH { get; set; }

        /// <summary>
        /// 登记时间
        /// </summary>
        public DateTime? DJTIME { get; set; }
        /// <summary>
        /// 登记科室代码
        /// </summary>
        public string DJKSDM { get; set; }

        /// <summary>
        /// 病人编号
        /// </summary>
        public string BRH { get; set; }

        /// <summary>
        /// 病人卡号
        /// </summary>
        public string BRKH { get; set; }
        /// <summary>
        /// 病人姓名
        /// </summary>
        public string  BRXM { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string DWMC { get; set; }
        /// <summary>
        /// 病人性别
        /// </summary>
        public string BRXB { get; set; }
        /// <summary>
        /// 病人出生日期
        /// </summary>
        public DateTime? BRCS { get; set; }
        /// <summary>
        /// 病人联系地址
        /// </summary>
        public string BRLXDZ { get; set; }
        /// <summary>
        /// 病人联系电话
        /// </summary>
        public string BRLXDH { get; set; }

        /// <summary>
        /// 病人发票
        /// </summary>
        public string BRFPH { get; set; }
        /// <summary>
        /// 病人收费号
        /// </summary>
        public string BRSFH { get; set; }
        /// <summary>
        /// 折扣类型 
        /// </summary>
        public string ZKLX { get; set; }
        /// <summary>
        ///折扣数据 FYZK
        /// </summary>
        public decimal? FYZK { get; set; }
        /// <summary>
        /// 退费标志
        /// </summary>
        public string REFUNDABLE { get; set; }
        /// <summary>
        /// 收费明细
        /// </summary>
        public TJSQMX TJSQMXls { get; set; }
        /// <summary>
        /// 回传状态
        /// </summary>
        public int? HCZT { get; set; }

    }
}
