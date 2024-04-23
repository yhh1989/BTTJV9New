#if Application
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
    //TjlOccQuesPastHistory
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlOccQuesPastHistory))]
#endif
    public class ReportOccQuesPastHistoryDto
    {

        /// <summary>
        /// 疾病名称
        /// </summary>
        [StringLength(100)]
        public virtual string IllName { get; set; }

        /// <summary>
        /// 其他疾病名称
        /// </summary>
        [StringLength(100)]
        public virtual string OtherIllName { get; set; }
        /// <summary>
        /// 诊断日期
        /// </summary>

        public virtual DateTime? DiagnTime { get; set; }
#if !Proxy
                [IgnoreMap]
#endif
        public virtual string FormatDiagnTime
        {
            get
            {
                var Diagn = DiagnTime.HasValue ? DiagnTime.Value.ToString() : "无";
                return Diagn;
            }
        }

        /// <summary>
        /// 家族遗传史
        /// </summary>
#if !Proxy
                [IgnoreMap]
#endif
        public virtual string GeneticHistory { get; set; }

        /// <summary>
        /// 服药史
        /// </summary>
#if !Proxy
                [IgnoreMap]
#endif
        public virtual string MedicationHistory { get; set; }

        /// <summary>
        /// 药物过敏史
        /// </summary>
#if !Proxy
                [IgnoreMap]
#endif
        public virtual string AllergicHistory { get; set; }
        /// <summary>
        /// 药物禁忌
        /// </summary>
#if !Proxy
                [IgnoreMap]
#endif
        public virtual string DrugTaboo { get; set; }

        /// <summary>
        /// 诊断单位
        /// </summary>
        [StringLength(100)]
        public virtual string DiagnosisClient { get; set; }
        /// <summary>
        /// 治疗方式
        /// </summary>
        [StringLength(100)]
        public virtual string Treatment { get; set; }
        /// <summary>
        /// 是否痊愈
        /// </summary>

        public virtual string Iscured { get; set; }

#if !Proxy
        [IgnoreMap]
#endif
        public virtual string FormatIscured
        {
            get
            {
                if (Iscured == "0")
                { return "否"; }
                else
                { return "是"; }
            }
        }
        /// <summary>
        /// 报告签名
        /// </summary>
#if !Proxy
        [IgnoreMap]
#endif
        public virtual string 受检人签名 { get; set; }
        /// <summary>
        /// 诊断证书编号
        /// </summary>
        [StringLength(100)]
        public virtual string DiagnosticCode { get; set; }

       



    }
}
