using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//#if !Proxy
//using AutoMapper;
//using Sw.Hospital.HealthExaminationSystem.Core.Examination;
//using Abp.AutoMapper;
//#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
//#if !Proxy
//    [AutoMap(typeof(TjlCustomerReg))]
//#endif
    public class SickNessDto
    {     
        /// <summary>
        /// 村庄
        /// </summary>
        public string TeamName { get; set; }
      /// <summary>
      /// 村庄
      /// </summary>
        public string NucleicAcidType { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 体检机构名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? SatrtDateTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDateTime { get; set; }
        /// <summary>
        /// 体检人数
        /// </summary>
        public string RCount { get; set; }
        /// <summary>
        /// 男性人数
        /// </summary>
        public int? ManSex { get; set; }
        /// <summary>
        /// 女性人数
        /// </summary>
        public int? WoManSex { get; set; }
        /// <summary>
        /// 异常人数
        /// </summary>
        public int? Symbol { get; set; }
        /// <summary>
        /// 高血压检出量
        /// </summary>
        public int? GxyCount { get; set; }
        /// <summary>
        /// 糖尿病检出量
        /// </summary>
        public int? TnbCount { get; set; }
        /// <summary>
        /// 高血压检出率
        /// </summary>
        public string JclGxyCount { get; set; }
        /// <summary>
        /// 糖尿病检出率
        /// </summary>
        public string JclTnbCount { get; set; }
        /// <summary>
        /// 老年
        /// </summary>
        public string OldGxyCount { get; set; }
        /// <summary>
        /// 异常人数检出率
        /// </summary>
        public string SignPer { get; set; }
    }
}
