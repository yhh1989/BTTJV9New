using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmOneAddXQuestionnaire))]
#endif
    public class OneAddQuestionsDto : EntityDto<Guid>
    {

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(32)]
        public virtual string Coding { get; set; }


        /// <summary>
        /// 外部编码
        /// </summary>
        [StringLength(20)]
        public virtual string ExternalCode { get; set; }

        /// <summary>
        /// 问卷名称
        /// </summary>
        [StringLength(100)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 问卷类别
        /// </summary>
        [StringLength(50)]
        public virtual string Category { get; set; }
        /// <summary>
        /// 问卷助记码
        /// </summary>
        [StringLength(50)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 序号
        /// </summary>        

        public virtual int? OrderNumber { get; set; }       



    }
}
