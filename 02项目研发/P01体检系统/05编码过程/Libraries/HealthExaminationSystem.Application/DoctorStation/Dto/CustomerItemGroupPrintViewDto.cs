using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 体检人项目组合
    /// 打印导引单使用
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerItemGroup))]
#endif
    public class CustomerItemGroupPrintViewDto : EntityDto<Guid>
    {

        /// <summary>
        /// 体检人预约主键
        /// </summary>
    
        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(64)]
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(32)]
        public virtual string DepartmentName { get; set; }
        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? ItemGroupOrder { get; set; }

        /// <summary>
        /// 暂停状态 1正常2暂停3解冻
        /// </summary>
        public virtual int? SuspendState { get; set; }

        /// <summary>
        /// 收费类别 SFType字典
        /// </summary>
        public virtual int? SFType { get; set; }

        /// <summary>
        /// 项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存
        /// </summary>
        public virtual int? CheckState { get; set; }

        /// <summary>
        /// 总检退回 1未退回2已退回3已处理4已审核
        /// </summary>
        public virtual int? SummBackSate { get; set; }

        /// <summary>
        /// 开单医生
        /// </summary>
        public virtual UserForComboDto BillingEmployeeBM { get; set; }

        /// <summary>
        /// 检查人
        /// </summary>
        public virtual UserForComboDto InspectEmployeeBM { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public virtual UserForComboDto CheckEmployeeBM { get; set; }

        /// <summary>
        /// 总审人
        /// </summary>
        public virtual UserForComboDto TotalEmployeeBM { get; set; }

        /// <summary>
        /// 加减状态 1为正常项目2为加项3为减项4调项减5调项加
        /// </summary>
        public virtual int? IsAddMinus { get; set; }

        /// <summary>
        /// 退费状态 1正常2带退费3退费 收费处退费后变为减项状态
        /// </summary>
        public virtual int? RefundState { get; set; }


       
        /// <summary>
        /// 套餐名称
        /// </summary>
        [StringLength(256)]
        public virtual string ItemSuitName { get; set; }

        /// <summary>
        /// 采血时间
        /// </summary>
        public virtual DateTime? Drawtime { get; set; }

        /// <summary>
        /// 采血人
        /// </summary>
        public virtual UserForComboDto DrawEmployeeIBM { get; set; }

        /// <summary>
        /// 组合小结
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemGroupSum { get; set; }

        /// <summary>
        /// 第一次检查时间
        /// </summary>
        public virtual DateTime? FirstDateTime { get; set; }
        /// <summary>
        /// 组合诊断 bxy
        /// </summary>
        public virtual string ItemGroupDiagnosis { get; set; }

        /// <summary>
        /// 原生组合小结 
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemGroupOriginalDiag { get; set; }
        public virtual int? IsZYB { get; set; }

        /// <summary>
        /// 报告编码 
        /// </summary>
        [StringLength(256)]
        public virtual string ReportBM { get; set; }

    }


}