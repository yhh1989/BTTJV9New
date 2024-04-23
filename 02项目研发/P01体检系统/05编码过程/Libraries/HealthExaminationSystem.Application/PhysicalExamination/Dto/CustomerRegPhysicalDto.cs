using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination
{
    /// <summary>
    /// 体检人预约登记信息表
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class CustomerRegPhysicalDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual CustomerPhysicalDto Customer { get; set; }

        
        public virtual ClientRegPhysicalDto ClientReg { get; set; }

        /// <summary>
        /// 单位分组
        /// </summary>
        public virtual ClientTeamPhysicalDto ClientTeamInfo { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>

        public virtual Guid TjlClientReg_Id { get; set; }


        /// <summary>
        /// 体检ID
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 婚姻状况 1未婚2结婚
        /// </summary>
        public virtual int? MarriageStatus { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        [StringLength(128)]
        public virtual string ItemSuitName { get; set; }

        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }

        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }

        /// <summary>
        /// 交表确认 1未交表2已交表
        /// </summary>
        public virtual int? SendToConfirm { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 抽血号
        /// </summary>
        public virtual int? DrawCard { get; set; }

        /// <summary>
        /// 准备孕或育
        /// </summary>
        public virtual int? ReadyPregnancybirth { get; set; }

        /// <summary>
        /// 收费状态 1未收费2已收费3欠费
        /// </summary>
        public virtual int? CostState { get; set; }

        /// <summary>
        /// 预约日期起
        /// </summary>
        public virtual DateTime? BookingDateStart { get; set; }

        /// <summary>
        /// 预约日期止
        /// </summary>
        public virtual DateTime? BookingDateEnd { get; set; }

        /// <summary>
        /// 是否仅今年
        /// </summary>
        public virtual int? OnlyYear { get; set; }
        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }
        /// <summary>
        /// 是否个人
        /// </summary>
        public bool IsPersonal { get; set; }

        /// <summary>
        /// 问卷状态 0已问卷 1未问卷
        /// </summary>
        public int? QuestionState { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>      
        public virtual string OrderNum { get; set; }


#if Application
            [AutoMapper.IgnoreMap]
#endif
        public virtual string FormatQuestionState
        {
            get
            {

                if (QuestionState == 1)
                {
                    return "已问诊";
                }
                else
                {
                    return "未问诊";
                }
                
            }

        }
    }
}