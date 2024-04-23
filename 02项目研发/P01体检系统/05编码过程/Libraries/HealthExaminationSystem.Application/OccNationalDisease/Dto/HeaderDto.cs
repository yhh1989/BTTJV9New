using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
   public  class HeaderDto
    {
        /// <summary>
        /// 文档标识 ID
        /// </summary>
        public string DocumentId { get; set; }
        /// <summary>
        /// 数据操作类型
        /// </summary>
        public string OperateType { get; set; }
        /// <summary>
        /// 业务活动类型 职业健康：NEWOMAR
        /// </summary>
        public string BusinessActivityIdentification { get; set; }
     
        /// <summary>
        /// 上报机构代码
        /// </summary>
        public string ReportOrgCode { get; set; }
        /// <summary>
        /// 上报机构授权
        /// </summary>
        public string License { get; set; }
        /// <summary>
        /// 上报地区代码
        /// </summary>
        public string ReportZoneCode { get; set; }
    }
}
