#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlQuestiontem))]
#endif
    public class TjlQuestiontemDto
    {
        /// <summary>
        /// 选项
        /// </summary>
        [StringLength(320)]
        public virtual string itemName { get; set; }

        /// <summary>
        /// 给的分数
        /// </summary>
        public virtual int? grade { get; set; }

        /// <summary>
        /// 是否已选中
        /// </summary>
        public virtual int? isSelected { get; set; }


        /// <summary>
        /// 序号
        /// </summary>   
        public virtual int? OrderNum { get; set; }
    }
}
