using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

#if Application
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#elif Proxy
using Newtonsoft.Json;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerReg))]
#endif 
    public class CustomerRegForPrintPreviewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 体检人
        /// </summary>
        public virtual CustomerForPrintPreviewDto Customer { get; set; }

        /// <summary>
        /// 报告打印状态 1未打印2已打印
        /// </summary>
        public virtual int? PrintSate { get; set; }


        /// <summary>
        /// 职业报告打印状态 1未打印2已打印
        /// </summary>
        public virtual int? OccPrintSate { get; set; }
        /// <summary>
        /// 职业报告导出状态 1未导出2已导出
        /// </summary>
        public virtual int? occExportSate { get; set; }
        /// <summary>
        /// 报告打印次数
        /// </summary>
        public virtual int? PrintCount { get; set; }

        /// <summary>
        /// 最后报告打印时间
        /// </summary>
        public virtual DateTime? PrintTime { get; set; }

        /// <summary>
        /// 报告导出状态 1未导出2已导出
        /// </summary>
        public virtual int? ExportSate { get; set; }


        /// <summary>
        /// 报告领取状态 1未领取2已领取
        /// </summary>
        public virtual int? ReceiveSate { get; set; }
#if Proxy
        [JsonIgnore]
        public string  FormatReceiveSate
        {
            get
            {
                if (ReceiveSate.HasValue && ReceiveSate == 2)
                    return "已领取";
                else
                    return "未领取";
            }
        }
#endif
#if Proxy
        [JsonIgnore]
        public string  FormatExportSate
        {
            get
            {
                if (ExportSate.HasValue && ExportSate == 2)
                    return "已导出";
                else
                    return "未导出";
            }
        }
#endif
#if Proxy
        [JsonIgnore]
        public string FormatoccExportSate
        {
            get
            {
                if (occExportSate.HasValue && occExportSate == 2)
                    return "已导出";
                else
                    return "未导出";
            }
        }
#endif
        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }
        
        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }

        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        [StringLength(128)]
        public virtual string ItemSuitName { get; set; }
        /// <summary>
        /// 柜子号
        /// </summary>
        [StringLength(32)]
        public virtual string CusCabitBM { get; set; }

        /// <summary>
        /// 存入状态
        /// </summary>        
        public virtual int? CusCabitState { get; set; }
        /// <summary>
        /// 报告短信 0未发送 1已发送
        /// </summary>
        public int? ReportMessageState { get; set; }


        /// <summary>
        /// 复查状态 1正常2复查3回访
        /// </summary>
        public virtual int? ReviewSate { get; set; }


        /// <summary>
        /// 是否早餐 1不吃2吃3已吃
        /// </summary>
        public virtual int? HaveBreakfast { get; set; }

#if Proxy
        [JsonIgnore]
        public string  HaveBreakfastState
        {
            get
            {
                if (HaveBreakfast.HasValue && HaveBreakfast == 2)
                    return "是";
                else
                    return "否";
            }
        }
#endif

        /// <summary>
        /// 报告导出状态 1未导出2已导出
        /// </summary>

#if Proxy
        [JsonIgnore]
        public string  FormatCusCabitState
        {
            get
            {
                if (CusCabitState.HasValue && CusCabitState == 1)
                    return "存入";
                else
                    return "未存入";
            }
        }
#endif
#if Proxy
        [JsonIgnore]
        public string CZCabitState
        {
            get
            {
                if (CusCabitState.HasValue && CusCabitState == 1)
                    return "取消存入";
                else
                    return "存入";
            }
        }
#endif

        /// <summary>
        /// 存入时间
        /// </summary>        
        public virtual DateTime? CusCabitTime { get; set; }

        /// <summary>
        /// 出报告日期
        /// </summary>
        public virtual DateTime? ReportDate { get; set; }
        /// <summary>
        /// 单位信息
        /// </summary>
        public virtual ClientInfoOfPrintPreviewDto ClientInfo { get; set; }

        /// <summary>
        /// 预总检医生外键
        /// </summary>    
        public virtual long? PreEmployeeId { get; set; }

#if Application
        [IgnoreMap]
#endif
        public virtual string FormatIsYQ
        {
            get
            {

                if (ReportDate.HasValue && SummSate == 1 &&
                    DateTime.Parse(ReportDate.Value.ToShortDateString()) < DateTime.Parse(System.DateTime.Now.ToShortDateString()))
                {

                    return "是";
                }
                else
                { return "否"; }
               
            }

        }

#if Proxy
        [JsonIgnore]
        public int? Age
        {
            get
            {
                if (LoginDate.HasValue && Customer.Birthday.HasValue)
        {
         if (LoginDate.Value.Month > Customer.Birthday.Value.Month)
                    return  LoginDate.Value.Year - Customer.Birthday.Value.Year;
                else if (LoginDate.Value.Month == Customer.Birthday.Value.Month && LoginDate.Value.Day >= Customer.Birthday.Value.Day)
                   return  LoginDate.Value.Year - Customer.Birthday.Value.Year;
                else
                  return  LoginDate.Value.Year - Customer.Birthday.Value.Year - 1;
                    //return LoginDate.Value.Year - Customer.Birthday.Value.Year;
        }
                return null;

 
            }
        }
#endif
    }
}