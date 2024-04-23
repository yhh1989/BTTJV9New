using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 登记列表页面dto
    /// </summary>
    public class CusRegListViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 体检ID
        /// </summary>
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }
        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }
        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public int? RegisterState { get; set; }
        /// <summary>
        /// 收费状态 1未收费6已收费7欠费
        /// </summary>
        public virtual int? CostState { get; set; }
        /// <summary>
        /// 交表确认 1未交表2已交表
        /// </summary>
        public virtual int? SendToConfirm { get; set; }

        /// <summary>
        /// 登记序号
        /// </summary>
        public virtual int? CusRegNum { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 单位序号
        /// </summary>
        public virtual int? ClientRegNum { get; set; }
    }
}
