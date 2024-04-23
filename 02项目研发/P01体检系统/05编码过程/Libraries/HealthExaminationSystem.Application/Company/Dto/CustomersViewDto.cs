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

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlCustomer))]
#endif
    public class CustomersViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 档案号
        /// </summary>
        [StringLength(16)]
        public virtual string ArchivesNum { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }


        /// <summary>
        /// 身份证号
        /// </summary>    
        public virtual string IDCardNo { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>   
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public virtual string Department { get; set; }


    }
}
