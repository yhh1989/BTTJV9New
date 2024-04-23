#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
    

    public class ReportSuggestBear
        ///健康建议 总检结论
    {

        /// <summary>
        /// 审核人
        /// </summary>
        public virtual string ShEmployeeBM { get; set; }


        /// <summary>
        /// 总检人
        /// </summary>
        public virtual string EmployeeBM { get; set; }

        /// <summary>
        /// 总检日期
        /// </summary>
        public virtual DateTime? ConclusionDate { get; set; }


        /// <summary>
        /// 总检建议
        /// </summary>
        [StringLength(3072)]
        public virtual string Advice { get; set; }

        /// <summary>
        /// 总检结论
        /// </summary>
        [StringLength(3072)]
        public virtual string CharacterSummary { get; set; }        

        /// <summary>
        /// 诊断
        /// </summary>
        [StringLength(3072)]
        public virtual string DagnosisSummary { get; set; }


    }
}
