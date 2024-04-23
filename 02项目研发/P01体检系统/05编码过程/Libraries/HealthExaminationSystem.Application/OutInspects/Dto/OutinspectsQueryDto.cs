using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.OutInspects.Dto
{
     
    public class OutinspectsQueryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 挂号科室
        /// </summary>
        public int? NucleicAcidType { get; set; }
        /// <summary>
        /// 登记序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Sex { get; set; }
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
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }
        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }
        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }
        /// <summary>
        /// 是否已打条码 1未打印2已打印3无须打印
        /// </summary>
        public virtual int? BarState { get; set; }
        /// <summary>
        ///体检日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }
        /// <summary>
        /// 套餐名称
        /// </summary>
        [StringLength(128)]
        public virtual string ItemSuitName { get; set; }
    }
}
