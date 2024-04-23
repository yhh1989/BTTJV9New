#if Application
using AutoMapper;
#endif
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Diagnosis.Dto
{
    public class CusRegInfoDetailDto
    {
        

        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string ItemName { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string ItemValue { get; set; }
        /// <summary>
        /// 项目诊断
        /// </summary>
        public virtual string ItemDiag { get; set; }

        /// <summary>
        /// 参考值
        /// </summary>
        [StringLength(256)]
        public virtual string Stand { get; set; }

        /// <summary>
        /// 项目标示 H偏高 HH超高L偏低 LL 超低M正常P异常
        /// </summary>
        [StringLength(16)]
        public virtual string Symbol { get; set; }

    }
}
