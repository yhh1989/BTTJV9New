using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmCountrySet))]
#endif
    public class CreateCountrySet 
    {
        /// <summary>
        /// 上报机构代码
        /// </summary>            
        [StringLength(640)]
        public virtual string ReportOrgCode { get; set; }

        /// <summary>
        /// 上报地区代码
        /// </summary>            
        [StringLength(640)]
        public virtual string ReportZoneCode { get; set; }


        /// <summary>
        /// 上报机构授权
        /// </summary>            
        [StringLength(640)]
        public virtual string License { get; set; }

        /// <summary>
        /// 填表单位名称
        /// </summary>            
        [StringLength(640)]
        public virtual string WriteUnit { get; set; }
        /// <summary>
        /// 填表人姓名
        /// </summary>            
        [StringLength(640)]
        public virtual string WritePeson { get; set; }
        /// <summary>
        /// 填表人电话
        /// </summary>            
        [StringLength(640)]
        public virtual string WritePesonTel { get; set; }
        /// <summary>
        /// 报告单位名称
        /// </summary>            
        [StringLength(640)]
        public virtual string ReportUnit { get; set; }
        /// <summary>
        /// 报告人姓名
        /// </summary>            
        [StringLength(640)]
        public virtual string ReportPeson { get; set; }
        /// <summary>
        /// 报告人电话
        /// </summary>            
        [StringLength(640)]
        public virtual string ReportPesonTel { get; set; }

    }
}
