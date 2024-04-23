using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 体检人信息表  
    /// 医生站读取患者信息用
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomer))]
#endif
    public class ATjlCustomerDto : EntityDto<Guid>
    {
        /// <summary>
        /// 档案号
        /// </summary>
        [StringLength(16)]
        public virtual string ArchivesNum { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [StringLength(16)]
        public virtual string WorkNumber { get; set; }

        /// <summary>
        /// 会员卡
        /// </summary>
        [StringLength(16)]
        public virtual string CardNumber { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        [StringLength(16)]
        public virtual string Duty { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }

        /// <summary>
        /// 岁
        /// </summary>
        [StringLength(2)]
        public virtual string AgeUnit { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public virtual DateTime? Birthday { get; set; }

        /// <summary>
        /// 婚姻状况 1未婚2结婚
        /// </summary>
        public virtual int? MarriageStatus { get; set; }

        /// <summary>
        /// 文化程度 字典
        /// </summary>
        public virtual int? Degree { get; set; }

        /// <summary>
        /// 用户证件 IDCardType字典
        /// </summary>
        public virtual int? IDCardType { get; set; }

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
        /// 固定电话
        /// </summary>
        [StringLength(16)]
        public virtual string Telephone { get; set; }

        /// <summary>
        /// 所属省
        /// </summary>
        [StringLength(16)]
        public virtual string StoreAdressP { get; set; }

        /// <summary>
        /// 所属市
        /// </summary>
        [StringLength(16)]
        public virtual string StoreAdressS { get; set; }

        /// <summary>
        /// 所属区
        /// </summary>
        [StringLength(16)]
        public virtual string StoreAdressQ { get; set; }

        /// <summary>
        /// 通讯地址
        /// </summary>
        [StringLength(128)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        [StringLength(64)]
        public virtual string PostgCode { get; set; }

        /// <summary>
        /// 邮件
        /// </summary>
        [StringLength(64)]
        public virtual string Email { get; set; }

        /// <summary>
        /// 保密级别 字典
        /// </summary>
        public virtual int? Secretlevel { get; set; }

        /// <summary>
        /// 类别 1为普通用户2为VIP用户
        /// </summary>
        public virtual int? CustomerType { get; set; }

        /// <summary>
        /// 行业 字典
        /// </summary>
        [StringLength(64)]
        public virtual string CustomerTrade { get; set; }

        /// <summary>
        /// 个人照片
        /// </summary>
        public virtual string CusPhotoBM { get; set; }
        /// <summary>
        /// 个人照片
        /// </summary>
        public virtual Guid? CusPhotoBmId { get; set; }

        /// <summary>
        /// 国家 字典
        /// </summary>
        [StringLength(32)]
        public virtual string GuoJi { get; set; }

        /// <summary>
        /// QQ号
        /// </summary>
        [StringLength(32)]
        public virtual string Qq { get; set; }

        /// <summary>
        /// 姓名简写
        /// </summary>
        [StringLength(32)]
        public virtual string NameAB { get; set; }

        /// <summary>
        /// 五笔简码
        /// </summary>
        [StringLength(32)]
        public virtual string WbCode { get; set; }

        /// <summary>
        /// 就诊卡
        /// </summary>
        [StringLength(32)]
        public virtual string VisitCard { get; set; }

        /// <summary>
        /// 医保卡
        /// </summary>
        [StringLength(32)]
        public virtual string MedicalCard { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        [StringLength(32)]
        public virtual string SectionNum { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        [StringLength(32)]
        public virtual string HospitalNum { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(128)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public virtual string Department { get; set; }
    }
}