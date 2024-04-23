#if !Proxy
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerReg))]
#endif
    public class OccQueCusDto : EntityDto<Guid>
    {

        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual QueryCustomerDto Customer { get; set; }
        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 职业史扫描照片
        /// </summary>
        public virtual Guid? OccQuesPhotoId { get; set; }

        /// <summary>
        /// 职业史扫描时间
        /// </summary>
        public virtual DateTime? OccQuesDate { get; set; }

        /// <summary>
        /// 职业史扫描状态 1未扫描2已扫描
        /// </summary>
        public virtual int? OccQuesSate { get; set; }

        /// <summary>
        /// 职业史扫描人外键
        /// </summary> 
        public virtual long? OccQuesUserId { get; set; }
    }
}
