#if Application
using AutoMapper;
using HealthExaminationSystem.Enumerations.Helpers;
#endif
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Diagnosis.Dto
{
    public class CusRegInfoDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(32)]
        public virtual string ClientName { get; set; }
        /// <summary>
        /// 格式化性别
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        public virtual string FormatSex
        {
            get
            {
                var SexName = SexHelper.GetSexModelsForItemInfo().Where(o => o.Id == Sex).Select(o => o.Display).FirstOrDefault();
                return SexName;
            }

        }
        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }

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



        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? DepartOrderNum { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? GrouptOrderNum { get; set; }
        /// <summary>
        /// 项目序号
        /// </summary>
        public virtual int? ItemOrderNum { get; set; }
    }
}
