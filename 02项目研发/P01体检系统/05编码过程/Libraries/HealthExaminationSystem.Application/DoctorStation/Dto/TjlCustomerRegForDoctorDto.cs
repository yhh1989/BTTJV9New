using System;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;

#if !Proxy
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
    /// <summary>
    /// 体检人预约登记信息表
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerReg))]
#endif
    public class TjlCustomerRegForDoctorDto : EntityDto<Guid>
    { 
        /// <summary>
        /// 体检人外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual Guid CustomerId { get; set; }
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual TjlCustomerForInspectionTotalDto Customer { get; set; }
        /// <summary>
        /// 体检人项目组合
        /// </summary>
        public virtual List<CusGroupStateDto> CustomerItemGroup { get; set; }

        /// <summary>
        /// 初审医生外键
        /// </summary>
        public virtual long? CSEmployeeId { get; set; }

        /// <summary>
        /// 复审医生外键
        /// </summary>
        public virtual long? FSEmployeeId { get; set; }

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
        public virtual string WebQueryCode { get; set; }

        /// <summary>
        /// 体检场所-1院内-2外出
        /// </summary>

        public virtual int? ExamPlace { get; set; }

        /// <summary>
        /// 单位家属 1正常 2单位家属
        /// </summary>
        public virtual int? FamilyState { get; set; }

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
        public virtual string ItemSuitName { get; set; }

        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }

        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }

#if Proxy
        /// <summary>
        /// 格式化体检状态
        /// </summary>
        [JsonIgnore]
        public virtual string CheckSateFormat
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(CheckSate.ToString()))
                {
                    if (int.TryParse(CheckSate.ToString(), out var result))
                    {
                        return EnumHelper.GetEnumDesc((PhysicalEState)result);
                    }
                }

                return string.Empty;
            }
        }
#endif

        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }

#if Proxy
        /// <summary>
        /// 格式化总检状态
        /// </summary>
        [JsonIgnore]
        public virtual string SummSateFormat
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(SummSate.ToString()))
                {
                    if (int.TryParse(SummSate.ToString(), out var result))
                    {
                        return EnumHelper.GetEnumDesc((SummSate)result);
                    }
                }

                return string.Empty;
            }
        }
#endif

        /// <summary>
        /// 总检锁定 1锁定2未锁定
        /// </summary>
        public virtual int? SummLocked { get; set; }

        /// <summary>
        /// 格式化锁定状态
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        public virtual string SummLockedFormat
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(SummLocked.ToString()))
                {
                    if (int.TryParse(SummLocked.ToString(), out var result))
                    {
                        return EnumHelper.GetEnumDesc((SummLockedState)result);
                    }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// 打印状态 1未打印2已打印
        /// </summary>
        public virtual int? PrintSate { get; set; }

        /// <summary>
        /// 格式化打印状态
        /// </summary>
        //#if Application
        //        [IgnoreMap]
        //#endif
        //public virtual string PrintSateFormat
        //{
        //    get
        //    {
        //        if (!string.IsNullOrWhiteSpace(PrintSate.ToString()))
        //        {
        //            if (int.TryParse(PrintSate.ToString(), out var result))
        //            {
        //                return EnumHelper.GetEnumDesc((PrintSate)result);
        //            }
        //        }

        //        return string.Empty;
        //    }
        //}

        /// <summary>
        /// 报告领取状态 1未领取2已领取
        /// </summary>
        public virtual int? ReceiveSate { get; set; }

        /// <summary>
        /// 格式化领取状态
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        public virtual string ReceiveSateFormat
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ReceiveSate.ToString()))
                {
                    if (int.TryParse(ReceiveSate.ToString(), out var result))
                    {
                        return EnumHelper.GetEnumDesc((ReceiveSateState)result);
                    }
                }

                return string.Empty;
            }
        }


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
        /// 交表确认时间
        /// </summary>
        public virtual DateTime? SendToConfirmDate { get; set; }

        /// <summary>
        /// 是否已打导引单 1未打印2已打印3无须打印
        /// </summary>
        public virtual int? GuidanceSate { get; set; }

        /// <summary>
        /// 是否已打条码 1未打印2已打印3无须打印
        /// </summary>
        public virtual int? BarState { get; set; }

        /// <summary>
        /// 是否已打申请单 1未打印2已打印3无须打印
        /// </summary>
        public virtual int? RequestState { get; set; }

        /// <summary>
        /// 是否加急 1不加急2加急
        /// </summary>
        public virtual int? UrgentState { get; set; }

        /// <summary>
        /// 拿报告时间
        /// </summary>
        public virtual DateTime? HowDayReport { get; set; }

        /// <summary>
        /// 抽血号
        /// </summary>
        public virtual int? DrawCard { get; set; }

        /// <summary>
        /// 抽血时间
        /// </summary>
        public virtual DateTime? DrawTime { get; set; }

        /// <summary>
        /// 末次月经
        /// </summary>
        public virtual string LastMenstruation { get; set; }

        /// <summary>
        /// 孕天
        /// </summary>
        public virtual string Gestation { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? RegAge { get; set; }

        /// <summary>
        /// ICD10诊断
        /// </summary>
        public virtual string ICD10 { get; set; }

        /// <summary>
        /// 原体检人
        /// </summary>
        public virtual string PrimaryName { get; set; }

        /// <summary>
        /// 危害因素 逗号隔开
        /// </summary>
        public virtual string RiskS { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        public virtual string PostState { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        public virtual string TypeWork { get; set; }

        /// <summary>
        /// 车间
        /// </summary>
        public virtual string WorkName { get; set; }

        /// <summary>
        /// 总工龄
        /// </summary>
        public virtual string TotalWorkAge { get; set; }

        /// <summary>
        /// 接害工龄
        /// </summary>
        public virtual string InjuryAge { get; set; }

        /// <summary>
        /// 体检类别 字典
        /// </summary>
        public virtual string ClientType { get; set; }

        public int TenantId { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual TjlClientInfoDto ClientInfo { get; set; }

        public virtual Guid? ClientInfoId { get; set; }

        /// <summary>
        /// 单位预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }
    }
}