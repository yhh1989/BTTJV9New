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

namespace Sw.Hospital.HealthExaminationSystem.Application.OAApproval.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlOAApproValcs))]
#endif
    public class CreatOAApproValcsDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>       
        public virtual Guid? ClientInfoId { get; set; }
        
        /// <summary>
        /// 表头编码
        /// </summary>
        [StringLength(500)]
        public virtual string TitleBM { get; set; }
        /// <summary>
        /// 表头名称
        /// </summary>
        [StringLength(500)]
        public virtual string TitleName { get; set; }
        /// <summary>
        /// 最高折扣
        /// </summary>

        public virtual Decimal? DiscountRate { get; set; }
        /// <summary>
        /// 加项最高折扣
        /// </summary>

        public virtual Decimal? AddDiscountRate { get; set; }


        /// <summary>
        /// 申请人
        /// </summary>
        [StringLength(50)]
        public virtual string Applicant { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>

        public virtual DateTime? AppliTime { get; set; }

        /// <状态>
        /// 审批状态 0未审批 1已审批2已拒绝
        /// </summary>

        public virtual int? AppliState { get; set; }

        /// <状态>
        /// 确认状态 0未确认1已确认
        /// </summary>
        public virtual int? OKState { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>

        public virtual DateTime? ApprovalTime { get; set; }

        
        /// <summary>
        /// 操作人
        /// </summary>       
        public virtual long? CreatorUserId { get; set; }
        /// <summary>
        /// 审批人Id
        /// </summary>       
        public virtual long? ApprovalUserId { get; set; }

      

        /// <summary>
        /// 抄送人Id
        /// </summary>       
        public virtual long? CCUserId { get; set; }

       
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 批示
        /// </summary>
        [StringLength(500)]
        public virtual string Comments { get; set; }

        /// <summary>
        /// 预约描述
        /// </summary>
        [StringLength(128)]
        public virtual string RegInfo { get; set; }



    }
}
