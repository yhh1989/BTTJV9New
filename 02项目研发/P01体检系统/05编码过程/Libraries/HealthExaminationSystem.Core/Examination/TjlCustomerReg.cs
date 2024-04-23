using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary> 
    /// 体检人预约登记信息表
    /// </summary>
    public class TjlCustomerReg : FullAuditedEntity<Guid>, IMustHaveTenant
    {

        /// <summary>
        /// 发票号
        /// </summary>
        public virtual ICollection<TjlApplicationForm>  ApplicationForm { get; set; }
        /// <summary>
        /// 收费记录
        /// </summary>
        public virtual ICollection<TjlMReceiptInfo> MReceiptInfo { get; set; }

        /// <summary>
        /// 危害因素总检
        /// </summary>
        public virtual ICollection<TjlOccCustomerHazardSum> OccCustomerHazardSum { get; set; }

        /// <summary>
        /// 个人应收已收
        /// </summary>
        public virtual TjlMcusPayMoney McusPayMoney { get; set; }

        /// <summary>
        /// 工作历史
        /// </summary>
        public virtual ICollection<TjlOAHistory> OAHistory { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        public virtual ICollection<TjlOHistory> OHistory { get; set; }

        ///// <summary>
        ///// 科室建议记录
        ///// </summary>
        //public virtual ICollection<TjlCustomerSummary> CustomerSummary { get; set; }

        /// <summary>
        /// 体检人项目组合
        /// </summary>
        public virtual ICollection<TjlCustomerItemGroup> CustomerItemGroup { get; set; }
        
        /// <summary>
        /// 体检人科室小结
        /// </summary>
        public virtual ICollection<TjlCustomerDepSummary> CustomerDepSummary { get; set; }

        /// <summary>
        /// 体检人诊断
        /// </summary>
        public virtual ICollection<TjlCustomerSummarizeBM> CustomerSummarizeBM { get; set; }

        ///// <summary>
        ///// 体检人科室小结
        ///// </summary>
        //public virtual TjlCustomerSummarize CustomerSummarize { get; set; }

        //public virtual User CreatorUserId { get; set; }
        /// <summary>
        /// 体检人条码集合
        /// </summary>
        [StringLength(32)]
        [NotMapped]
        [Obsolete("停止使用！", true)]
        public virtual ICollection<TjlCustomerBarPrintInfoItemGroup> lstCustomerBarPrintInfo { get; set; }

        /// <summary>
        /// 体检人条码集合
        /// </summary>
        [StringLength(32)]
        public virtual ICollection<TjlCustomerBarPrintInfo> CustomerBarPrintInfo { get; set; }

        /// <summary>
        /// 体检人外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("Customer")]
        public virtual Guid CustomerId { get; set; }

        /// <summary>
        /// 体检人
        /// </summary>
        public virtual TjlCustomer Customer { get; set; }

        /// <summary>
        /// 单位分组信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("ClientTeamInfo")]
        public virtual Guid? ClientTeamInfoId { get; set; }

        /// <summary>
        /// 单位分组
        /// </summary>
        public virtual TjlClientTeamInfo ClientTeamInfo { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual TjlClientInfo ClientInfo { get; set; }

        /// <summary>
        /// 单位信息标识
        /// </summary>
        [ForeignKey(nameof(ClientInfo))]
        public virtual Guid? ClientInfoId { get; set; }

        /// <summary>
        /// 单位预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("ClientReg")]
        public virtual Guid? ClientRegId { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual TjlClientReg ClientReg { get; set; }
        

        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [StringLength(32)]
        public virtual string OrderNum { get; set; }

        /// <summary>
        /// 登记序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }

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
        ///体检日期
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
        /// 套餐信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("ItemSuitBM")]
        public virtual Guid? ItemSuitBMId { get; set; }

        /// <summary>
        /// 套餐id
        /// </summary>
        public virtual TbmItemSuit ItemSuitBM { get; set; }

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
        /// 总检状态 1未总检2已分诊3已初检4已审核5审核不通过
        /// </summary>
        public virtual int? SummSate { get; set; }
        /// <summary>
        /// 职业检总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? OccSummSate { get; set; }

        /// <summary>
        /// 总检锁定 1锁定2未锁定
        /// </summary>
        public virtual int? SummLocked { get; set; }


        /// <summary>
        /// 总检锁定时间
        /// </summary>
        public virtual DateTime? SummLockTime { get; set; }

        /// <summary>
        /// 总检锁定人
        /// </summary>
        public virtual User SummLockEmployeeBM { get; set; }

        /// <summary>
        /// 总检锁定用户ID
        /// </summary>
        [ForeignKey(nameof(SummLockEmployeeBM))]
        public virtual long? SummLockEmployeeBMId { get; set; }


        /// <summary>
        /// 报告打印状态 1未打印2已打印
        /// </summary>
        public virtual int? PrintSate { get; set; }

        /// <summary>
        /// 职业报告打印状态 1未打印2已打印
        /// </summary>
        public virtual int? OccPrintSate { get; set; }

        /// <summary>
        /// 报告打印次数
        /// </summary>
        public virtual int? PrintCount { get; set; }
        /// <summary>
        /// 最后报告打印时间
        /// </summary>
        public virtual DateTime? PrintTime { get; set; }
        /// <summary>
        /// 通知单打印状态 1未打印2已打印
        /// </summary>
        public virtual int? TZDPrintSate { get; set; }

        /// <summary>
        /// 报告导出状态 1未导出2已导出
        /// </summary>
        public virtual int? ExportSate { get; set; }
        /// <summary>
        /// 职业报告导出状态 1未导出2已导出
        /// </summary>
        public virtual int? occExportSate { get; set; }

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
        /// 是否启用短信 1启用2不启用3已发送
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
        /// 导检护士
        /// </summary>
        public virtual User NavigationEmployeeBM { get; set; }

        /// <summary>
        /// 导检护士标识
        /// </summary>
        [ForeignKey(nameof(NavigationEmployeeBM))]
        public virtual long? NavigationEmployeeBMId { get; set; }

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
        /// 导检结束审核人
        /// </summary>
        public virtual User NavigationOperation { get; set; }

        /// <summary>
        /// 1已预约2到店核销3报告可上传4报告已上传
        /// </summary>
        public virtual int? BespeakState { get; set; }

        /// <summary>
        /// 复查状态 1正常2复查3回访
        /// </summary>
        public virtual int? ReviewSate { get; set; }

        /// <summary>
        /// 复查原预约ID
        /// </summary>
        [ForeignKey("ReviewReg")]
        public virtual Guid? ReviewRegID { get; set; }


        /// <summary>
        /// 复查预约信息
        /// </summary>
        public virtual TjlCustomerReg ReviewReg { get; set; }

        /// <summary>
        /// 补检查原预约ID
        /// </summary>        
        public virtual Guid? SupplementaryRegID { get; set; }

 
        /// <summary>
        /// 收费状态 1未收费6已收费7欠费
        /// </summary>
        public virtual int? CostState { get; set; }

        /// <summary>
        /// 总检医生外键
        /// </summary>
        [ForeignKey("CSEmployeeBM")]
        public virtual long? CSEmployeeId { get; set; }

        /// <summary>
        /// 总检医生id  
        /// </summary>
        public virtual User CSEmployeeBM { get; set; }

        /// <summary>
        /// 复审医生外键
        /// </summary>
        [ForeignKey("FSEmployeeBM")]
        public virtual long? FSEmployeeId { get; set; }

        /// <summary>
        /// 复审医生id  
        /// </summary>
        public virtual User FSEmployeeBM { get; set; }

        /// <summary>
        /// 预总检医生外键
        /// </summary>
        [ForeignKey("PreEmployeeBM")]
        public virtual long? PreEmployeeId { get; set; }

        /// <summary>
        /// 总检医生id 用于预总检
        /// </summary>
        public virtual User PreEmployeeBM { get; set; }

        /// <summary>
        /// 预总检时间
        /// </summary>
        public virtual DateTime? PreSumDate { get; set; }

        /// <summary>
        /// 交表确认 1未交表2已交表
        /// </summary>
        public virtual int? SendToConfirm { get; set; }

        /// <summary>
        /// 交表人Id 1未交表2已交表
        /// </summary>
        [ForeignKey("SendUser")]
        public virtual long? SendUserId { get; set; }
        /// <summary>
        /// 交表人
        /// </summary>
        public virtual User SendUser { get; set; }
        /// <summary>
        /// 类别 1为普通用户2为VIP用户
        /// </summary>
        public virtual int? CustomerType { get; set; }

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
        /// ICD10诊断
        /// </summary>
        [StringLength(32)]
        public virtual string ICD10 { get; set; }

        /// <summary>
        /// 原体检人
        /// </summary>
        [StringLength(64)]
        public virtual string PrimaryName { get; set; }

        /// <summary>
        /// 危害因素 逗号隔开
        /// </summary>
        [StringLength(800)]
        public virtual string RiskS { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        [StringLength(16)]
        public virtual string PostState { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        [StringLength(32)]
        public virtual string TypeWork { get; set; }
        /// <summary>
        /// 其他工种
        /// </summary>
        [StringLength(64)]
        public virtual string OtherTypeWork { get; set; }
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
        /// 危害因素
        /// </summary>
        public virtual ICollection<TbmOccHazardFactor> OccHazardFactors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(16)]
        public virtual string ClientType { get; set; }

        /// <summary>
        /// 准备孕或育
        /// </summary>
        public virtual int? ReadyPregnancybirth { get; set; }

        /// <summary>
        /// 导引单号 单位累加、个人当天累加
        /// </summary>
        public virtual int? GuidanceNum { get; set; }

        /// <summary>
        /// 导引单打印次数
        /// </summary>
        public virtual int? GuidancePrintNum { get; set; }

        /// <summary>
        /// 介绍人名字
        /// </summary>
        [StringLength(64)]
        public virtual string Introducer { get; set; }

        /// <summary>
        /// 预约项目
        /// </summary>
        public virtual ICollection<TjlCustomerRegItem> CustomerRegItems { get; set; }
        /// <summary>
        /// 短信通知
        /// </summary>
        public virtual ICollection<TjlCusMessage> TjlCusMessages { get; set; }
        
        /// <summary>
        /// 人员类别
        /// </summary>
        public virtual PersonnelCategory PersonnelCategory { get; set; }

        /// <summary>
        /// 人员类别标识
        /// </summary>
        [ForeignKey(nameof(PersonnelCategory))]
        public Guid? PersonnelCategoryId { get; set; }
        /// <summary>
        /// 导引单照片
        /// </summary>
        public virtual Guid? GuidancePhotoId { get; set; }

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
        /// 存入时间
        /// </summary>        
        public virtual DateTime? CusCabitTime { get; set; }
        /// <summary>
        /// 健康证登记号
        /// </summary>
        [StringLength(32)]
        public virtual string JKZBM { get; set; }

        /// <summary>
        /// 合格证编码
        /// </summary>
        [StringLength(32)]
        public virtual string HGZBH { get; set; }
        /// <inheritdoc />
        public int TenantId { get; set; }
        /// <summary>
        /// 核酸类别
        /// </summary>
        public int? NucleicAcidType { get; set; }

        /// <summary>
        /// 开单医生Id
        /// </summary>
        [ForeignKey("OrderUser")]
        public virtual long? OrderUserId { get; set; }

        /// <summary>
        /// 开单医生
        /// </summary>
        public virtual User OrderUser { get; set; }

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
        /// Pacs号
        /// </summary>
        [StringLength(32)]
        public virtual string PacsBM { get; set; }

        /// <summary>
        /// 单位人员编号
        /// </summary>
        public virtual int? ClientRegNum { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>     
        [StringLength(128)]
        public virtual string FPNo { get; set; }

        /// <summary>
        /// 是否预约 0未预约 1已预约
        /// </summary>
        public int? BookingStatus{ get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime? AppointmentTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(128)]
        public virtual string Remarks { get; set; }

        /// <summary>
        ///  网报上传状态 0未上传 1已上传 2可上传
        /// </summary>
        public int? UploadState { get; set; }

        /// <summary> 
        /// 网报上传时间
        /// </summary>
        public DateTime? UploadStateTime { get; set; }

        /// <summary>
        /// 预约短信 0未发送 1已发送
        /// </summary>
        public int? RegMessageState { get; set; }

        /// <summary>
        ///  回访状态0未回访1已回访3已取消
        /// </summary>
        public virtual int? VisitSate { get; set; }

        /// <summary>
        /// 回访方式1短信2电话
        /// </summary>
        [MaxLength(32)]
        public virtual string VisitType { get; set; }
        /// <summary>
        /// 回访人外键
        /// </summary>
        [ForeignKey("VisitEmployeeBM")]
        public virtual long? VisitEmployeeId { get; set; }

        /// <summary>
        /// 回访人
        /// </summary>
        public virtual User VisitEmployeeBM { get; set; }

        /// <summary> 
        /// 回访日期
        /// </summary>
        public DateTime? VisitTime { get; set; }

        /// <summary>
        /// 报告短信 0未发送 1已发送
        /// </summary>
        public int? ReportMessageState { get; set; }
        /// <summary>
        /// 危急值短信 0未发送 1已发送
        /// </summary>
        public int? CrissMessageState { get; set; }
        /// <summary>
        /// 复查短信 0未发送 1已发送
        /// </summary>
        public int? VisitMessageState { get; set; }

        /// <summary>
        /// 问卷状态 0已问卷 1未问卷
        /// </summary>
        public int? QuestionState { get; set; }


        /// <summary> 
        /// 问卷日期
        /// </summary>
        public DateTime? QuestionTime { get; set; }
        /// <summary>
        /// 到检状态 0未到检 1已到检查
        /// /// </summary>
        public int? ArriveCheck { get; set; }

        /// <summary>
        /// 所属院区
        /// </summary>
        public virtual int? HospitalArea { get; set; }

        /// <summary>
        /// 职业史扫描照片
        /// </summary>
        public virtual ICollection<TjlCustomerOCCPic> OccQuesPhotos { get; set; }
        /// <summary>
        /// 职业史扫描照片
        /// </summary>
        public virtual Guid? OccQuesPhotoId { get; set; }

        /// <summary>
        /// 职业史扫描时间
        /// </summary>
        public virtual DateTime? OccQuesDate { get; set; }

        /// <summary>
        /// 职业史扫描状态 0未扫描1已扫描
        /// </summary>
        public virtual int? OccQuesSate { get; set; }

        /// <summary>
        /// 职业史扫描人外键
        /// </summary>
        [ForeignKey("OccQuesUser")]
        public virtual long? OccQuesUserId { get; set; }

        /// <summary>
        /// 职业史扫描人
        /// </summary>
        public virtual User OccQuesUser { get; set; }
        /// <summary>
        /// 放弃待查
        /// </summary>
        public virtual ICollection<TjlCusGiveUp> TjlCusGiveUps { get; set; }

        /// <summary>
        /// 需要复查项目
        /// </summary>
        public virtual ICollection<TjlCusReview> TjlCusReviews { get; set; }


        /// <summary>
        /// 微信报告导出状态 1未导出2已导出
        /// </summary>

        /// <summary>
        public virtual int? WXExportSate { get; set; }

        /// 签名
        /// </summary>
        public virtual Guid? SignBmId { get; set; }

        /// <summary>
        /// 拍照照片
        /// </summary>
        public virtual Guid? PhotoBmId { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }
      
        /// <summary>
        /// 挂号单号
        /// </summary>
        [MaxLength(32)]
        public virtual string rgst_no { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? StarTime { get; set; }

        /// <summary>
        /// 照射种类
        /// </summary>
        [MaxLength(320)]
        public virtual string RadiationName { get; set; }
        /// <summary>
        /// 用工单位名称
        /// </summary>
        [MaxLength(320)]
        public virtual string employerEnterpriseName { get; set; }
        /// <summary>
        /// 用工单位组织机构代码
        /// </summary>
        [MaxLength(320)]
        public virtual string employerCreditCode { get; set; }

        /// <summary>
        /// 预计出报告日期
        /// </summary>
        public virtual DateTime? ReportDate { get; set; }
    }
}