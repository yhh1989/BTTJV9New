using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TbmOccDictionary))]
#endif
    public class OutOccDictionaryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 父级名称
        /// </summary>
        public virtual string ParentName { get; set; }

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
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(64)]
        public virtual string code { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(3072)]
        public virtual string Remarks { get; set; }



    }
}
