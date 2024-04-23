using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;

#if !Proxy
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerReg))]
#endif
    public class CustomerRegForCrossTableViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人检查组合记录
        /// </summary>
        public virtual ICollection<CustomerItemGroupDto> CustomerItemGroup { get; set; }

        /// <summary>
        /// 体检人
        /// </summary>
        [Required]
        public virtual TjlCustomerDto Customer { get; set; }

        ///// <summary>
        ///// 单位分组
        ///// </summary>
        //public virtual TjlClientTeamInfoDto ClientTeamInfo { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual TjlClientInfoDto ClientInfo { get; set; }
        /// <summary>
        /// 体检ID
        /// </summary>
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }

        /// <summary>
        /// 查询码
        /// </summary>
        [StringLength(32)]
        public virtual string WebQueryCode { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        [StringLength(64)]
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
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }

        /// <summary>
        /// 总检锁定 1锁定2未锁定
        /// </summary>
        public virtual int? SummLocked { get; set; }

        /// <summary>
        /// 打印状态 1未打印2已打印
        /// </summary>
        public virtual int? PrintSate { get; set; }

        /// <summary>
        /// 报告领取状态 1未领取2已领取
        /// </summary>
        public virtual int? ReceiveSate { get; set; }

        /// <summary>
        /// 体检来源 字典ClientSource
        /// </summary>
        public virtual int? InfoSource { get; set; }

        /// <summary>
        /// 是否早餐 1不吃2吃3已吃
        /// </summary>
        public virtual int? HaveBreakfast { get; set; }

        /// <summary>
        /// 是否启用短信 1启用2不启用3已发生
        /// </summary>
        public virtual int? Message { get; set; }

        /// <summary>
        /// 是否启用邮件 1启用2不启用3已发送
        /// </summary>
        public virtual int? EmailReport { get; set; }

        /// <summary>
        /// 报告自取 1自取2邮寄3已邮寄
        /// </summary>
        public virtual int? ReportBySelf { get; set; }

        /// <summary>
        /// 是否健康管理 1管理2不管理
        /// </summary>
        public virtual int? MailingReport { get; set; }

        /// <summary>
        /// 是否盲检 1正常2盲检
        /// </summary>
        public virtual int? BlindSate { get; set; }

        /// <summary>
        /// 是否替检 1正常2替检
        /// </summary>
        public virtual int? ReplaceSate { get; set; }
        
        /// <summary>
        /// 导检状态 1未导检2正在导检3已暂停4已结束
        /// </summary>
        public virtual int? NavigationSate { get; set; }

        /// <summary>
        /// 导诊开始时间
        /// </summary>
        public virtual DateTime? NavigationStartTime { get; set; }

        /// <summary>
        /// 导诊结束时间
        /// </summary>
        public virtual DateTime? NavigationEndTime { get; set; }

        /// <summary>
        /// 1已预约2到店核销3报告可上传4报告已上传
        /// </summary>
        public virtual int? BespeakState { get; set; }

        /// <summary>
        /// 复查状态 1正常2复查3回访
        /// </summary>
        public virtual int? ReviewSate { get; set; }

        /// <summary>
        /// 收费状态 1未收费2已收费3欠费
        /// </summary>
        public virtual int? CostState { get; set; }

        /// <summary>
        /// 交表确认 1未交表2已交表
        /// </summary>
        public virtual int? SendToConfirm { get; set; }

        /// <summary>
        /// 交表人Id 1未交表2已交表
        /// </summary>    
        public virtual long? SendUserId { get; set; }

        /// <summary>
        /// 交表确认时间
        /// </summary>
        public virtual DateTime? SendToConfirmDate { get; set; }




        /// <summary>
        /// 抽血号
        /// </summary>
        public virtual int? DrawCard { get; set; }

        /// <summary>
        /// 抽血时间
        /// </summary>
        public virtual DateTime? DrawTime { get; set; }
        

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? RegAge { get; set; }

        /// <summary>
        /// 体检人检查组合记录
        /// </summary>
        public virtual ICollection<CusGiveUpDto> TjlCusGiveUps { get; set; }
#if Application
        [IgnoreMap] 
#endif
        public virtual DateTime? StayDateFormat
        {
            get
            {
                if (TjlCusGiveUps != null && TjlCusGiveUps.Count > 0)
                {
                    return TjlCusGiveUps.FirstOrDefault().stayDate;
                }
                else
                {
                    return null;
                }
            }

        }


    }
}
