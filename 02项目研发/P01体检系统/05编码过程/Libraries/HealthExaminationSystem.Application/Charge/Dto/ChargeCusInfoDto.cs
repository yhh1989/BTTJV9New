using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#elif Proxy
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if Application
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class ChargeCusInfoDto : EntityDto<Guid>
    {
#if Proxy
        [JsonIgnore]
        public string CostStateDisplay
        {
            get
            {
                if (CostState.HasValue)
                    return CostStateHelper.CostStateFormatter(CostState.Value);

                return "";
            }
        }
         [JsonIgnore]        
         public string free
        {
            get
            {
                if (PersonnelCategoryId.HasValue && PersonnelCategory.IsFree == true)
                {
                    return "是";
                }
                else
                {
                    return "否";
                }

                
            }
        }
           public decimal PersonalMoney 
        {
            get
            {
                if (PersonnelCategoryId.HasValue && PersonnelCategory.IsFree == true)                  
                   return 0.00M;                      
                else
                return McusPayMoney.PersonalMoney ;
            }
        }
#endif

        /// <summary>
        /// 人员信息
        /// </summary>
        public virtual ChargeCusDto Customer { get; set; }
       


        ///// <summary>
        ///// 组合信息
        ///// </summary>
        //public virtual ICollection<ChargeGroupsDto> CustomerItemGroup { get; set; }

        ///// <summary>
        ///// 收费信息
        ///// </summary>
        //public virtual ICollection<MReceiptInfoPerDto> MReceiptInfo { get; set; }

        /// <summary>
        /// 个人应收已收
        /// </summary>
        public virtual CusPayMoneyViewDto McusPayMoney { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual ClientInfosNameDto ClientInfo { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 体检ID
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 体检类别 字典
        /// </summary>
        [StringLength(16)]
        public virtual string ClientType { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        [StringLength(128)]
        public virtual string ItemSuitName { get; set; }

        /// <summary>
        /// 收费状态 1未收费2已收费3欠费
        /// </summary>
        public virtual int? CostState { get; set; }

        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }
        /// <summary>
        /// 人员类别
        /// </summary>
        public virtual PersonnelCategoryDto PersonnelCategory { get; set; }

        /// <summary>
        /// 人员类别标识
        /// </summary>       
        public Guid? PersonnelCategoryId { get; set; }
    }

    #region 已检、放弃、待查、未检统计
    /// <summary>
    /// 患者检查项目统计(已检、放弃、待查、未检统计)
    /// </summary>
    public class PatientExaminationProjectStatisticsViewDto
    {
        //体检号
        public virtual string ArchivesNum { get; set; }

        //姓名
        public virtual string Name { get; set; }
        /// <summary>
        /// 介绍人
        /// </summary>
        public string Introducer { get; set; }
        // 性别
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(24)]
        public virtual string IDCardNo { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(16)]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(128)]
        public virtual string RegRemarks { get; set; }

        public string SexDisplay
        {
            get
            {
                switch (Sex)
                {
                    case 0:
                        return "未知的性别";
                    case 1:
                        return "男";
                    case 2:
                        return "女";
                    default: return "";
                }
            }
        }

        // 年龄
        public virtual int? Age { get; set; }

        // 单位
        public virtual string ClientName { get; set; }
        // 单位预约ID
        public virtual string ClientRegId { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// 登记状态
        /// </summary>
        public int? RegisterState { get; set; }

        /// <summary>
        /// 交表状态
        /// </summary>
        public int? SendToConfrim { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 检查状态
        /// </summary>
        public int? CusCheckSate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }
        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }
        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        //体检状态 1未体检2体检中3体检完成
        //public virtual int? CheckSate { get; set; }
        //总检状态 1未总检2已分诊3已初检4已审核
        public virtual int? SummSate { get; set; }

        public virtual string SummSateDisplay
        {
            get
            {
                switch (SummSate)
                {
                    case 1:
                        return "未总检";
                    case 2:
                        return "已分诊";
                    case 3:
                        return "已初检";
                    case 4:
                        return "已审核";
                    default: return "";
                }
            }
        }

        //打印状态 1未打印2已打印
        public virtual int? PrintSate { get; set; }

        public virtual string PrintSateDisplay
        {
            get
            {
                switch (PrintSate)
                {
                    case 1:
                        return "未打印";
                    case 2:
                        return "已打印";
                    default: return "";
                }
            }
        }

        //收费状态 1未收费2已收费3欠费
        public virtual int? CostState { get; set; }

        public string CostStateDisplay 
        {
            get
            {
                var Sate = "";
                if (CostState != null)
                {

                    Sate = PayerCatTypeHelper.PayerCatTypeHelperFormatter(CostState);
                }
               
                return Sate;
            }

            //get
            //{
            //    switch (CostState)
            //    {
            //        case 1:
            //            return "未收费";
            //        case 2:
            //            return "个人支付";
            //        case 3:
            //            return "单位支付";
            //        case 4:
            //            return "混合付款";
            //        case 5:
            //            return "赠检";
            //        case 6:
            //            return "已收费";
            //        case 7:
            //            return "欠费";
            //        default: return "";
            //    }
            //}
        }

        //项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存
        //public virtual int? CheckState { get; set; }
        //应收金额
        public virtual decimal? ItemPrice { get; set; }

        //单位应收金额
        public virtual decimal? ClientPrice { get; set; }

        //实收金额
        public virtual decimal? PriceAfterDis { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        public virtual string ItemSuitName { get; set; }

        /// <summary>
        /// 体检人项目组合
        /// </summary>
        public virtual List<ATjlCustomerItemGroupDto> CustomerItemGroup { get; set; }

        /// <summary>
        /// 未检查组合数
        /// </summary>
        public virtual int? UnCheckNum { get; set; }

        /// <summary>
        /// 已检查组合数
        /// </summary>
        public virtual int? CheckNum { get; set; }

        /// <summary>
        /// 放弃检查组合数
        /// </summary>
        public virtual int? GiveupCheckNum { get; set; }

        /// <summary>
        /// 待查检查数
        /// </summary>
        public virtual int? AwaitCheckNum { get; set; }

        /// <summary>
        /// 未检查项目
        /// </summary>
        public virtual string UnCheckItems { get; set; }

        /// <summary>
        /// 已检查项目
        /// </summary>
        public virtual string CheckItems { get; set; }
        /// <summary>
        /// 部分检查
        /// </summary>
        public virtual string PartItems { get; set; }
        /// <summary>
        /// 放弃检查项目
        /// </summary>
        public virtual string GiveupCheckItems { get; set; }

        /// <summary>
        /// 待查检查项目
        /// </summary>
        public virtual string AwaitCheckItems { get; set; }
        /// <summary>
        /// 待查日期
        /// </summary>
        public virtual string AwaitDate { get; set; }
        /// <summary>
        /// 放弃日期
        /// </summary>
        public virtual string GiveupDate { get; set; }
        /// <summary>
        /// 加项目
        /// </summary>
        public virtual string AddCheckItems { get; set; }

        /// <summary>
        /// 减项目
        /// </summary>
        public virtual string MinusCheckItems { get; set; }
    }

    /// <summary>
    /// 查询条件（已检、放弃、待查、未检统计）
    /// </summary>
    public class PatientExaminationCondition
    {
        //单位
        public List<string> ClientName { get; set; }

        // 单位预约ID
        public virtual List<Guid?> ClientRegId { get; set; }

        //体检号
        public virtual string ArchivesNum { get; set; }

        public virtual string CustomerBM { get; set; }

        //姓名
        public virtual string Name { get; set; }

        // 时间类别
        public int? DateType { get; set; }

        // 开始时间
        public DateTime? StartTime { get; set; }

        // 结束时间
        public DateTime? EndTime { get; set; }

        //项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存
        public virtual int? CheckState { get; set; }
        public virtual int? PersonlCheckState { get; set; }

        //总检状态 1未总检2已分诊3已初检4已审核
        public virtual int? SummSate { get; set; }

        //打印状态 1未打印2已打印
        public virtual int? PrintSate { get; set; }

        //收费状态 1未收费2已收费3欠费
        public virtual int? CostState { get; set; }

        //加减状态 1为正常项目2为加项3为减项
        public virtual int? IsAddMinus { get; set; }
        //是否仅查个人
        public string IsPersonal { get; set; }
        /// <summary>
        /// 介绍人
        /// </summary>
        public string Introducer { get; set; }
    }
    #endregion
}