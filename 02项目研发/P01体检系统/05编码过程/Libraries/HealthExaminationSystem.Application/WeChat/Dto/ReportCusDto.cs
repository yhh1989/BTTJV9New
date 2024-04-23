using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{

    public class ReportCusDto
    {

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime?  LoginDate { get; set; }


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
        /// 单位名称
        /// </summary>
        [StringLength(256)]
        [Required]
        public virtual string ClientName { get; set; }


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
        [StringLength(32)]
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
        /// 检查号
        /// </summary>
        public virtual string customerBm { get; set; }


        /// <summary>
        /// 民族
        /// </summary>
        [StringLength(16)]
        public virtual string Nation { get; set; }

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
        /// 固定电话
        /// </summary>
        [StringLength(16)]
        public virtual string Telephone { get; set; }
    }
}
