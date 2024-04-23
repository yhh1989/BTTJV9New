
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System.ComponentModel.DataAnnotations;

#if !Proxy
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{ 
 
        /// <summary>
        /// 查询客户预约信息Dto
        /// </summary>
#if !Proxy
        [Abp.AutoMapper.AutoMap(typeof(TjlCustomerReg))]
#endif
    public class CustomerRegRosterDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检来源 字典ClientSource
        /// </summary>
        public virtual int? InfoSource { get; set; }
            /// <summary>
            /// 初审医生id 用于总检分诊
            /// </summary>
            public virtual UserFormDto CSEmployeeBM { get; set; }
            /// <summary>
            /// 复审医生id 用于总检分诊
            /// </summary>
            public virtual Users.Dto.UserFormDto FSEmployeeBM { get; set; }
            /// <summary>
            /// 序号
            /// </summary>
            public virtual int? CustomerRegNum { get; set; }
            /// <summary>
            /// 分组id
            /// </summary>
            public virtual Guid ClientTeamInfo_Id { get; set; }
            /// <summary>
            /// 单位预约id
            /// </summary>
            public virtual Guid ClientRegId { get; set; }
            /// <summary>
            /// 导引单号
            /// </summary>
            public virtual int? GuidanceNum { get; set; }

            /// <summary>
            /// 体检人
            /// </summary>
            public virtual QueryCustomerDto Customer { get; set; }
            /// <summary>
            /// 单位分组
            /// </summary>
            public virtual ClientTeamInfoDto ClientTeamInfo { get; set; }

            /// <summary>
            /// 单位预约信息
            /// </summary>
            public virtual ClientRegDto ClientReg { get; set; }


#if Application
            [IgnoreMap]
#endif
            public virtual string GroupOrPersonal
            {
                get
                {
                    if (ClientReg != null)
                    {
                        return "单位";
                    }
                    else
                    {
                        return "个人";
                    }
                }

            }

            /// <summary>
            /// 预约ID
            /// </summary>  
            public virtual int? CustomerRegID { get; set; }
            /// <summary>
            /// 收费记录
            /// </summary>
            public virtual List<MReceiptClientDto> MReceiptInfo { get; set; }

            /// <summary>
            /// 体检人项目组合
            /// </summary>
            public virtual List<CustomerItemGroupDto> CustomerItemGroup { get; set; }

#if Application
            [IgnoreMap]
#endif
        public virtual string AddGroups
        {
            get
            {
                if (CustomerItemGroup != null)
                {
                    return string.Join(",", CustomerItemGroup.Where(o => o.IsAddMinus == (int)AddMinusType.Add).Select(o => o.ItemGroupName).ToList()).TrimEnd(',');
                }
                else
                {
                    return "";
                }
            }

        }


#if Application
            [IgnoreMap]
#endif
        public virtual string MinusGroups
        {
            get
            {
                if (CustomerItemGroup != null)
                {
                    return string.Join(",", CustomerItemGroup.Where(o => o.IsAddMinus == (int)AddMinusType.Minus).Select(o => o.ItemGroupName).ToList()).TrimEnd(',');
                }
                else
                {
                    return "";
                }
            }

        }
#if Application
            [IgnoreMap]
#endif
        public virtual string NoCheckState
        {
            get
            {
                if (CustomerItemGroup != null)
                {
                    return string.Join(",", CustomerItemGroup.Where(o => o.IsAddMinus != (int)AddMinusType.Minus 
                    && o.CheckState==(int)ProjectIState.Not).Select(o => o.ItemGroupName).ToList()).TrimEnd(',');
                }
                else
                {
                    return "";
                }
            }

        }
        /// <summary>
        /// 体检人已检金额
        /// </summary>
        public virtual decimal? AmountChecked { get; set; }
            /// <summary>
            /// 体检号
            /// </summary>
            public virtual string CustomerBM { get; set; }
            /// <summary>
            /// 总检状态 1未总检2已分诊3已初检4已审核
            /// </summary>
            public virtual int? SummSate { get; set; }
#if Application
            [IgnoreMap]
#endif
            public virtual string FormatSummSate
            {
                get
                {

                    // var Physical = CheckSateHelper.PhysicalEStateFormatter(CheckSate);
                    var Physical = SummSateHelper.SummSateFormatter(SummSate);
                    return Physical;
                }

            }

            /// <summary>
            /// 预约日期
            /// </summary>
            public virtual DateTime? BookingDate { get; set; }
            /// <summary>
            /// 登记状态 1未登记 2已登记
            /// </summary>
            public virtual int? RegisterState { get; set; }
#if Application
            [IgnoreMap]
#endif
            public virtual string FormatRegisterState
            {
                get
                {
                    //var Physical = RegisterStateHelper.GetSelectList(RegisterState);
                    //return Physical;
                    if (RegisterState == 1)
                    {
                        return "未登记";
                    }
                    if (RegisterState == 2)
                    {
                        return "已登记";
                    }
                    return null;
                }

            }
            /// <summary>
            /// 登记日期 第一次登记日期
            /// </summary>
            public virtual DateTime? LoginDate { get; set; }
            /// <summary>
            /// 套餐ID
            /// </summary>
            public virtual Guid ItemSuitBM_Id { get; set; }

            /// <summary>
            /// 套餐名称
            /// </summary>
            public virtual string ItemSuitName { get; set; }

            /// <summary>
            /// 体检状态 1未体检2体检中3体检完成
            /// </summary>
            public virtual int? CheckSate { get; set; }
#if Application
            [IgnoreMap]
#endif
            public virtual string FormatPhysical
            {
                get
                {
                    var Physical = CheckSateHelper.PhysicalEStateFormatter(CheckSate);
                    return Physical;
                }

            }
            /// <summary>
            /// 交表
            /// </summary>
            public int? SendToConfirm { get; set; }
#if Application
            [IgnoreMap]
#endif
            public virtual string SendToConfirmState
            {
                get
                {
                    var Physical = SendToConfirmHelper.SendToConfirmFormatter(SendToConfirm);
                    return Physical;
                }

            }
            /// <summary>
            /// 报告领取状态 1未领取2已领取
            /// </summary>
            public virtual int? ReceiveSate { get; set; }
            /// <summary>
            /// 报告自取 1自取2邮寄3已邮寄
            /// </summary>
            public virtual int? ReportBySelf { get; set; }
#if Application
            [IgnoreMap]
#endif
            public virtual string FormatReceive
            {
                get
                {
                    var Sate = "";
                    if (ReceiveSate == null)
                    {
                        Sate = ReceiveSateHelper.ReceiveSateFormatter(1);
                        return Sate;
                    }
                    Sate = ReceiveSateHelper.ReceiveSateFormatter(ReceiveSate);
                    return Sate;
                }

            }
            /// <summary>
            /// 体检类别
            /// </summary>
            public virtual int? PhysicalType { get; set; }
            /// <summary>
            /// 是否免费
            /// </summary>
            public bool? IsFree { get; set; }
            /// <summary>
            /// 人员类别
            /// </summary>
            public PersonnelCategoryDto PersonnelCategory { get; set; }
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
            /// 年龄
            /// </summary>
            public virtual int? RegAge { get; set; }

            /// <summary>
            /// 体检类别 字典
            /// </summary>
            public virtual string ClientType { get; set; }
            public virtual int? ReadyPregnancybirth { get; set; }

            public int TenantId { get; set; }
            public int? AgeStart { get; set; }
            public int? AgeEnd { get; set; }
            //单位Id
            public List<Guid> CompanysT { get; set; }
            //套餐Id
            public List<Guid> SetMealChoiceT { get; set; }
            //人数总数
            public int? Total { get; set; }
            //男总数
            public int? MaleTotal { get; set; }
            //女总数
            public int? FemaleTotal { get; set; }
            //未说明性别总数
            public int? Unknown { get; set; }
            //未体检总数
            public int? NoTotal { get; set; }
            //体检中总数
            public int? ConductTotal { get; set; }
            //已体检总数
            public int? AlreadyTotal { get; set; }
            /// <summary>
            /// 免费总数
            /// </summary>
            public int? IsFreeTotal { get; set; }


            /// <summary>
            /// 介绍人名字
            /// </summary>
            public virtual string Introducer { get; set; }

            /// <summary>
            /// 介绍人名字
            /// </summary>
            public virtual string DWIntroducer
            {
                get
                {
                    if (ClientReg != null && ClientReg.Id != Guid.Empty)
                    {
                        return ClientReg.linkMan;
                    }
                    else
                    {
                        return Introducer;
                    }
                }
            }
            /// <summary>
            /// 工种
            /// </summary>    
            public virtual string TypeWork { get; set; }
            /// <summary>
            /// 原体检人
            /// </summary>       
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
            /// 健康证编号
            /// </summary>
            [StringLength(32)]
            public virtual string JKZBM { get; set; }
            public int CreatorUserId { get; set; }
            public string UserNames { get; set; }
            /// <summary>
            /// Pacs号
            /// </summary>   
            public virtual string PacsBM { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(128)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 单位序号
        /// </summary>
        public virtual int? ClientRegNum { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>     
        [StringLength(128)]
        public virtual string FPNo { get; set; }

    }
    }
