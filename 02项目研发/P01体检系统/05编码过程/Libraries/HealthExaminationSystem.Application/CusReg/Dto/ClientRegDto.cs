using System;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlClientReg))]
#endif 
    public class ClientRegDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位信息
        /// </summary>
        public QueryClientInfoDto ClientInfo { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartCheckDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime EndCheckDate { get; set; }

        /// <summary>
        /// 封帐状态 1已封帐 2未封帐
        /// </summary>
        public virtual int FZState { get; set; }

        /// <summary>
        /// 单位负责人 默认单位负责人
        /// </summary>
        [StringLength(64)]
        public virtual string linkMan { get; set; }
        /// <summary>
        /// 预约ID
        /// </summary>
        public virtual string ClientRegBM { get; set; }

        /// <summary>
        /// 人员类别
        /// </summary>
        public virtual PersonnelCategoryDto PersonnelCategory { get; set; }

        /// <summary>
        /// 人员类别标识
        /// </summary>       
        public Guid? PersonnelCategoryId { get; set; }
        /// <summary>
        /// 确认状态（财务确认）
        /// </summary>
        public virtual int? ConfirmState { get; set; }

        //
        // 创建时间
        //     Creation time of this entity.
        public DateTime CreationTime { get; set; }


        /// <summary>
        /// 预约描述
        /// </summary>
        [StringLength(128)]
        public virtual string RegInfo { get; set; }
        /// <summary>
        /// 说明 
        /// </summary>
        [StringLength(128)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 锁定状态 1锁定2未锁定
        /// </summary>
        public virtual int SDState { get; set; }

    }
}