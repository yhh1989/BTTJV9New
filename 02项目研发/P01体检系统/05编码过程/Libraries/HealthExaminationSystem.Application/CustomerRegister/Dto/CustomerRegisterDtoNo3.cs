using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto
{
    /// <summary>
    /// 体检人预约信息数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Examination.TjlCustomerReg))]
#endif
    public class CustomerRegisterDtoNo3 : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual CustomerInformationDtoNo1 Customer { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public string CustomerRegisterCode { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// 单位预约信息外键
        /// </summary>
        public Guid? CompanyRegisterId { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual CompanyInformationDtoNo2 CompanyInformation { get; set; }

        /// <summary>
        /// 人员类别
        /// </summary>
        public PersonnelCategoryDto PersonnelCategory { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? RegAge { get; set; }

        /// <summary>
        /// 体检类别
        /// <para>已废弃：</para>
        /// <para>1健康体检2职业病体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检</para>
        /// <para>参考：<see cref="Enum体检类型"/></para>
        /// </summary>
        public virtual int? PhysicalType { get; set; }

        /// <summary>
        /// 初审医生id 用于总检分诊
        /// </summary>
        public virtual UserDtoNo1 CSEmployeeBM { get; set; }

        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

#if Proxy
        /// <summary>
        /// 获取体检人年龄
        /// </summary>
        /// <returns></returns>
        public int GetCustomerAge()
        {
            return RegAge ?? Customer?.Age ?? -1;
        }
#endif
    }
}