
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{

//#if !Proxy
//    [AutoMapFrom(typeof(TjlCustomerReg))]  
//#endif
    public class OccdieaseBasicInformationDto: EntityDto<Guid>
    {
        /// <summary>
        /// 体检号
        /// </summary>
       
        public virtual string CustomerBM { get; set; }


        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 单位名称TjlClientInfo
        /// </summary>
        public virtual string ClientName { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public virtual string TeamName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }

        /// <summary>
        /// 婚姻状况 1未婚2结婚
        /// </summary>
        public virtual int? MarriageStatus { get; set; }

        /// <summary>
        /// 文化程度 字典
        /// </summary>
        public virtual int? Degree { get; set; }

        /// <summary>
        /// 职务
        /// </summary>     
        public virtual string Duty { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>       
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 照射种类
        /// </summary>      
        public virtual string RadiationName { get; set; }

        /// <summary>
        /// 危害因素 逗号隔开
        /// </summary>
        public virtual string RiskS { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual List<SimpOccHazardFactorDto> OccHazardFactors { get; set; }

        /// <summary>
        /// 工种
        /// </summary>

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
        /// 总工龄单位
        /// </summary>
        [StringLength(2)]
        public virtual string WorkAgeUnit { get; set; }

        /// <summary>
        /// 接害工龄
        /// </summary>
        [StringLength(16)]
        public virtual string InjuryAge { get; set; }
        /// <summary>
        /// 接害工龄单位
        /// </summary>
        [StringLength(2)]
        public virtual string InjuryAgeUnit { get; set; }



        //public TjlCusomerDto Customer { get; set; }


        /// <summary>
        /// 岗位类别
        /// </summary>
        [StringLength(16)]
        public virtual string PostState { get; set; }



        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }


        /// <summary>
        /// 收费状态 1未收费6已收费7欠费
        /// </summary>
        public virtual int? CostState { get; set; }


        /// <summary>
        /// 个人照片
        /// </summary>
        public virtual Guid? CusPhotoBmId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? StarTime { get; set; }


    }
}
