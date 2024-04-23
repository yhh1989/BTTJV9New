#if !Proxy
using AutoMapper;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
   public  class ReportOccQuesSymptomDto
    {

        /// <summary>
        /// 序号
        /// </summary>       
        public virtual string BM { get; set; }
        /// <summary>
        ///症状名称
        /// </summary>
        [StringLength(500)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 症状分类
        /// </summary>
        [StringLength(500)]
        public virtual string Type { get; set; }
        /// <summary>
        /// 症状程度
        /// </summary>
        [StringLength(5)]
        public virtual string Degree { get; set; }
        /// <summary>
        /// 症状程度
        /// </summary>
        [StringLength(5)]
        public virtual string QuesUser { get; set; }
        /// <summary>
        /// xiyanshi
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        public string 受检人签名 { get; set; }

        /// <summary>
        /// 出现时间
        /// </summary>     
        public virtual DateTime? StarTime { get; set; }

    }
}
