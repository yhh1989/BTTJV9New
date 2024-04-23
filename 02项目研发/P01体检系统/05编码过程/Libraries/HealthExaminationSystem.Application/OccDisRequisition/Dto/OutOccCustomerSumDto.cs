#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System.Collections.Generic;
#endif


using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccDisRequisition.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlOccCustomerSum))]
#endif
    public class OutOccCustomerSumDto : EntityDto<Guid>
    {

        public virtual System.Guid? CustomerRegBMId { get; set; }
        /// <summary>
        /// 职业健康结论
        /// </summary>
        public virtual string Conclusion { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }
      
        /// <summary>
        /// 单位预约ID
        /// </summary>
        public virtual Guid? ClientRegID { get; set; }
 
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public virtual string TypeWork { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public virtual string IDCardNo { get; set; }
        /// <summary>
        /// 危害因素 多个逗号拼接
        /// </summary>
        public virtual string ZYRiskName { get; set; }
        /// <summary>
        /// 复查日期
        /// </summary>
        public virtual DateTime? ReviewContentDate { get; set; }
        /// <summary>
        /// 体检日期
        /// </summary>
        public virtual DateTime? CheckDate { get; set; }
        /// <summary>
        /// 复查项目
        /// </summary>
        public virtual string ReviewContent { get; set; }
        /// <summary>
        /// 疑似职业健康
        /// </summary>
        public virtual List<OccNameDto> ZYTabooIllness { get; set; }

        /// <summary>
        /// 疑似禁忌证
        /// </summary>
        public virtual List<OccNameDto> OccCustomerContraindications { get; set; }
        /// <summary>
        /// 处理意见
        /// </summary>
        public virtual string ZYTreatmentAdvice { get; set; }
        /// <summary>
        /// 总工龄
        /// </summary>
        public virtual string TotalWorkAge { get; set; }

        /// <summary>
        /// 接害工龄
        /// </summary>
    
        public virtual string InjuryAge { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        public virtual string WorkName { get; set; }
        /// <summary>
        /// 岗位类别
        /// </summary>
        public virtual string PostState { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndCheckDate { get; set; }
        /// <summary>
        /// 年度
        /// </summary>
        public string YearTime { get; set; }

        public string IsReview { get; set; }
        /// <summary>
        /// 通知单打印状态 1未打印2已打印
        /// </summary>
        public virtual int? TZDPrintSate { get; set; }

        /// <summary>
        /// 职业病结论描述
        /// </summary>
        public virtual string Description { get; set; }

    }
}
