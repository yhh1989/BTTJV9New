using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// 查询客户预约信息Dto  
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg),typeof(QueryCustomerRegDto))]
#endif 
    public class SaveCusRegInfoDto : EntityDto<Guid>
    {

        /// <summary>
        /// 分组id
        /// </summary>
        public virtual Guid? ClientTeamInfo_Id { get; set; }

        /// <summary>
        /// 单位预约id
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }

        /// <summary>
        /// 单位Id
        /// </summary>
        public virtual Guid? ClientInfoId { get; set; }

        /// <summary>
        /// 单位分组Id
        /// </summary>
        public virtual Guid? ClientTeamInfoId { get; set; }

        /// <summary>
        /// 复查原预约ID
        /// </summary>      
        public virtual Guid? ReviewRegID { get; set; }
        /// <summary>
        /// 补检查原预约ID
        /// </summary>  
        public virtual Guid? SupplementaryRegID { get; set; }
      
       
        /// <summary>
        /// 预约ID
        /// </summary>
        public virtual int? CustomerRegID { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [StringLength(32)]
        public virtual string OrderNum { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }
        /// <summary>
        /// 单位人员编号
        /// </summary>
        public virtual int? ClientRegNum { get; set; }

        /// <summary>
        /// 查询码
        /// </summary>
        [StringLength(32)]
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
        /// 体检日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime? AppointmentTime { get; set; }

        /// <summary>
        /// 预约开始时间
        /// </summary>
        public virtual DateTime? BookingDateStartTime { get; set; }

        /// <summary>
        /// 预约结束时间
        /// </summary>
        public virtual DateTime? BookingDateEndTime { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary> 
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 预约开始时间
        /// </summary>
        public virtual DateTime? LoginDateStartTime { get; set; }

        /// <summary>
        /// 预约结束时间
        /// </summary>
        public virtual DateTime? LoginDateEndTime { get; set; }
        /// <summary>
        /// 交表开始时间
        /// </summary>
        public virtual DateTime? SendDateStartTime { get; set; }

        /// <summary>
        /// 交表结束时间
        /// </summary>
        public virtual DateTime? SendDateEndTime { get; set; }


        /// <summary>
        /// 套餐ID
        /// </summary>
        public virtual Guid? ItemSuitBMId { get; set; }

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
        /// 交表状态
        /// </summary>
        public virtual int? SendToConfrim { get; set; }

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
        /// 导诊开始时间
        /// </summary>
        public virtual DateTime? NavigationStartTime { get; set; }

        /// <summary>
        /// 导诊结束时间
        /// </summary>
        public virtual DateTime? NavigationEndTime { get; set; }

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
        /// 类别 1为普通用户2为VIP用户
        /// </summary>
        public virtual int? CustomerType { get; set; }

        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }

        /// <summary>
        /// 是否加急 1不加急2加急
        /// </summary>
        public virtual int? UrgentState { get; set; }

        /// <summary>
        /// 末次月经
        /// </summary>
        [StringLength(16)]
        public virtual string LastMenstruation { get; set; }

        /// <summary>
        /// 孕天
        /// </summary>
        [StringLength(16)]
        public virtual string Gestation { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? RegAge { get; set; }

        /// <summary>
        /// 原体检人
        /// </summary>
        [StringLength(64)]
        public virtual string PrimaryName { get; set; }

        /// <summary>
        /// 介绍人名字
        /// </summary>
        public virtual string Introducer { get; set; }

        /// <summary>
        /// 体检类别 字典
        /// </summary>
        public virtual string ClientType { get; set; }
        /// <summary>
        /// 是否免费
        /// </summary>
        public bool? IsFree { get; set; }

        /// <summary>
        /// 准备孕或育
        /// </summary>
        public virtual int? ReadyPregnancybirth { get; set; }

        /// <summary>
        /// 婚姻状况 1未婚2结婚
        /// </summary>
        public virtual int? MarriageStatus { get; set; }

        public int? AgeStart { get; set; }

        public int? AgeEnd { get; set; }
        public int? sex { get; set; }

        //单位Id
        public List<Guid> CompanysT { get; set; }

        //套餐Id
        public List<Guid> SetMealChoiceT { get; set; }

        /// <summary>
        /// 人员类别标识
        /// </summary>
        public Guid? GroupID { get; set; }

        /// <summary>
        /// 开单医师
        /// </summary>
        public string KaidanYisheng { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string DanweiMingcheng { get; set; }

        /// <summary>
        /// 人员类别标识
        /// </summary>
        public Guid? PersonnelCategoryId { get; set; }

        /// <summary>
        /// 人员类别标识(list)
        /// </summary>
        public List<Guid> PersonnelCategoryIdL { get; set; }
        /// <summary>
        /// 岗位类别
        /// </summary>
        [StringLength(16)]
        public virtual string PostState { get; set; }


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

        /// <summary>
        /// 危害因素 逗号隔开
        /// </summary>
        [StringLength(800)]
        public virtual string RiskS { get; set; }       
        /// <summary>
        /// 健康证编号
        /// </summary>
        [StringLength(32)]
        public virtual string JKZBM { get; set; }
        public long? LoginUserId { get; set; }
        /// <summary>
        /// 登记人姓名
        /// </summary>
        public string UserNames { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public virtual string IDCardNo { get; set; }
        /// <summary>
        /// 核酸类别
        /// </summary>
        public int? NucleicAcidType { get; set; }


        /// <summary>
        /// 开单医生
        /// </summary>     
        public virtual long? OrderUserId { get; set; }

        /// <summary>
        /// HIS门诊号
        /// </summary>
        [StringLength(32)]
        public virtual string HisSectionNum { get; set; }
        /// <summary>
        /// HIS患者ID
        /// </summary>
        [StringLength(32)]
        public virtual string HisPatientId { get; set; }


        /// <summary>
        /// 发票号
        /// </summary>     
        public virtual string FPNo { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(128)]
        public virtual string Remarks { get; set; }



        /// <summary>
        /// 签名
        /// </summary>
        public virtual Guid? SignBmId { get; set; }


        /// <summary>
        /// 拍照照片
        /// </summary>
        public virtual Guid? PhotoBmId { get; set; }

        /// <summary>
        /// 用工单位名称
        /// </summary>
        [StringLength(320)]
        public virtual string employerEnterpriseName { get; set; }
        /// <summary>
        /// 用工单位组织机构代码
        /// </summary>
        [StringLength(320)]
        public virtual string employerCreditCode { get; set; }
        /// <summary>
        /// 其他工种
        /// </summary>
        [StringLength(64)]
        public virtual string OtherTypeWork { get; set; }
    }
}
