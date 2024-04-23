using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
#if Application
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class GuideUpdateCustomerRegDto : EntityDto<Guid>
    {

        /// <summary>
        /// 体检人项目组合
        /// </summary>
        public virtual List<GuideUpdateCustomerItemGroupDto> CustomerItemGroup { get; set; }

        /// <summary>
        /// 单位预约id
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }

        /// <summary>
        /// 打印状态 1未打印2已打印
        /// </summary>
        public virtual int? PrintSate { get; set; }

        /// <summary>
        /// 导引单号 单位累加、个人当天累加
        /// </summary>
        public virtual int? GuidanceNum { get; set; }

        /// <summary>
        /// 导引单打印次数
        /// </summary>
        public virtual int? GuidancePrintNum { get; set; }
    }
}