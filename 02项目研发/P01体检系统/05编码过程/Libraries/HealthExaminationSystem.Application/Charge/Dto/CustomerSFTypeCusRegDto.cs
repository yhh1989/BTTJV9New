#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerReg))]
#endif
    public class CustomerSFTypeCusRegDto
    {

        public virtual ClientInfosNameDto ClientInfo { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }       
      
        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 介绍人名字
        /// </summary>
        [StringLength(64)]
        public virtual string Introducer { get; set; }
        /// <summary>
        /// 人员类别
        /// </summary>
        public virtual PersonnelCategoryDto PersonnelCategory { get; set; }

    }
}
