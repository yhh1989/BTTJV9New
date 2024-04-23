using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmOccDictionary))]
#endif
    public class ShowOccDictionary : EntityDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(500)]
        public virtual string HelpChar { get; set; }
        /// <summary>
        /// 是否启用1启用0停用
        /// </summary>
        public virtual int IsActive { get; set; }
        /// <summary>
        ///  序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
        /// <summary>
        /// 字典类别，来自程序的枚举
        /// </summary>
        public virtual string Type { get; set; }
        /// <summary>
        /// 父级单位
        /// </summary>
        public virtual string Parent { get; set; }
        /// <summary>
        /// 父级单位
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 父级名称
        /// </summary>
        public virtual string ParentName { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(64)]
        public virtual string code { get; set; }



    }
}
