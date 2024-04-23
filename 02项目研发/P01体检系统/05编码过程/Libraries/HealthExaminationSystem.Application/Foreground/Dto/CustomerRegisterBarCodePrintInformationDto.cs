using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Foreground.Dto
{
    /// <summary>
    /// 体检人预约条码号打印信息数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Examination.TjlCustomerBarPrintInfo))]
#endif
    public class CustomerRegisterBarCodePrintInformationDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人标识
        /// </summary>
        public Guid? CustomerReg_Id { get; set; }

        /// <summary>
        /// 条码标识
        /// </summary>
        public virtual Guid? BarSettingsId { get; set; }

        /// <summary>
        /// 条码名称
        /// </summary>
        [StringLength(1000)]
        public virtual string BarName { get; set; }

        /// <summary>
        /// 条码编号
        /// </summary>
        [StringLength(32)]
        public virtual string BarNumBM { get; set; }

        /// <summary>
        /// 打印时间
        /// </summary>
        public virtual DateTime? BarPrintTime { get; set; }

        /// <summary>
        /// 已抽血
        /// </summary>
        public bool HaveBlood { get; set; }

        /// <summary>
        /// 抽血时间
        /// </summary>
        public DateTime? BloodTime { get; set; }

        /// <summary>
        /// 抽血人
        /// </summary>
        public virtual UserForComboDto BloodUser { get; set; }
        /// <summary>
        /// 送检人
        /// </summary>
        public virtual UserForComboDto SendUser { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public virtual UserForComboDto ReceiveUser { get; set; }

        /// <summary>
        /// 已送检
        /// </summary>      
        public bool HaveSend { get; set; }
        /// <summary>
        /// 送检时间
        /// </summary> 
        public DateTime? SendTime { get; set; }


        /// <summary>
        /// 已接收
        /// </summary>    
        public bool HaveReceive { get; set; }
        /// <summary>
        /// 接收时间
        /// </summary>  
        public DateTime? ReceiveTime { get; set; }

        /// <summary>
        /// 体检人
        /// </summary>
        public virtual CustomerRegister52Dto1 CustomerReg { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        public virtual BarSettingsDto BarSettings { get; set; }

#if Proxy
        /// <summary>
        /// 体检人信息
        /// </summary>
        public string CustomerInformation => $"{CustomerReg?.Customer?.Name ?? "未知"}|{SexHelper.CustomSexFormatter(CustomerReg?.Customer?.Sex) ?? "未知"}|{CustomerReg?.CustomerBM ?? "未知"}|{CustomerReg?.ClientInfo?.ClientName ?? "无"}";
#endif
    }
}