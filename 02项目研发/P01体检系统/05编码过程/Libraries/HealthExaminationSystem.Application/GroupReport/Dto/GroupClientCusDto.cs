using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using System.ComponentModel.DataAnnotations;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
#if !Proxy
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerReg))]
#endif
    public class GroupClientCusDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位预约id
        /// </summary>
        public virtual Guid TjlClientReg_Id { get; set; }
        /// <summary>
        /// 预约ID
        /// </summary>  
        public virtual int? CustomerRegID { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }
        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }

        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        [StringLength(16)]
        public virtual string TypeWork { get; set; }

        /// <summary>
        /// 车间
        /// </summary>
        [StringLength(16)]
        public virtual string WorkName { get; set; }

        /// <summary>
        /// 总工龄
        /// </summary>
        [StringLength(16)]
        public virtual string TotalWorkAge { get; set; }

        /// <summary>
        /// 接害工龄
        /// </summary>
        [StringLength(16)]
        public virtual string InjuryAge { get; set; }

        /// <summary>
        /// 体检人
        /// </summary>
        public virtual GroupCustomerDto Customer { get; set; }

        /// <summary>
        /// 体检人项目组合
        /// </summary>
        public virtual ICollection<GroupCusGroupDto> CustomerItemGroup { get; set; }
        public virtual Guid? ClientTeamInfoId { get; set; }

        public virtual ClientTeamInfosDto ClientTeamInfo { get; set; }
        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        [StringLength(16)]
        public virtual string PostState { get; set; }


        /// <summary>
        /// 危害因素 逗号隔开
        /// </summary>
        [StringLength(800)]
        public virtual string RiskS { get; set; }


        /// <summary>
        /// 复查状态 1正常2复查3回访
        /// </summary>
        public virtual int? ReviewSate { get; set; }

        /// <summary>
        /// 复查原预约ID
        /// </summary>   
        public virtual Guid? ReviewRegID { get; set; }
    }
}
