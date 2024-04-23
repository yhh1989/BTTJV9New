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

namespace Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlCustomer))]
#endif
    public class OccCusInfoDto : EntityDto<Guid>
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
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }
        
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
        /// 民族
        /// </summary>
        [StringLength(16)]
        public virtual string Nation { get; set; }

        /// <summary>
        /// 通讯地址
        /// </summary>
        [StringLength(128)]
        public virtual string Address { get; set; }


    }
}
