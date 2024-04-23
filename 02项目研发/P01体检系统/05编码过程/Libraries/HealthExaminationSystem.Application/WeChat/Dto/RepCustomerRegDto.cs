#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
using System.ComponentModel.DataAnnotations.Schema;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
#if !Proxy
    [AutoMapTo(typeof(TjlCustomerReg))]
#endif

    public class RepCustomerRegDto
    {
        /// <summary>
        /// 返回状态
        /// </summary>
        public virtual int ReState { get; set; }

        /// <summary>
        /// 状态说明  0未总检  1已总检
        /// </summary>
        public virtual string StateMa { get; set; }

        /// <summary>
        /// 科室建议记录
        /// </summary>
        public virtual List<CustomerSummarizeDto> CustomerSummary { get; set; }

        /// <summary>
        /// 体检人项目组合
        /// </summary>
        public virtual List<InCusGroupsDto> CustomerItemGroup { get; set; }

        /// <summary>
        /// 体检人科室小结
        /// </summary>
        public virtual List<DepDto> CustomerDepSummary { get; set; }


        /// <summary>
        /// 体检人外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
 
        public virtual Guid CustomerId { get; set; }

        /// <summary>
        /// 体检人
        /// </summary>
        public virtual ReportCusDto Customer { get; set; }

        /// <summary>
        /// 单位分组信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
       
        public virtual Guid? ClientTeamInfoId { get; set; }

        /// <summary>
        /// 单位分组
        /// </summary>
        public virtual ClientInfoDto ClientTeamInfo { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual ClientInfoDto ClientInfo { get; set; }

        /// <summary>
        /// 单位预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
       
        public virtual Guid? ClientRegId { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual ClientInfoDto ClientReg { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }

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
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }

      

        /// <summary>
        /// 报告打印状态 1未打印2已打印
        /// </summary>
        public virtual int? PrintSate { get; set; }

        /// <summary>
        /// 报告导出状态 1未导出2已导出
        /// </summary>
        public virtual int? ExportSate { get; set; }

        /// <summary>
        /// 报告领取状态 1未领取2已领取
        /// </summary>
        public virtual int? ReceiveSate { get; set; }


        /// <summary>
        /// 交表确认 1未交表2已交表
        /// </summary>
        public virtual int? SendToConfirm { get; set; }


        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }

        /// <summary>
        /// 是否免费
        /// </summary>
        public bool? IsFree { get; set; }

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
        /// 年龄
        /// </summary>
        public virtual int? RegAge { get; set; }


    





    }
}
